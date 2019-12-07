using KomodoRpcClient.Api.Types;

namespace KomodoRpcClient.Api.Defaults
{
	public partial class DefaultApi
	{
		public static Module GetDefaultControlModule ( )
		{
			var methods = new[]
			{
				new Method ( "getinfo",
							 "Returns an object containing various state info" ),
				new Method ( "help",
							 "Lists all commands, or all information for a specified command" ),
				new Method ( "stop",
							 "Instructs the coin daemon to shut down" )
			};

			return new Module ( "Control", methods );
		}
	}
}