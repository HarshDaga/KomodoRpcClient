using KomodoRpcClient.Api.Types;

namespace KomodoRpcClient.Api.Defaults
{
	public partial class DefaultApi
	{
		public static Module GetMiningModule ( )
		{
			var methods = new[]
			{
				new Method ( "getblocksubsidy",
							 "Returns the block-subsidy reward. The resulting calculation takes into account the mining slow start",
							 Numeric ( "height", "Block Height", true ) ),
				new Method ( "getblocktemplate",
							 "Returns data that is necessary to construct a block",
							 Object ( String ( "mode", "This must be set to \"template\" or omitted", true ),
									  Array ( "capabilities", "List of capabilities",
											  String ( "capability", "Capability" ), true ) ),
							 String ( "support",
									  "Client side supported features: \"longpoll\", \"coinbasetxn\", \"coinbasevalue\", \"proposal\", \"serverlist\", \"workid\"",
									  true ) ),
				new Method ( "getlocalsolps",
							 "Returns the average local solutions per second since this node was started" ),
				new Method ( "getmininginfo",
							 "Returns a json object containing mining-related information" ),
				new Method ( "getnetworksolps",
							 "Returns the estimated network solutions per second based on the last n blocks",
							 Numeric ( "blocks",
									   "The number of blocks; use -1 to calculate according to the relevant difficulty averaging window",
									   true ),
							 Numeric ( "height", "The block height that corresponds to the requested data", true ) ),
				new Method ( "prioritisetransaction",
							 "Instructs the daemon to accept the indicated transaction into mined blocks at a higher (or lower) priority",
							 String ( "transaction_id", "Transaction ID" ),
							 Numeric ( "priority_delta",
									   "The priority to add or subtract (if negative). The transaction selection algorithm assigns the tx a higher or lower priority" ),
							 Numeric ( "fee_delta",
									   "The fee value in satoshis to add or subtract (if negative); the fee is not actually paid, only the algorithm for selecting transactions into a block considers the transaction as if it paid a higher (or lower) fee" ) ),
				new Method ( "submitblock",
							 "Instructs the daemon to propose a new block to the network",
							 String ( "hexdata", "The hex-encoded block data to submit" ) )
			};

			return new Module ( "Mining", methods );
		}
	}
}