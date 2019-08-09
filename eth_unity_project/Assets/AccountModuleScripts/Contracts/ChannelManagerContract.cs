using System;
using System.Numerics;
using System.Threading.Tasks;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

public class ChannelManagerContract
{
    public static string abi = @"[
    {
      ""constant"": true,
      ""inputs"": [],
      ""name"": ""totalChannelWei"",
      ""outputs"": [
        {
          ""name"": """",
          ""type"": ""uint256""
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""view"",
      ""type"": ""function"",
      ""signature"": ""0x009e8690""
    },
    {
      ""constant"": true,
      ""inputs"": [],
      ""name"": ""totalChannelToken"",
      ""outputs"": [
        {
          ""name"": """",
          ""type"": ""uint256""
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""view"",
      ""type"": ""function"",
      ""signature"": ""0x32b573e1""
    },
    {
      ""constant"": true,
      ""inputs"": [],
      ""name"": ""hub"",
      ""outputs"": [
        {
          ""name"": """",
          ""type"": ""address""
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""view"",
      ""type"": ""function"",
      ""signature"": ""0x365a86fc""
    },
    {
      ""constant"": true,
      ""inputs"": [
        {
          ""name"": """",
          ""type"": ""address""
        }
      ],
      ""name"": ""channels"",
      ""outputs"": [
        {
          ""name"": ""threadRoot"",
          ""type"": ""bytes32""
        },
        {
          ""name"": ""threadCount"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""exitInitiator"",
          ""type"": ""address""
        },
        {
          ""name"": ""channelClosingTime"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""status"",
          ""type"": ""uint8""
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""view"",
      ""type"": ""function"",
      ""signature"": ""0x7dce34f7""
    },
    {
      ""constant"": true,
      ""inputs"": [],
      ""name"": ""NAME"",
      ""outputs"": [
        {
          ""name"": """",
          ""type"": ""string""
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""view"",
      ""type"": ""function"",
      ""signature"": ""0xa3f4df7e""
    },
    {
      ""constant"": true,
      ""inputs"": [],
      ""name"": ""approvedToken"",
      ""outputs"": [
        {
          ""name"": """",
          ""type"": ""address""
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""view"",
      ""type"": ""function"",
      ""signature"": ""0xbab46259""
    },
    {
      ""constant"": true,
      ""inputs"": [],
      ""name"": ""challengePeriod"",
      ""outputs"": [
        {
          ""name"": """",
          ""type"": ""uint256""
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""view"",
      ""type"": ""function"",
      ""signature"": ""0xf3f480d9""
    },
    {
      ""constant"": true,
      ""inputs"": [],
      ""name"": ""VERSION"",
      ""outputs"": [
        {
          ""name"": """",
          ""type"": ""string""
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""view"",
      ""type"": ""function"",
      ""signature"": ""0xffa1ad74""
    },
    {
      ""inputs"": [
        {
          ""name"": ""_hub"",
          ""type"": ""address""
        },
        {
          ""name"": ""_challengePeriod"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""_tokenAddress"",
          ""type"": ""address""
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""nonpayable"",
      ""type"": ""constructor"",
      ""signature"": ""constructor""
    },
    {
      ""payable"": true,
      ""stateMutability"": ""payable"",
      ""type"": ""fallback""
    },
    {
      ""anonymous"": false,
      ""inputs"": [
        {
          ""indexed"": false,
          ""name"": ""weiAmount"",
          ""type"": ""uint256""
        },
        {
          ""indexed"": false,
          ""name"": ""tokenAmount"",
          ""type"": ""uint256""
        }
      ],
      ""name"": ""DidHubContractWithdraw"",
      ""type"": ""event"",
      ""signature"": ""0x60a3ff34ec09137572f54ff0fde3035ae459c9bebfdb1643a897de83211ebdf0""
    },
    {
      ""anonymous"": false,
      ""inputs"": [
        {
          ""indexed"": true,
          ""name"": ""user"",
          ""type"": ""address""
        },
        {
          ""indexed"": false,
          ""name"": ""senderIdx"",
          ""type"": ""uint256""
        },
        {
          ""indexed"": false,
          ""name"": ""weiBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""indexed"": false,
          ""name"": ""tokenBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""indexed"": false,
          ""name"": ""pendingWeiUpdates"",
          ""type"": ""uint256[4]""
        },
        {
          ""indexed"": false,
          ""name"": ""pendingTokenUpdates"",
          ""type"": ""uint256[4]""
        },
        {
          ""indexed"": false,
          ""name"": ""txCount"",
          ""type"": ""uint256[2]""
        },
        {
          ""indexed"": false,
          ""name"": ""threadRoot"",
          ""type"": ""bytes32""
        },
        {
          ""indexed"": false,
          ""name"": ""threadCount"",
          ""type"": ""uint256""
        }
      ],
      ""name"": ""DidUpdateChannel"",
      ""type"": ""event"",
      ""signature"": ""0xeace9ecdebd30bbfc243bdc30bfa016abfa8f627654b4989da4620271dc77b1c""
    },
    {
      ""anonymous"": false,
      ""inputs"": [
        {
          ""indexed"": true,
          ""name"": ""user"",
          ""type"": ""address""
        },
        {
          ""indexed"": false,
          ""name"": ""senderIdx"",
          ""type"": ""uint256""
        },
        {
          ""indexed"": false,
          ""name"": ""weiBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""indexed"": false,
          ""name"": ""tokenBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""indexed"": false,
          ""name"": ""txCount"",
          ""type"": ""uint256[2]""
        },
        {
          ""indexed"": false,
          ""name"": ""threadRoot"",
          ""type"": ""bytes32""
        },
        {
          ""indexed"": false,
          ""name"": ""threadCount"",
          ""type"": ""uint256""
        }
      ],
      ""name"": ""DidStartExitChannel"",
      ""type"": ""event"",
      ""signature"": ""0x6e65112e059a868cb1c7c4aed27e34fbbe470d2df0cbaa09bb5f82e5cba029fa""
    },
    {
      ""anonymous"": false,
      ""inputs"": [
        {
          ""indexed"": true,
          ""name"": ""user"",
          ""type"": ""address""
        },
        {
          ""indexed"": false,
          ""name"": ""senderIdx"",
          ""type"": ""uint256""
        },
        {
          ""indexed"": false,
          ""name"": ""weiBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""indexed"": false,
          ""name"": ""tokenBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""indexed"": false,
          ""name"": ""txCount"",
          ""type"": ""uint256[2]""
        },
        {
          ""indexed"": false,
          ""name"": ""threadRoot"",
          ""type"": ""bytes32""
        },
        {
          ""indexed"": false,
          ""name"": ""threadCount"",
          ""type"": ""uint256""
        }
      ],
      ""name"": ""DidEmptyChannel"",
      ""type"": ""event"",
      ""signature"": ""0xff678da893f9e68225fd9be0e51123341ba6d50fe0df41edebef4e9c0d242f77""
    },
    {
      ""anonymous"": false,
      ""inputs"": [
        {
          ""indexed"": false,
          ""name"": ""user"",
          ""type"": ""address""
        },
        {
          ""indexed"": true,
          ""name"": ""sender"",
          ""type"": ""address""
        },
        {
          ""indexed"": true,
          ""name"": ""receiver"",
          ""type"": ""address""
        },
        {
          ""indexed"": false,
          ""name"": ""threadId"",
          ""type"": ""uint256""
        },
        {
          ""indexed"": false,
          ""name"": ""senderAddress"",
          ""type"": ""address""
        },
        {
          ""indexed"": false,
          ""name"": ""weiBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""indexed"": false,
          ""name"": ""tokenBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""indexed"": false,
          ""name"": ""txCount"",
          ""type"": ""uint256""
        }
      ],
      ""name"": ""DidStartExitThread"",
      ""type"": ""event"",
      ""signature"": ""0xdbf69f39706ae3cb4e5b9dbca5780e14ba4968cdd060d5c3268f335ad6c25761""
    },
    {
      ""anonymous"": false,
      ""inputs"": [
        {
          ""indexed"": true,
          ""name"": ""sender"",
          ""type"": ""address""
        },
        {
          ""indexed"": true,
          ""name"": ""receiver"",
          ""type"": ""address""
        },
        {
          ""indexed"": false,
          ""name"": ""threadId"",
          ""type"": ""uint256""
        },
        {
          ""indexed"": false,
          ""name"": ""senderAddress"",
          ""type"": ""address""
        },
        {
          ""indexed"": false,
          ""name"": ""weiBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""indexed"": false,
          ""name"": ""tokenBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""indexed"": false,
          ""name"": ""txCount"",
          ""type"": ""uint256""
        }
      ],
      ""name"": ""DidChallengeThread"",
      ""type"": ""event"",
      ""signature"": ""0x738f3bb8a8a2b4d0dc29a4076d3a4e41e510cd1044877421546903039766ad19""
    },
    {
      ""anonymous"": false,
      ""inputs"": [
        {
          ""indexed"": false,
          ""name"": ""user"",
          ""type"": ""address""
        },
        {
          ""indexed"": true,
          ""name"": ""sender"",
          ""type"": ""address""
        },
        {
          ""indexed"": true,
          ""name"": ""receiver"",
          ""type"": ""address""
        },
        {
          ""indexed"": false,
          ""name"": ""threadId"",
          ""type"": ""uint256""
        },
        {
          ""indexed"": false,
          ""name"": ""senderAddress"",
          ""type"": ""address""
        },
        {
          ""indexed"": false,
          ""name"": ""channelWeiBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""indexed"": false,
          ""name"": ""channelTokenBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""indexed"": false,
          ""name"": ""channelTxCount"",
          ""type"": ""uint256[2]""
        },
        {
          ""indexed"": false,
          ""name"": ""channelThreadRoot"",
          ""type"": ""bytes32""
        },
        {
          ""indexed"": false,
          ""name"": ""channelThreadCount"",
          ""type"": ""uint256""
        }
      ],
      ""name"": ""DidEmptyThread"",
      ""type"": ""event"",
      ""signature"": ""0xf45587a14ff8928bdd940cbf0564b42320b5e46a8fdecaf8a98a9eab63ab1f96""
    },
    {
      ""anonymous"": false,
      ""inputs"": [
        {
          ""indexed"": true,
          ""name"": ""user"",
          ""type"": ""address""
        },
        {
          ""indexed"": false,
          ""name"": ""senderAddress"",
          ""type"": ""address""
        },
        {
          ""indexed"": false,
          ""name"": ""weiAmount"",
          ""type"": ""uint256""
        },
        {
          ""indexed"": false,
          ""name"": ""tokenAmount"",
          ""type"": ""uint256""
        },
        {
          ""indexed"": false,
          ""name"": ""channelWeiBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""indexed"": false,
          ""name"": ""channelTokenBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""indexed"": false,
          ""name"": ""channelTxCount"",
          ""type"": ""uint256[2]""
        },
        {
          ""indexed"": false,
          ""name"": ""channelThreadRoot"",
          ""type"": ""bytes32""
        },
        {
          ""indexed"": false,
          ""name"": ""channelThreadCount"",
          ""type"": ""uint256""
        }
      ],
      ""name"": ""DidNukeThreads"",
      ""type"": ""event"",
      ""signature"": ""0x02d2d0f262d032138bbd82feccd6d357a4441f394333cfa7d61792f44a70a0ed""
    },
    {
      ""constant"": false,
      ""inputs"": [
        {
          ""name"": ""weiAmount"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""tokenAmount"",
          ""type"": ""uint256""
        }
      ],
      ""name"": ""hubContractWithdraw"",
      ""outputs"": [],
      ""payable"": false,
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function"",
      ""signature"": ""0x01dd7da9""
    },
    {
      ""constant"": true,
      ""inputs"": [],
      ""name"": ""getHubReserveWei"",
      ""outputs"": [
        {
          ""name"": """",
          ""type"": ""uint256""
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""view"",
      ""type"": ""function"",
      ""signature"": ""0xad872d03""
    },
    {
      ""constant"": true,
      ""inputs"": [],
      ""name"": ""getHubReserveTokens"",
      ""outputs"": [
        {
          ""name"": """",
          ""type"": ""uint256""
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""view"",
      ""type"": ""function"",
      ""signature"": ""0x9bcf63cd""
    },
    {
      ""constant"": false,
      ""inputs"": [
        {
          ""name"": ""user"",
          ""type"": ""address""
        },
        {
          ""name"": ""recipient"",
          ""type"": ""address""
        },
        {
          ""name"": ""weiBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""name"": ""tokenBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""name"": ""pendingWeiUpdates"",
          ""type"": ""uint256[4]""
        },
        {
          ""name"": ""pendingTokenUpdates"",
          ""type"": ""uint256[4]""
        },
        {
          ""name"": ""txCount"",
          ""type"": ""uint256[2]""
        },
        {
          ""name"": ""threadRoot"",
          ""type"": ""bytes32""
        },
        {
          ""name"": ""threadCount"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""timeout"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""sigUser"",
          ""type"": ""string""
        }
      ],
      ""name"": ""hubAuthorizedUpdate"",
      ""outputs"": [],
      ""payable"": false,
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function"",
      ""signature"": ""0x686bf460""
    },
    {
      ""constant"": false,
      ""inputs"": [
        {
          ""name"": ""recipient"",
          ""type"": ""address""
        },
        {
          ""name"": ""weiBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""name"": ""tokenBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""name"": ""pendingWeiUpdates"",
          ""type"": ""uint256[4]""
        },
        {
          ""name"": ""pendingTokenUpdates"",
          ""type"": ""uint256[4]""
        },
        {
          ""name"": ""txCount"",
          ""type"": ""uint256[2]""
        },
        {
          ""name"": ""threadRoot"",
          ""type"": ""bytes32""
        },
        {
          ""name"": ""threadCount"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""timeout"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""sigHub"",
          ""type"": ""string""
        }
      ],
      ""name"": ""userAuthorizedUpdate"",
      ""outputs"": [],
      ""payable"": true,
      ""stateMutability"": ""payable"",
      ""type"": ""function"",
      ""signature"": ""0xea682e37""
    },
    {
      ""constant"": false,
      ""inputs"": [
        {
          ""name"": ""user"",
          ""type"": ""address""
        }
      ],
      ""name"": ""startExit"",
      ""outputs"": [],
      ""payable"": false,
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function"",
      ""signature"": ""0x72cc174c""
    },
    {
      ""constant"": false,
      ""inputs"": [
        {
          ""name"": ""user"",
          ""type"": ""address[2]""
        },
        {
          ""name"": ""weiBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""name"": ""tokenBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""name"": ""pendingWeiUpdates"",
          ""type"": ""uint256[4]""
        },
        {
          ""name"": ""pendingTokenUpdates"",
          ""type"": ""uint256[4]""
        },
        {
          ""name"": ""txCount"",
          ""type"": ""uint256[2]""
        },
        {
          ""name"": ""threadRoot"",
          ""type"": ""bytes32""
        },
        {
          ""name"": ""threadCount"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""timeout"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""sigHub"",
          ""type"": ""string""
        },
        {
          ""name"": ""sigUser"",
          ""type"": ""string""
        }
      ],
      ""name"": ""startExitWithUpdate"",
      ""outputs"": [],
      ""payable"": false,
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function"",
      ""signature"": ""0x69f81776""
    },
    {
      ""constant"": false,
      ""inputs"": [
        {
          ""name"": ""user"",
          ""type"": ""address[2]""
        },
        {
          ""name"": ""weiBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""name"": ""tokenBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""name"": ""pendingWeiUpdates"",
          ""type"": ""uint256[4]""
        },
        {
          ""name"": ""pendingTokenUpdates"",
          ""type"": ""uint256[4]""
        },
        {
          ""name"": ""txCount"",
          ""type"": ""uint256[2]""
        },
        {
          ""name"": ""threadRoot"",
          ""type"": ""bytes32""
        },
        {
          ""name"": ""threadCount"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""timeout"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""sigHub"",
          ""type"": ""string""
        },
        {
          ""name"": ""sigUser"",
          ""type"": ""string""
        }
      ],
      ""name"": ""emptyChannelWithChallenge"",
      ""outputs"": [],
      ""payable"": false,
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function"",
      ""signature"": ""0xa1e1fe93""
    },
    {
      ""constant"": false,
      ""inputs"": [
        {
          ""name"": ""user"",
          ""type"": ""address""
        }
      ],
      ""name"": ""emptyChannel"",
      ""outputs"": [],
      ""payable"": false,
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function"",
      ""signature"": ""0x4e2a5c5a""
    },
    {
      ""constant"": false,
      ""inputs"": [
        {
          ""name"": ""user"",
          ""type"": ""address""
        },
        {
          ""name"": ""sender"",
          ""type"": ""address""
        },
        {
          ""name"": ""receiver"",
          ""type"": ""address""
        },
        {
          ""name"": ""threadId"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""weiBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""name"": ""tokenBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""name"": ""proof"",
          ""type"": ""bytes""
        },
        {
          ""name"": ""sig"",
          ""type"": ""string""
        }
      ],
      ""name"": ""startExitThread"",
      ""outputs"": [],
      ""payable"": false,
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function"",
      ""signature"": ""0xc8b2f7d6""
    },
    {
      ""constant"": false,
      ""inputs"": [
        {
          ""name"": ""user"",
          ""type"": ""address""
        },
        {
          ""name"": ""threadMembers"",
          ""type"": ""address[2]""
        },
        {
          ""name"": ""threadId"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""weiBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""name"": ""tokenBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""name"": ""proof"",
          ""type"": ""bytes""
        },
        {
          ""name"": ""sig"",
          ""type"": ""string""
        },
        {
          ""name"": ""updatedWeiBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""name"": ""updatedTokenBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""name"": ""updatedTxCount"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""updateSig"",
          ""type"": ""string""
        }
      ],
      ""name"": ""startExitThreadWithUpdate"",
      ""outputs"": [],
      ""payable"": false,
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function"",
      ""signature"": ""0x0955acd4""
    },
    {
      ""constant"": false,
      ""inputs"": [
        {
          ""name"": ""sender"",
          ""type"": ""address""
        },
        {
          ""name"": ""receiver"",
          ""type"": ""address""
        },
        {
          ""name"": ""threadId"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""weiBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""name"": ""tokenBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""name"": ""txCount"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""sig"",
          ""type"": ""string""
        }
      ],
      ""name"": ""challengeThread"",
      ""outputs"": [],
      ""payable"": false,
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function"",
      ""signature"": ""0x25c29be0""
    },
    {
      ""constant"": false,
      ""inputs"": [
        {
          ""name"": ""user"",
          ""type"": ""address""
        },
        {
          ""name"": ""sender"",
          ""type"": ""address""
        },
        {
          ""name"": ""receiver"",
          ""type"": ""address""
        },
        {
          ""name"": ""threadId"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""weiBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""name"": ""tokenBalances"",
          ""type"": ""uint256[2]""
        },
        {
          ""name"": ""proof"",
          ""type"": ""bytes""
        },
        {
          ""name"": ""sig"",
          ""type"": ""string""
        }
      ],
      ""name"": ""emptyThread"",
      ""outputs"": [],
      ""payable"": false,
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function"",
      ""signature"": ""0xb04993ef""
    },
    {
      ""constant"": false,
      ""inputs"": [
        {
          ""name"": ""user"",
          ""type"": ""address""
        }
      ],
      ""name"": ""nukeThreads"",
      ""outputs"": [],
      ""payable"": false,
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function"",
      ""signature"": ""0x7651a86b""
    },
    {
      ""constant"": true,
      ""inputs"": [
        {
          ""name"": ""user"",
          ""type"": ""address""
        }
      ],
      ""name"": ""getChannelBalances"",
      ""outputs"": [
        {
          ""name"": ""weiHub"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""weiUser"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""weiTotal"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""tokenHub"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""tokenUser"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""tokenTotal"",
          ""type"": ""uint256""
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""view"",
      ""type"": ""function"",
      ""signature"": ""0x45a92009""
    },
    {
      ""constant"": true,
      ""inputs"": [
        {
          ""name"": ""user"",
          ""type"": ""address""
        }
      ],
      ""name"": ""getChannelDetails"",
      ""outputs"": [
        {
          ""name"": ""txCountGlobal"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""txCountChain"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""threadRoot"",
          ""type"": ""bytes32""
        },
        {
          ""name"": ""threadCount"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""exitInitiator"",
          ""type"": ""address""
        },
        {
          ""name"": ""channelClosingTime"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""status"",
          ""type"": ""uint8""
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""view"",
      ""type"": ""function"",
      ""signature"": ""0x74c25c20""
    },
    {
      ""constant"": true,
      ""inputs"": [
        {
          ""name"": ""sender"",
          ""type"": ""address""
        },
        {
          ""name"": ""receiver"",
          ""type"": ""address""
        },
        {
          ""name"": ""threadId"",
          ""type"": ""uint256""
        }
      ],
      ""name"": ""getThread"",
      ""outputs"": [
        {
          ""name"": ""weiSender"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""weiReceiver"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""tokenSender"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""tokenReceiver"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""txCount"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""threadClosingTime"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""emptiedSender"",
          ""type"": ""bool""
        },
        {
          ""name"": ""emptiedReceiver"",
          ""type"": ""bool""
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""view"",
      ""type"": ""function"",
      ""signature"": ""0x5f2f3dfd""
    }
  ]";

    private Web3 web3;
    private Contract contractInstance;
    private string contractAddress;

	public ChannelManagerContract(Nethereum.Web3.Accounts.Account account, string contractAddress, string url)
	{
        web3 = new Web3(account, url);
        this.contractAddress = contractAddress;
	}

    // Return a reference to the instance of this contract at the given address.
    public Contract getContract()
    {
        if (contractInstance == null)
        {
            contractInstance = web3.Eth.GetContract(abi, contractAddress);
        }
        return contractInstance;
    }

    public Function GetUserAuthorizedUpdateFunction()
    {
        return contractInstance.GetFunction("userAuthorizedUpdate");
    }

    public Function GetChannelDetailsFunction()
    {
        return contractInstance.GetFunction("getChannelDetails");
    }

    [Function("userAuthorizedUpdate")]
    public class UserAuthorizedUpdateFunction : FunctionMessage
    {
        [Parameter("address", "recipient")]
        public string Recipient { get; set; }
        [Parameter("uint256[2]", "weiBalances")]
        public BigInteger[] WeiBalances { get; set; } // [hub, user]
        [Parameter("uint256[2]", "tokenBalances")]
        public BigInteger[] TokenBalances { get; set; } // [hub, user]
        [Parameter("uint256[4]", "pendingWeiUpdates")]
        public BigInteger[] PendingWeiUpdates { get; set; } // [hubDeposit, hubWithdrawal, userDeposit, userWithdrawal]
        [Parameter("uint256[4]", "pendingTokenUpdates")]
        public BigInteger[] PendingTokenUpdates { get; set; } // [hubDeposit, hubWithdrawal, userDeposit, userWithdrawal]
        [Parameter("uint256[2]", "txCount")]
        public uint[] TxCount { get; set; } // persisted onchain even when empty
        [Parameter("bytes32", "threadRoot")]
        public string ThreadRoot { get; set; }
        [Parameter("uint256", "threadCount")]
        public uint ThreadCount { get; set; }
        [Parameter("uint256", "timeout")]
        public uint Timeout { get; set; }
        [Parameter("string", "sigHub")]
        public string SigHub { get; set; }
    }
}
