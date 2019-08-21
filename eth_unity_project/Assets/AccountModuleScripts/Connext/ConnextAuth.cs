using Newtonsoft.Json;
using System;
//using System.Net.Http;
using UnityEngine;
using System.Net;
using UnityEngine.Networking;

class ConnextAuth
{
    public string address;
    private string nonce;
    private string signature;

    public void AddAuthHeaders(Utils.WebRequest req)
    {
        Debug.Log("setting header. sig: " + signature);
        //var headers = req.Headers;
        req.AddHeader("x-address", address);
        req.AddHeader("x-nonce", nonce);
        req.AddHeader("x-signature", signature);
    }
    //public void AddAuthHeaders(HttpWebRequest req)
    //{
    //    Debug.Log("setting header. sig: " + signature);
    //    var headers = req.Headers;
    //    headers.Add("x-address", address);
    //    headers.Add("x-nonce", nonce);
    //    headers.Add("x-signature", signature);
    //}

    //public void AddAuthHeaders(HttpClient client)
    //{
    //    Debug.Log("setting header. sig: " + signature);
    //    var headers = client.DefaultRequestHeaders;
    //    headers.Add("x-address", address);
    //    headers.Add("x-nonce", nonce);
    //    headers.Add("x-signature", signature);
    //}


    // Set nonce and sign it to get signature.
    public void SetNonce(string nonce)
    {
        this.nonce = nonce;
    }

    public class Nonce
    {
        public string nonce;
    }
}