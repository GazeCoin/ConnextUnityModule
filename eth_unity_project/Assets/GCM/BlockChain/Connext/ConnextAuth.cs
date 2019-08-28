using Newtonsoft.Json;
using System;
using UnityEngine;
using System.Net;
using UnityEngine.Networking;

class ConnextAuth
{
    public string Address;
    public string Nonce { get; set; }
    public string Signature { get; protected set; }
    private bool IsSigned;

    public void AddAuthHeaders(Utils.WebRequest req)
    {
        if (!IsSigned && Nonce != null)
        {
            Sign();
        }
        Debug.Log("setting header. sig: " + Signature);
        if (Address != null) req.AddHeader("x-address", Address);
        if (Nonce != null) req.AddHeader("x-nonce", Nonce);
        if (Signature != null) req.AddHeader("x-signature", Signature);
    }

    private void Sign()
    {
        this.Signature = HDWallet.Sign(Nonce);
        IsSigned = true;
    }


    public class NonceSerialiser
    {
        public string nonce;
    }
}