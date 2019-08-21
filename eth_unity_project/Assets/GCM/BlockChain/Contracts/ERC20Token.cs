using System;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

public class ERC20Token
{
    public static string abi = @"[{
      ""constant"": true,
      ""inputs"": [],
      ""name"": ""name"",
      ""outputs"": [
        {
          ""name"": """",
          ""type"": ""string""
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""view"",
      ""type"": ""function""
    },
    {
      ""constant"": false,
      ""inputs"": [
        {
          ""name"": ""_spender"",
          ""type"": ""address""
        },
        {
          ""name"": ""_value"",
          ""type"": ""uint256""
        }
      ],
      ""name"": ""approve"",
      ""outputs"": [
        {
          ""name"": ""success"",
          ""type"": ""bool""
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function""
    },
    {
      ""constant"": true,
      ""inputs"": [],
      ""name"": ""totalSupply"",
      ""outputs"": [
        {
          ""name"": """",
          ""type"": ""uint256""
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""view"",
      ""type"": ""function""
    },
    {
      ""constant"": false,
      ""inputs"": [
        {
          ""name"": ""_from"",
          ""type"": ""address""
        },
        {
          ""name"": ""_to"",
          ""type"": ""address""
        },
        {
          ""name"": ""_value"",
          ""type"": ""uint256""
        }
      ],
      ""name"": ""transferFrom"",
      ""outputs"": [
        {
          ""name"": ""success"",
          ""type"": ""bool""
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function""
    },
    {
      ""constant"": true,
      ""inputs"": [],
      ""name"": ""decimals"",
      ""outputs"": [
        {
          ""name"": """",
          ""type"": ""uint8""
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""view"",
      ""type"": ""function""
    },
    {
      ""constant"": true,
      ""inputs"": [],
      ""name"": ""version"",
      ""outputs"": [
        {
          ""name"": """",
          ""type"": ""string""
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""view"",
      ""type"": ""function""
    },
    {
      ""constant"": true,
      ""inputs"": [
        {
          ""name"": ""_owner"",
          ""type"": ""address""
        }
      ],
      ""name"": ""balanceOf"",
      ""outputs"": [
        {
          ""name"": ""balance"",
          ""type"": ""uint256""
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""view"",
      ""type"": ""function""
    },
    {
      ""constant"": true,
      ""inputs"": [],
      ""name"": ""symbol"",
      ""outputs"": [
        {
          ""name"": """",
          ""type"": ""string""
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""view"",
      ""type"": ""function""
    },
    {
      ""constant"": false,
      ""inputs"": [
        {
          ""name"": ""_to"",
          ""type"": ""address""
        },
        {
          ""name"": ""_value"",
          ""type"": ""uint256""
        }
      ],
      ""name"": ""transfer"",
      ""outputs"": [
        {
          ""name"": ""success"",
          ""type"": ""bool""
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function""
    },
    {
      ""constant"": true,
      ""inputs"": [
        {
          ""name"": ""_owner"",
          ""type"": ""address""
        },
        {
          ""name"": ""_spender"",
          ""type"": ""address""
        }
      ],
      ""name"": ""allowance"",
      ""outputs"": [
        {
          ""name"": ""remaining"",
          ""type"": ""uint256""
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""view"",
      ""type"": ""function""
    },
    {
      ""inputs"": [
        {
          ""name"": ""_initialAmount"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""_tokenName"",
          ""type"": ""string""
        },
        {
          ""name"": ""_decimalUnits"",
          ""type"": ""uint8""
        },
        {
          ""name"": ""_tokenSymbol"",
          ""type"": ""string""
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""nonpayable"",
      ""type"": ""constructor""
    },
    {
      ""payable"": false,
      ""stateMutability"": ""nonpayable"",
      ""type"": ""fallback""
    },
    {
      ""anonymous"": false,
      ""inputs"": [
        {
          ""indexed"": true,
          ""name"": ""_from"",
          ""type"": ""address""
        },
        {
          ""indexed"": true,
          ""name"": ""_to"",
          ""type"": ""address""
        },
        {
          ""indexed"": false,
          ""name"": ""_value"",
          ""type"": ""uint256""
        }
      ],
      ""name"": ""Transfer"",
      ""type"": ""event""
    },
    {
      ""anonymous"": false,
      ""inputs"": [
        {
          ""indexed"": true,
          ""name"": ""_owner"",
          ""type"": ""address""
        },
        {
          ""indexed"": true,
          ""name"": ""_spender"",
          ""type"": ""address""
        },
        {
          ""indexed"": false,
          ""name"": ""_value"",
          ""type"": ""uint256""
        }
      ],
      ""name"": ""Approval"",
      ""type"": ""event""
    },
    {
      ""constant"": false,
      ""inputs"": [
        {
          ""name"": ""_spender"",
          ""type"": ""address""
        },
        {
          ""name"": ""_value"",
          ""type"": ""uint256""
        },
        {
          ""name"": ""_extraData"",
          ""type"": ""bytes""
        }
      ],
      ""name"": ""approveAndCall"",
      ""outputs"": [
        {
          ""name"": ""success"",
          ""type"": ""bool""
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function""
    }
  ]";

    private Web3 web3;
    private Contract contractInstance;
    private string contractAddress;

    public ERC20Token(Nethereum.Web3.Accounts.Account account, string contractAddress, string url)
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

    public Function ApproveFunction()
    {
        return contractInstance.GetFunction("approve");
    }

}
