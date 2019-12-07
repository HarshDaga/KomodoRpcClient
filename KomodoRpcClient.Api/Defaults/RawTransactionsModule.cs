using KomodoRpcClient.Api.Types;

namespace KomodoRpcClient.Api.Defaults
{
	public partial class DefaultApi
	{
		public static Module GetRawTransactionsModule ( )
		{
			var txid = String ( "txid", "Transaction ID" );
			var vout = Numeric ( "vout", "Output number" );

			var methods = new[]
			{
				new Method ( "createrawtransaction",
							 "Creates a transaction, spending the given inputs and sending to the given addresses. The method returns a hex-encoded raw transaction",
							 Array ( "transactions", "Input transactions",
									 Object ( txid, vout ) ),
							 Dict ( String ( "address", "Address" ),
									Numeric ( "amount", "COIN amount" ) ) ),
				new Method ( "decoderawtransaction",
							 "Returns a json object representing the serialized, hex-encoded transaction",
							 String ( "hex", "Transaction hex string" ) ),
				new Method ( "decodescript",
							 "Decodes a hex-encoded script",
							 String ( "hex", "Hex encoded script" ) ),
				new Method ( "fundrawtransaction",
							 "Adds inputs to a transaction until it has enough in value to meet its out value. This will not modify existing inputs, and will add one change output to the outputs",
							 String ( "hexstring", "Hex string of the raw transaction" ) ),
				new Method ( "getrawtransaction",
							 "Returns the raw transaction data",
							 txid ),
				new Method ( "sendrawtransaction",
							 "Submits raw transaction (serialized, hex-encoded) to local nodes and the network",
							 String ( "hexstring", "Hex string of the raw transaction" ),
							 Bool ( "allowhighfees", "Whether to allow high fees", true ) ),
				new Method ( "signrawtransaction",
							 "Signs inputs for a raw transaction (serialized, hex-encoded)",
							 String ( "hexstring", "Transaction hex string" ),
							 Array ( "prevtxs", "Previous dependent transaction outputs",
									 Object ( txid, vout,
											  String ( "scriptPubKey", "The script key" ),
											  String ( "redeemScript", "Redeem script" ) ),
									 true ),
							 Array ( "privatekeys", "A json array of base58-encoded private keys for signing",
									 String ( "privatekey", "The private key in base58-encoding" ),
									 true ),
							 String ( "sighashtype",
									  "The signature hash type; the following options are available: \"ALL\"", true ) )
			};

			return new Module ( "RawTransactions", methods );
		}
	}
}