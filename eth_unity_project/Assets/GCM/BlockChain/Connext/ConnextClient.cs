using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Net.Http;
using UnityEngine;
using Nethereum.HdWallet;
using System.Threading.Tasks;
using Numba.Awaiting.Engine;
using Nethereum.Web3;
using System.Numerics;
using System.Net;
using UnityEngine.Networking;
using Nethereum.Contracts;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.StandardTokenEIP20;
using Nethereum.Util;

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

    private const string CONNEXT_HUB_URL = "https://hub.gazecoin.xyz";
    private static ConnextConfig config;
    private static string address; // Wallet address, as string
    private static Wallet wallet;
    private ConnextAuth auth;
    private ChannelState channelState;
    private static uint channelTxCount;
    private static uint channelThreadCount;
    private Nethereum.Web3.Accounts.Account account;
    private ERC20Token token;
    private ChannelManagerContract channelManager;
    private Web3 web3;
    private string ethNodeUrl;
    //private const string NATS_URL = "nats://demo.nats.io"; 
    private const string NATS_URL = "wss://rinkeby.indra.connext.network:4222/api/messaging";
    private MessagingClient mc;

    public ConnextClient(Web3 web3, Nethereum.Web3.Accounts.Account account, string url)
    {
        this.account = account;
        this.web3 = web3;
        this.ethNodeUrl = url;
    }

    public async Task Init()
    {
        //client.BaseAddress = new Uri(connextHubUrl);
        //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        wallet = HDWallet.getInstance();
        address = wallet.GetAccount(0).Address;
        auth = new ConnextAuth
        {
            Address = address
        };

        mc = new MessagingClient(NATS_URL);
        mc.Connect();

        // Get connext config
        Debug.Log("get config");
        string configJson = mc.Send("config.get");
        Debug.Log("connext config: " + configJson);

        /* Utils.WebRequest request = new Utils.WebRequest(CONNEXT_HUB_URL + "/config", "GET");
        //await request.DoRequest();

        if (!request.IsSuccess())
        {
            throw new Exception("Connext hub not responding.");
        }
        var configJson = request.Response; */
        config = JsonConvert.DeserializeObject<ConnextConfig>(configJson); 


        /*
        // Get ERC20 token contract instance
        token = new ERC20Token(account, config.tokenAddress, ethNodeUrl);
        channelManager = new ChannelManagerContract(account, config.contractAddress, ethNodeUrl);

        // Auth sequence
        await Authorisation();

        await FetchChannelState();
        channelTxCount = channelState.txCountChain;

        await HubSync();
        */
    }

    public async Task Authorisation()
    {
        //var request = new HttpRequestMessage();
        Debug.Log("Requesting nonce for " + address);
        Utils.WebRequest request = new Utils.WebRequest(CONNEXT_HUB_URL + "/nonce", "GET");
        auth.AddAuthHeaders(request);
        await request.DoRequest();
        if (!request.IsSuccess())
        {
            throw new Exception("Connext auth request failed." + request.ReasonMessage);
        }
        var authJson = request.Response;
        ConnextAuth.NonceSerialiser nonce = JsonConvert.DeserializeObject<ConnextAuth.NonceSerialiser>(authJson);
        auth.Nonce = nonce.nonce;
        Debug.Log("auth nonce: " + nonce.nonce);
        // Set auth headers for future use.
        //auth.AddAuthHeaders(client); 
    }

    public async Task FetchChannelState()
    {
        Debug.Log("FetchChannelState");
        Utils.WebRequest request = new Utils.WebRequest(CONNEXT_HUB_URL + "/channel/" + address + "/latest-no-pending", "GET");
        auth.AddAuthHeaders(request);
        await request.DoRequest();
        if (!request.IsSuccess())
        {
            throw new Exception("Connext balance request failed." + request.ReasonMessage);
        }
        var bal = request.Response;
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
        rd.SetPaymentRequest(amounts);
        rd.lastChanTx = channelTxCount;
        rd.lastThreadUpdateId = channelThreadCount;
        string jsonRequest = JsonConvert.SerializeObject(rd);
        Debug.Log("Deposit Request:" + jsonRequest);

        Utils.WebRequest request = new Utils.WebRequest(CONNEXT_HUB_URL + "/channel/" + address + "/request-deposit", "POST");
        request.SetBody(jsonRequest);
        auth.AddAuthHeaders(request);
        await request.DoRequest();
        if (!request.IsSuccess())
        {
            throw new Exception("Connext deposit request failed." + request.ReasonMessage);
        }
        var bal = request.Response;

        channelState = JsonConvert.DeserializeObject<ChannelState>(bal);
        Debug.Log("balance result: " + channelState.getBalanceEthHub());
    }

    public async Task RequestCollateral(long txCountGlobal)
    {
        Debug.Log("Requesting collateralisation");
        string jsonRequest = "{\"lastChanTx:\" {0} }".Replace("{0}", txCountGlobal.ToString());

        Utils.WebRequest request = new Utils.WebRequest(CONNEXT_HUB_URL + "/channel/" + address + "/request-collateralization", "POST");
        request.SetBody(jsonRequest);
        auth.AddAuthHeaders(request);
        await request.DoRequest();
        if (!request.IsSuccess())
        {
            throw new Exception("Collateral request failed." + request.ReasonMessage);
        }
        Debug.Log("Collateralization response" + request.Response.ToString());
    }
    public async Task UpdateHub(UpdateRequest[] updateRequest, long lastThreadUpdateId)
    {
        Debug.Log("Update hub");
        string jsonRequest = "{\"lastThreadUpdateId:\" {0} }".Replace("{0}", lastThreadUpdateId.ToString());

        Utils.WebRequest request = new Utils.WebRequest(CONNEXT_HUB_URL + "/channel/" + address + "/request-collateralization", "POST");
        request.SetBody(jsonRequest);
        auth.AddAuthHeaders(request);
        await request.DoRequest();
        if (!request.IsSuccess())
        {
            throw new Exception("Collateral request failed." + request.ReasonMessage);
        }
        Debug.Log("Collateralization response" + request.Response.ToString());
    }

    public async Task HubSync()
    {
        Utils.WebRequest request = new Utils.WebRequest(String.Format("{0}/channel/{1}/sync?lastChanTx={2}&lastThreadUpdateId={3}", CONNEXT_HUB_URL, address, channelTxCount, channelThreadCount), "GET");
        auth.AddAuthHeaders(request);
        await request.DoRequest();
        if (!request.IsSuccess())
        {
            throw new Exception("Connext sync request failed." + request.ReasonMessage);
        }
        var sync = request.Response;

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
                    await ProposePendingDeposit(update);
                }
                // TODO: handle other reason types
                //TODO: handle > 1 update
            }
        }
    }

    private async Task ProposePendingDeposit(SyncResult.UpdateDetails update)
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
        newState.sigUser = SignChannelStateHash(newState);
        Debug.Log("signed channel state update: " + newState.sigUser);
        newState.sigHub = update.sigHub;
        //maxTimeout = getUpdateReqestTimeout(); TODO

        // Get gas price
        var gasPrice = Web3.Convert.ToWei(4, UnitConversion.EthUnit.Gwei); //TODO - should be dynamic
        Debug.Log("GasPrice " + gasPrice.ToString());

        // Generate token allowance and approve txs
        if (UInt64.Parse(update.args.depositTokenUser) > 0)
        {
            try
            {
                Debug.Log("Token deposit - requesting approve");
                var tokenService = new Nethereum.StandardTokenEIP20.StandardTokenService(web3, config.tokenAddress);

                BigInteger allowance = await tokenService.AllowanceQueryAsync(channelState.user, config.contractAddress);
                Debug.Log("Allowance: " + allowance.ToString());
                var receipt = await tokenService.ApproveRequestAndWaitForReceiptAsync(config.contractAddress, BigInteger.Parse(update.args.depositTokenUser));
                Debug.Log("Approve request completed. " + receipt);
            } catch (Exception ex)
            {
                Debug.Log("Error submitting approve request." + ex.Message);
            } 
        }

        // Invoke connext contract UserAuthorizedUpdate
        var updateHandler = channelManager.GetContractHandler();
        ChannelManagerContract.UserAuthorizedUpdateFunction fm = (ChannelManagerContract.UserAuthorizedUpdateFunction)newState.UserAuthorizedUpdateFunction();
        fm.AmountToSend = BigInteger.Parse(update.args.depositWeiUser);
        fm.GasPrice = gasPrice;
        fm.Gas = new BigInteger(1000000);
        //var txReceipt = await updateHandler.SendRequestAsync(config.contractAddress, fm);
        var txReceipt = await updateHandler.SendRequestAndWaitForReceiptAsync(config.contractAddress, fm);
        Debug.Log("Deposit requested from channelManager contract. tx hash: " + txReceipt.TransactionHash.ToString());

        //var balHandler = web3.Eth.GetContractHandler(config.contractAddress);
        //ChannelManagerContract.GetChannelBalances fm = new ChannelManagerContract.GetChannelBalances() 
        //{
        //    User = "0xc303b2169f64b636d143c6fb89e62506e451b0f3"  //account.Address
        //};
        //var txReceipt = await balHandler.QueryDeserializingToObjectAsync<ChannelManagerContract.GetChannelBalances, ChannelManagerContract.ChannelBalances>(fm);
        //Debug.Log("Deposit requested from channelManager contract. " + txReceipt.TokenTotal);
    }

    private string SignChannelStateHash(ChannelState state)
    {
        return HDWallet.HashAndSign(state.getBytes());
    }

}
