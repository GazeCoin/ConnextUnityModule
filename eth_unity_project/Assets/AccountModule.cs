//using System.Collections;
//using Nethereum.JsonRpc.UnityClient;
//using UnityEngine;

//// https://github.com/Nethereum/Nethereum/blob/master/src/Nethereum.Unity/UnityRPCRequests.cs
//// SEE API: https://nethereum.readthedocs.io/en/latest/nethereum-transferring-ether/
//// SEE REFERENCE: https://github.com/Nethereum/Nethereum/blob/master/src/Nethereum.Unity/UnityRPCRequests.cs

//// USAGE:
//// Drag the AccountModule onto the stage of Unity within the IDE (if it does not exist).
//// On the right hand configuration panel, under the module include the block chain address.
//// Ensure the public address given exists in the Start method below - local example was used to test this code with Gnache.

//public class AccountModule : MonoBehaviour
//{

//    public string server;

//    void Start()
//    {
//        // At the start of the script we are going to call getAccountBalance()
//        // with the address we want to check, and a callback to know when the request has finished.

//        StartCoroutine(getAccountBalance("0x179496CeA107ee91e8738724CE2931924A65E4eD", (balance) => {
//            // When the callback is called, we are just going print the balance of the account
//            Debug.Log(balance);
//        }));

//        // Here we call CreateAccount() and we send it a password to encrypt the new account
//        // (for now are going to use "strong_password") and a callback
//        CreateAccount("strong_password", (address, encryptedJson) => {
//            // We just print the address and the encrypted json we just created
//            Debug.Log(address);
//            Debug.Log(encryptedJson);

//            // Then we check the balance like before but in this case using the new account
//            StartCoroutine(getAccountBalance(address, (balance) => {
//                Debug.Log(balance);
//            }));

//        });


//    }

//    // We create the function which will check the balance of the address and return a callback with a decimal variable
//    public static IEnumerator getAccountBalance(string address, System.Action<decimal> callback)
//    {
//        // Now we define a new EthGetBalanceUnityRequest and send it the testnet url where we are going to
//        // check the address, in this case "https://kovan.infura.io".
//        // (we get EthGetBalanceUnityRequest from the Netherum lib imported at the start)
//        //var getBalanceRequest = new EthGetBalanceUnityRequest("https://kovan.infura.io");
//        var getBalanceRequest = new EthGetBalanceUnityRequest("HTTP://127.0.0.1:7545");
//        // Then we call the method SendRequest() from the getBalanceRequest we created
//        // with the address and the newest created block.
//        yield return getBalanceRequest.SendRequest(address, Nethereum.RPC.Eth.DTOs.BlockParameter.CreateLatest());

//        // Now we check if the request has an exception
//        if (getBalanceRequest.Exception == null)
//        {
//            // We define balance and assign the value that the getBalanceRequest gave us.
//            var balance = getBalanceRequest.Result.Value;
//            // Finally we execute the callback and we use the Netherum.Util.UnitConversion
//            // to convert the balance from WEI to ETHER (that has 18 decimal places)
//            callback(Nethereum.Util.UnitConversion.Convert.FromWei(balance, 18));
//        }
//        else
//        {
//            // If there was an error we just throw an exception.
//            throw new System.InvalidOperationException("Get balance request failed");
//        }

//    }

//    // This function will just execute a callback after it creates and encrypt a new account
//    public void CreateAccount(string password, System.Action<string, string> callback)
//    {
//        // We use the Nethereum.Signer to generate a new secret key
//        var ecKey = Nethereum.Signer.EthECKey.GenerateKey();

//        // After creating the secret key, we can get the public address and the private key with
//        // ecKey.GetPublicAddress() and ecKey.GetPrivateKeyAsBytes()
//        // (so it return it as bytes to be encrypted)
//        var address = ecKey.GetPublicAddress();
//        var privateKey = ecKey.GetPrivateKeyAsBytes();

//        // Then we define a new KeyStore service
//        var keystoreservice = new Nethereum.KeyStore.KeyStoreService();

//        // And we can proceed to define encryptedJson with EncryptAndGenerateDefaultKeyStoreAsJson(),
//        // and send it the password, the private key and the address to be encrypted.
//        var encryptedJson = keystoreservice.EncryptAndGenerateDefaultKeyStoreAsJson(password, privateKey, address);
//        // Finally we execute the callback and return our public address and the encrypted json.
//        // (you will only be able to decrypt the json with the password used to encrypt it)
//        callback(address, encryptedJson);
//    }
//}