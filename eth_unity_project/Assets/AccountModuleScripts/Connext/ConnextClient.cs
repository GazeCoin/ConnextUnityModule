using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using Nethereum.HdWallet;
using System.Threading.Tasks;
using Nethereum.Web3;
using System.Numerics;
using System.Net;
using UnityEngine.Networking;

public class ConnextClient
{
    public class Config
    {
        public string contractAddress;
        public string ethChainId;
        public string hubAddress;
        public string maxCollateralization;
        public string tokenAddress;
    }

    private static string connextHubUrl = "https://hub.gazecoin.xyz";
    private static HttpClient client = new HttpClient();
    private static Config config = new Config();
    private static string address; // Wallet address, as string
    private static Wallet wallet;
    private ConnextAuth auth;
    private ChannelState channelState;
    private static int channelTxCount;
    private static int channelThreadCount;

    public async void Init()
    {
        client.BaseAddress = new Uri(connextHubUrl);
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        wallet = HDWallet.getInstance();
        address = wallet.GetAccount(0).Address;
        auth = new ConnextAuth
        {
            address = address
        };

        // Get connext config
        HttpResponseMessage response = await client.GetAsync("/config");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Connext hub not responding.");
        }
        var configJson = await response.Content.ReadAsStringAsync();
        Debug.Log("connext config: " + configJson);
        config = JsonConvert.DeserializeObject<Config>(configJson);

        // Auth sequence
        await Authorisation();

        await FetchChannelState();
        channelTxCount = channelState.txCountChain;

        //if (channelState.HasTxToSync())
        //{
            await HubSync();
        //}
    }

    public async Task Authorisation()
    {
        var request = new HttpRequestMessage();
        Debug.Log("Requesting nonce for " + address);
        auth.AddAuthHeaders(request);
        request.RequestUri = new Uri(connextHubUrl + "/nonce");
        request.Method = HttpMethod.Get;
        HttpResponseMessage response = await client.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Connext auth request failed." + response.ReasonPhrase);
        }
        var authJson = await response.Content.ReadAsStringAsync();
        ConnextAuth.Nonce nonce = JsonConvert.DeserializeObject<ConnextAuth.Nonce>(authJson);
        auth.SetNonce(nonce.nonce);
        Debug.Log("auth nonce: " + nonce.nonce);
        // Set auth headers for future use.
        auth.AddAuthHeaders(client);
    }

    public async Task FetchChannelState()
    {
        HttpResponseMessage response = await client.GetAsync("/channel/" + address + "/latest-no-pending");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Connext balance request failed." + response.ReasonPhrase);
        }
        var bal = await response.Content.ReadAsStringAsync();
        Debug.Log("channel state: " + bal);
        channelState = JsonConvert.DeserializeObject<ChannelState>(bal);
        Debug.Log("balance result: " + channelState.getBalanceEthHub());
    }

    public ChannelState getChannelState()
    {
        return channelState;
    }

    public async Task RequestDeposit(BigInteger tokens, BigInteger wei)
    {
        await RequestDeposit((decimal)tokens, (decimal)wei);
    }

    public async Task RequestDeposit(decimal tokens, decimal wei)
    {
        PurchasePayment.PaymentAmounts amounts = new PurchasePayment.PaymentAmounts();
        amounts.setAmounts(wei, tokens);

        RequestDeposit rd = new RequestDeposit();
        rd.setPaymentRequest(amounts);
        rd.lastChanTx = channelTxCount;
        rd.lastThreadUpdateId = channelThreadCount;
        string jsonRequest = JsonConvert.SerializeObject(rd);
        Debug.Log("Deposit Request:" + jsonRequest);

        var request = new HttpRequestMessage(HttpMethod.Post, "/channel/" + address + "/request-deposit");
        request.Content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Connext deposit request failed." + response.ReasonPhrase);
        }
        var bal = await response.Content.ReadAsStringAsync();
        channelState = JsonConvert.DeserializeObject<ChannelState>(bal);
        Debug.Log("balance result: " + channelState.getBalanceEthHub());
    }

    public async Task HubSync()
    {
        HttpResponseMessage response = await client.GetAsync(
            String.Format("/channel/{0}/sync?lastChanTx={1}&lastThreadUpdateId={2}", address, channelTxCount, channelThreadCount));
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Connext sync request failed." + response.ReasonPhrase);
        }
        var sync = await response.Content.ReadAsStringAsync();
        var syncResult = JsonConvert.DeserializeObject<SyncResult>(sync);
        Debug.Log("sync result: " + sync);

        // TODO: handle states other than CS_OPEN
        if ("CS_OPEN".Equals(syncResult.status))
        {
            if (syncResult.updates.Length > 0)
            {
                SyncResult.UpdateDetails update = syncResult.updates[0].update;
                if ("ProposePendingDeposit".Equals(update.reason))
                {
                    // TODO validate args signature
                    // Generate new state
                    ChannelState newState = channelState.Clone();
                    newState.pendingDepositTokenHub = update.args.depositTokenHub;
                    newState.pendingDepositTokenUser = update.args.depositTokenUser;
                    newState.pendingDepositWeiHub = update.args.depositWeiHub;
                    newState.pendingDepositWeiUser = update.args.depositWeiUser;
                    newState.recipient = channelState.user;
                    newState.timeout = update.args.timeout.ToString(); // TODO calculate this; shouuld it be long in ChannelState?
                    newState.txCountChain++;
                    newState.txCountGlobal++;

                    // Sign state update
                    var hash = createChannelStateHash(newState);

                    // Get gas price
                    // Generate token allowance and approve txs
                    // Invoke connext contract UserAUthorizedUpdate
                }
                // TODO: handle other reason types
                //TODO: handle > 1 update
            }
        }
    }

}
