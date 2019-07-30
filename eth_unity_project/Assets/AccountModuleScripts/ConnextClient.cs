using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using Nethereum.HdWallet;
using System.Threading.Tasks;

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

    public async void Init()
    {
        wallet = HDWallet.getInstance();
        address = wallet.GetAccount(0).Address;
        auth = new ConnextAuth();
        auth.address = address;

        // Get connext config
        HttpResponseMessage response = await client.GetAsync(connextHubUrl + "/config");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Connext hub not responding.");
        }
        var configJson = await response.Content.ReadAsStringAsync();
        Debug.Log("connext config: " + configJson);
        config = JsonConvert.DeserializeObject<Config>(configJson);

        // Auth sequence
        await Authorisation();

        await getBalance();
    }

    public async Task Authorisation()
    {
        //var data = "";
        //var content = new StringContent(data);
        var request = new HttpRequestMessage();
        Debug.Log("Requesting nonce for " + address);
        auth.addAuthHeaders(request);
        request.RequestUri = new Uri(connextHubUrl + "/nonce");
        request.Method = HttpMethod.Get;
        HttpResponseMessage response = await client.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Connext auth request failed." + response.ReasonPhrase);
        }
        var authJson = await response.Content.ReadAsStringAsync();
        Nonce nonce = JsonConvert.DeserializeObject<Nonce>(authJson);
        auth.setNonce(nonce.nonce);
        Debug.Log("auth nonce: " + nonce.nonce);
    }

    public async Task getBalance()
    {
        var request = new HttpRequestMessage();
        auth.addAuthHeaders(request);
        request.RequestUri = new Uri(connextHubUrl + "/channel/" + address + "/latest-no-pending");
        request.Method = HttpMethod.Get;
        HttpResponseMessage response = await client.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Connext balance request failed." + response.ReasonPhrase);
        }
        var bal = await response.Content.ReadAsStringAsync();
        Debug.Log("balance result: " + bal);
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

        public void addAuthHeaders(HttpRequestMessage req)
        {
            Debug.Log("setting header. sig: " + signature);
            var headers = req.Headers;
            headers.Add("x-address", address);
            headers.Add("x-nonce", nonce);
            headers.Add("x-signature", signature);
        }

        // Set nonce and sign it to get signature.
        public void setNonce(string nonce)
        {
            this.nonce = nonce;
            this.signature = HDWallet.sign(nonce);
        }
    }
}
