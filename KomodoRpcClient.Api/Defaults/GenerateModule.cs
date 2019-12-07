using KomodoRpcClient.Api.Types;

namespace KomodoRpcClient.Api.Defaults
{
	public partial class DefaultApi
	{
		public static Module GetGenerateModule ( )
		{
			var methods = new[]
			{
				new Method ( "generate",
							 "Instructs the coin daemon to immediately mine the indicated number of blocks" ),
				new Method ( "getgenerate",
							 "Returns a boolean value indicating the server's mining status" ),
				new Method ( "setgenerate",
							 "Sets the generate property in the coin daemon to true or false",
							 Bool ( "generate", "Set to true to turn on generation" ),
							 Numeric ( "genproclimit",
									   "set the processor limit for when generation is on; use value  -1 for unlimited",
									   true ) )
			};

			return new Module ( "Generate", methods );
		}
	}
}