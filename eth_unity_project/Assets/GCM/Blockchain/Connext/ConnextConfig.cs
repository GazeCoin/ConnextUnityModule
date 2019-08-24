using System;

public class ConnextConfig
{
    public string err;
    public ConfigResponse response;

    public ConnextConfig()
	{
	}

    public class ConfigResponse
    {
        public ContractAddresses contractAddresses;
        public EthNetwork ethNetwork;
        public Messaging messaging;
        public string nodePublicIdentifier;

    }

    public class ContractAddresses
    {
        public string Token;
        public string ChallengeRegistry;
        public string ConditionalTransactionDelegateTarget;
        public string CoinBalanceRefundApp;
        public string MultiAssetMultiPartyCoinTransferInterpreter;
        public string IdentityApp;
        public string MinimumViableMultisig;
        public string ProxyFactory;
        public string SingleAssetTwoPartyCoinTransferInterpreter;
        public string TimeLockedPassThrough;
        public string TwoPartyFixedOutcomeInterpreter;
        public string TwoPartyFixedOutcomeFromVirtualAppInterpreter;
        public string SimpleTwoPartySwapApp;
        public string UnidirectionalTransferApp;
        public string UnidirectionalLinkedTransferApp;
    }

    public class EthNetwork
    {
        public string name;
        public int chainId;
        public string ensAddress;
    }

    public class Messaging
    {
        public string clusterId;
        public int logLevel;
        public string[] messagingUrl;
        public string token;
    }
}
