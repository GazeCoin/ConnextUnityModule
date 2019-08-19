using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Nethereum.Web3;
using System.Timers;
using System;
using System.Threading.Tasks;

public class GazeEthAPI
{

    public Nethereum.Web3.Accounts.Account account { get; set; }
    //private string ethNodeUrl = "https://eth-ropsten.alchemyapi.io/jsonrpc/HxEg1dDqvI297deLt3jVNowBPYWWlZLo"; 
    private const string ETH_NODE_URL = "https://rpc.gazecoin.xyz";
    ConnextClient connext;
    private Web3 web3;

    private Timer l2StateCheckTimer;
    private Timer l1BalanceTimer;
    private const uint MILLISECONDS = 1;
    private const uint SECONDS = 1000 * MILLISECONDS;
    private const uint L2_STATE_CHECK_PERIOD = 10 * SECONDS;
    private const uint L1_BALANCE_CHECK_PERIOD = 30 * SECONDS;

    public BalanceEvent BalanceUpdated; // Parameters are type, reason, amount, token
    public BoolEvent InitComplete;
    public UnityEvent ConnextDisconnected;
    public UnityEvent ConnextConnected;
    public UnityEvent DepositCompleted;
    public UnityEvent WithdrawalCompleted;
    public UnityEvent Collateralised;
    public UnityEvent SwapCompleted;

    /*
     * Initialise the Connext client. Will retrieve the HD wallet from storage, if found. Otherwise a new wallet will be created.
     * Starts periodic tasks to track layer 1 and layer 2 balances and events. 
     */
    public void Init()
    {
        web3 = new Web3(ETH_NODE_URL);

        Wallet = new HDWallet();

        Wallet.CreateWallet();
        account = HDWallet.getAccount();

        connext = new ConnextClient(web3, account, ETH_NODE_URL);
        connext.Init();

        StartBalanceMonitor();

        InitComplete.Invoke(true);
    }

    public void Stop()
    {
        l2StateCheckTimer.Stop();
        l1BalanceTimer.Stop();
    }

    public void ImportPassphrase(string passphrase)
    {

    }

    public String GetPassphrase()
    {
        return ""; //TODO
    }

    /*
     * Returns the ETH address for the default account
     */
    public string GetAddress()
    {
        return account.Address;
    }

    public Balances GetBalances()
    {
        ChannelState state = connext.getChannelState();
        Balances bals = new Balances();
        return bals;
    }

    /*
     * Request a deposit into the layer 2 token account. 
     * 
     */
    public async void RequestDeposit(decimal amount)
    {
        await connext.RequestDeposit(Nethereum.Web3.Web3.Convert.ToWei(amount), 0);
    }

    /*
    public async Task Pay(decimal amount, string token)
    {

    }

    public async Task Swap(decimal amount, string fromToken, string toToken)
    {

    }

    public async Task Withdraw(decimal amount, string fromToken, string toToken, string toAccount)
    {

    }
    */

    public Apartment GetApartment(string id)
    {
        return new Apartment(); // TODO
    }




    void StartBalanceMonitor()
    {
        // Get Connext channel state.
        l2StateCheckTimer = new Timer(L2_STATE_CHECK_PERIOD);
        l2StateCheckTimer.Elapsed += async ( sender, e ) => {
            //await connext.getChannelState();
        };
        l2StateCheckTimer.Start();

        // Check account balance on mainnet
        l1BalanceTimer = new Timer(L1_BALANCE_CHECK_PERIOD);
        l1BalanceTimer.Elapsed += async (sender, e) => {
            /*await _balance.PeriodicBalanceRequest(
            ETH_NODE_URL,
            account.Address, (balance) => {
                ethBalance = balance;
                Debug.Log("L1 balance is " + balance);
            })*/
        };
        l1BalanceTimer.Start();
    }

    public static HDWallet Wallet
    {
        get; set;
    }

    public class Balances
    {
        public List<Balance> BalanceList { get;  private set; }

        internal Balances()
        {
            BalanceList = new List<Balance>();
        }

        internal void AddBalance(Balance bal)
        {
            BalanceList.Add(bal);
        }

        internal void AddBalance(decimal amount, string token)
        {
            AddBalance(new Balance(amount, token));
        }
    }

    public struct Balance
    {
        public decimal amount;
        public string token;

        public Balance(decimal amount, string token)
        {
            this.amount = amount;
            this.token = token;
        }
    }

    /*
     */
    [System.Serializable]
    public class BalanceEvent : UnityEvent<string, string, decimal, string>
    {
    }

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool>
    {
    }

}
