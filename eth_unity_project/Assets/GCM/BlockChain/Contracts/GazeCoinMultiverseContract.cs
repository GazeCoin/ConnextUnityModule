using System;
using System.Numerics;
using System.Threading.Tasks;
//using Nethereum.ABI.FunctionEncoding.Attributes;
//using Nethereum.Contracts;
//using Nethereum.Contracts.ContractHandlers;
//using Nethereum.Web3;
//using Nethereum.Web3.Accounts;

/*
 * A class for interacting with the GazeCoin ERC721 contract, which stores
 * apartments and their contents.
 * 
 * TODO extend a generic Contract class
 */
public class GazeCoinMultiverseContract
{
    public static string abi = @"[
	...
  ]";

 //   private Web3 web3;
 //   private Contract contractInstance;
 //   private string contractAddress;

	//public GazeCoinMultiverseContract(Nethereum.Web3.Accounts.Account account, string contractAddress, string url)
	//{
 //       web3 = new Web3(account, url);
 //       this.contractAddress = contractAddress;
	//}

 //   // Return a reference to the instance of this contract at the given address.
 //   public Contract Contract
 //   {
 //       get
 //       {
 //           if (contractInstance == null)
 //           {
 //               contractInstance = web3.Eth.GetContract(abi, contractAddress);
 //           }
 //           return contractInstance;
 //       }
 //   }

 //   public IContractTransactionHandler<GetApartmentFunction> GetContractHandler()
 //   {
 //       return web3.Eth.GetContractTransactionHandler<GetApartmentFunction>();
 //   }


 //   [Function("getApartment", "GetApartmentDTO")]
 //   public class GetApartmentFunction : FunctionMessage
 //   {
 //       [Parameter("uint256", "apartmentId")]
 //       public BigInteger ApartmentId { get; set; }
 //   }

 //   [FunctionOutput]
 //   public class GetApartmentDTO : IFunctionOutputDTO
 //   {
 //       [Parameter("uint256", "tokenId")]
 //       public BigInteger TokenId { get; set; }
 //       [Parameter("address", "owner")]
 //       public BigInteger Owner { get; set; }
 //       [Parameter("string", "name")]
 //       public String Name { get; set; }
 //       [Parameter("string", "description")]
 //       public String Description { get; set; }
 //       [Parameter("string", "image")]
 //       public String ImageUri { get; set; }
 //       [Parameter("string", "mediaUri")]
 //       public String MediaUri { get; set; }

 //       [Parameter("Attribute[]", "attributes")]
 //       public AttributeDTO[] Attributes { get; set; }
 //   }

 //   // Attributes of a trackable entity
 //   public class AttributeDTO : IFunctionOutputDTO
 //   {
 //       [Parameter("string","trait_type")]
 //       public String TraitType { get; set; }
 //       [Parameter("string", "value")]
 //       public String Value { get; set; }
 //   }
}
