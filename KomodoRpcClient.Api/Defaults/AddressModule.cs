using KomodoRpcClient.Api.Types;

namespace KomodoRpcClient.Api.Defaults
{
	public partial class DefaultApi
	{
		public static Module GetDefaultAddressModule ( )
		{
			var addresses = Array ( "addresses", "List of addresses",
									String ( "address", "The address" ) );
			var start     = Numeric ( "start", "Start block height", true );
			var end       = Numeric ( "end", "End block height", true );
			var chainInfo = Bool ( "chainInfo", "Include chain info in results", true );

			var methods = new[]
			{
				new Method ( "getaddressbalance",
							 "Returns the confirmed balance for an address, or addresses",
							 Object ( addresses ) ),
				new Method ( "getaddressdeltas",
							 "Returns all confirmed balance changes of an address. The user can optionally limit the response to a given interval of blocks",
							 Object ( addresses, start, end, chainInfo ) ),
				new Method ( "getaddressmempool",
							 "Returns all mempool deltas for an address, or addresses",
							 Object ( addresses ) ),
				new Method ( "getaddresstxids",
							 "Returns the txids for an address, or addresses",
							 Object ( addresses, start, end ) ),
				new Method ( "getaddressutxos",
							 "Returns all unspent outputs for an address",
							 Object ( addresses, chainInfo ) ),
				new Method ( "getsnapshot",
							 "Returns a snapshot of addresses and their amounts at the Smart Chain's current height",
							 NumericString ( "top", "Only return these many addresses", true ) )
			};

			return new Module ( "Address", methods );
		}
	}
}