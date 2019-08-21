using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Nethereum.Web3;
using System.Timers;
using System;
using System.Threading.Tasks;

public class GazeCoinEthAPI
{

    public Nethereum.Web3.Accounts.Account Account { get; set; }
    //private string ethNodeUrl = "https://eth-ropsten.alchemyapi.io/jsonrpc/HxEg1dDqvI297deLt3jVNowBPYWWlZLo"; 
    private const string ETH_NODE_URL = "https://rpc.gazecoin.xyz";
    ConnextClient connext;
    private Web3 web3;
    private Balance _balance;
    private decimal ethBalance;
    private bool isReady = false;
    public static HDWallet Wallet { get; set; }

    private Timer l2StateCheckTimer;
    private Timer l1BalanceTimer;
    private const uint MILLISECONDS = 1;
    private const uint SECONDS = 1000 * MILLISECONDS;
    private const uint L2_STATE_CHECK_PERIOD = 10 * SECONDS;
    private const uint L1_BALANCE_CHECK_PERIOD = 30 * SECONDS;

    public BalanceEvent BalanceUpdated = new BalanceEvent(); // Parameters are type, reason, amount, token
    public BoolEvent InitComplete = new BoolEvent();
    public UnityEvent ConnextDisconnected = new UnityEvent();
    public UnityEvent ConnextConnected = new UnityEvent();
    public UnityEvent DepositCompleted = new UnityEvent();
    public UnityEvent WithdrawalCompleted = new UnityEvent();
    public UnityEvent Collateralised = new UnityEvent();
    public UnityEvent SwapCompleted = new UnityEvent();

    /*
     * Initialise the Connext client. Will retrieve the HD wallet from storage, if found. Otherwise a new wallet will be created.
     * Starts periodic tasks to track layer 1 and layer 2 balances and events. 
     */
    public async Task Init()
    {
        web3 = new Web3(ETH_NODE_URL);

        Wallet = new HDWallet();

        Wallet.CreateWallet();
        Account = HDWallet.getAccount();

        connext = new ConnextClient(web3, Account, ETH_NODE_URL);
        try
        {
            await connext.Init();

            StartBalanceMonitor();

            isReady = true;
        }
        catch (Exception ex)
        {
            Debug.Log("Init failed. " + ex.Message);
            isReady = false;
        }
        InitComplete.Invoke(isReady);

    }

    public void Stop()
    {
        l2StateCheckTimer.Stop();
        l1BalanceTimer.Stop();
    }

    public bool IsReady()
    {
        return isReady;
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
        return Account.Address;
    }

    public Balances GetBalances()
    {
        ChannelState state = connext.getChannelState();
        Balances bals = new Balances();
        bals.AddBalance(state.getBalanceEthUser(), "ETH");
        bals.AddBalance(state.getBalanceTokenUser(), "DAI");
        return bals;
    }

    // Returns the balance for the given token abbreviation
    // or 0 if not found
    public decimal GetBalance(string token)
    {
        Balances bals = GetBalances();
        return bals.GetBalance(token);
    }

    /*
     * Request a deposit into the layer 2 token account. 
     * 
     */
    public async Task RequestDeposit(decimal amount)
    {
        await connext.RequestDeposit(Nethereum.Web3.Web3.Convert.ToWei(amount), 0);
        DepositCompleted.Invoke();
    }

    public void RequestCollateral()
    {
        //await connext.RequestCollateral TODO
        Collateralised.Invoke();
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
            await connext.FetchChannelState();
        };
        l2StateCheckTimer.Start();

        // Check account balance on mainnet
        l1BalanceTimer = new Timer(L1_BALANCE_CHECK_PERIOD);
        l1BalanceTimer.Elapsed += (sender, e) => {
            _balance.PeriodicBalanceRequest(
            ETH_NODE_URL,
            Account.Address, (balance) => {
                ethBalance = balance;
                Debug.Log("L1 balance is " + balance);
            }); 
        };
        l1BalanceTimer.Start();
    }

    public class Balances
    {
        public List<TokenBalance> BalanceList { get;  private set; }

        internal Balances()
        {
            BalanceList = new List<TokenBalance>();
        }

        internal void AddBalance(TokenBalance bal)
        {
            BalanceList.Add(bal);
        }

        internal void AddBalance(decimal amount, string token)
        {
            AddBalance(new TokenBalance(amount, token));
        }

        // Return the balance for a given token abbreviation
        public decimal GetBalance(string token)
        {
            foreach (TokenBalance bal in BalanceList)
            {
                if (bal.token.Equals(token)) return bal.amount;
            }
            return 0m;
        }
    }

    public struct TokenBalance
    {
        public decimal amount;
        public string token;

        public TokenBalance(decimal amount, string token)
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
