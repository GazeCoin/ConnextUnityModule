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
    }

    public async Task Authorisation()
    {
        //var data = "";
        //var content = new StringContent(data);
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
        Nonce nonce = JsonConvert.DeserializeObject<Nonce>(authJson);
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
        channelState = JsonConvert.DeserializeObject<ChannelState>(bal);
        Debug.Log("balance result: " + channelState.getBalanceEthHub());
    }

    public ChannelState getChannelState()
    {
        return channelState;
    }

    public async Task RequestDeposit(decimal tokens, decimal wei)
    {
        PurchasePayment.PaymentAmounts amounts = new PurchasePayment.PaymentAmounts
        {
            amountToken = tokens,
            amountWei = wei
        };

        RequestDeposit rd = new RequestDeposit();
        rd.setPaymentRequest(amounts);
        rd.lastChanTx = ++channelTxCount;
        rd.lastThreadUpdateId = channelThreadCount;
        string jsonRequest = JsonConvert.SerializeObject(rd);
        Debug.Log("Deposit Request:" + jsonRequest);

        var request = new HttpRequestMessage(HttpMethod.Post, "/channel/" + address + "/request-deposit");
        request.Content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Connext balance request failed." + response.ReasonPhrase);
        }
        var bal = await response.Content.ReadAsStringAsync();
        channelState = JsonConvert.DeserializeObject<ChannelState>(bal);
        Debug.Log("balance result: " + channelState.getBalanceEthHub());

    }

    class Nonce
    {
        public string nonce;
    }

    class ConnextAuth
    {
        public string address;
        private string nonce;
        private string signature;

        public void AddAuthHeaders(HttpRequestMessage req)
        {
            Debug.Log("setting header. sig: " + signature);
            var headers = req.Headers;
            headers.Add("x-address", address);
            headers.Add("x-nonce", nonce);
            headers.Add("x-signature", signature);
        }
        public void AddAuthHeaders(HttpWebRequest req)
        {
            Debug.Log("setting header. sig: " + signature);
            var headers = req.Headers;
            headers.Add("x-address", address);
            headers.Add("x-nonce", nonce);
            headers.Add("x-signature", signature);
        }

        public void AddAuthHeaders(HttpClient client)
        {
            Debug.Log("setting header. sig: " + signature);
            var headers = client.DefaultRequestHeaders;
            headers.Add("x-address", address);
            headers.Add("x-nonce", nonce);
            headers.Add("x-signature", signature);
        }


        // Set nonce and sign it to get signature.
        public void SetNonce(string nonce)
        {
            this.nonce = nonce;
            this.signature = HDWallet.sign(nonce);
        }
    }


}
