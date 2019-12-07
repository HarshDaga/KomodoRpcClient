using KomodoRpcClient.Api.Types;

namespace KomodoRpcClient.Api.Defaults
{
	public partial class DefaultApi
	{
		public static Module GetNetworkModule ( )
		{
			var methods = new[]
			{
				new Method ( "addnode",
							 "Attempts to add or remove a node from the addnode list, or to make a single attempt to connect to a node",
							 String ( "node", "The node" ),
							 String ( "command", "add|remove|onetry" ) ),
				new Method ( "clearbanned",
							 "Clears all banned IPs" ),
				new Method ( "disconnectnode",
							 "Instructs the daemon to immediately disconnect from the specified node",
							 String ( "node", "The node's address" ) ),
				new Method ( "getaddednodeinfo",
							 "Returns information about the given added node, or all added nodes",
							 Bool ( "dns",
									"if false, only a list of added nodes will be provided; otherwise, connection information is also provided" ),
							 String ( "node",
									  "If provided, the method returns information about this specific node; otherwise, all nodes are returned",
									  true ) ),
				new Method ( "getconnectioncount",
							 "Returns the number of connections to other nodes" ),
				new Method ( "getdeprecationinfo",
							 "Returns an object containing current version and deprecation block height" ),
				new Method ( "getnettotals",
							 "Returns information about network traffic, including bytes in, bytes out, and current time" ),
				new Method ( "getnetworkinfo",
							 "Returns an object containing various state info regarding p2p networking" ),
				new Method ( "getpeerinfo",
							 "Returns data about each connected network node as a json array of objects" ),
				new Method ( "listbanned",
							 "Lists all banned IP addresses and subnets" ),
				new Method ( "ping",
							 "Requests that a ping be sent to all other nodes, to measure ping time" ),
				new Method ( "setban",
							 "Attempts to add or remove an IP address (and subnet, if indicated) from the banned list",
							 String (
								 "ip",
								 "The IP/subnet (see getpeerinfo for nodes ip) with an optional netmask (default is /32 = single ip)" ),
							 String ( "command", "add|remove" ),
							 Numeric ( "bantime",
									   "Indicates how long (in seconds) the ip is banned (or until when, if [absolute] is set). 0 or empty means the ban is using the default time of 24h, which can also be overwritten using the -bantime runtime parameter",
									   true ),
							 Bool ( "absolute",
									"If set to true, the bantime must be an absolute timestamp (in seconds) since epoch",
									true ) )
			};

			return new Module ( "Network", methods );
		}
	}
}