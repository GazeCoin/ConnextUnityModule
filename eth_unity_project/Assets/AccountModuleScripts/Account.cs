using System.Collections;
//using Nethereum.JsonRpc.UnityClient;
using UnityEngine;

public class Account {
    public void CreateAccount(string password, System.Action<string, string> callback)
    {
        //var ecKey = Nethereum.Signer.EthECKey.GenerateKey();
        //var address = ecKey.GetPublicAddress();
        //var privateKey = ecKey.GetPrivateKeyAsBytes();
        //var keystoreservice = new Nethereum.KeyStore.KeyStoreService();
        //var encryptedJson = keystoreservice.EncryptAndGenerateDefaultKeyStoreAsJson(password, privateKey, address);
        //callback(address, encryptedJson);
    }	
}