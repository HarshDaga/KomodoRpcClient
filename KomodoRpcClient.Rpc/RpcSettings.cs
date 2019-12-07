using System;
using JetBrains.Annotations;

namespace KomodoRpcClient.Rpc
{
	public class RpcSettings
	{
		public string Address        { get; [UsedImplicitly] set; }
		public string UserName       { get; [UsedImplicitly] set; }
		public string Password       { get; [UsedImplicitly] set; }
		public string JsonRpcVersion { get; set; } = "1.0";

		public bool TryValidate ( out string error )
		{
			error = null;
			if ( !Uri.IsWellFormedUriString ( Address, UriKind.Absolute ) )
				error = $"Invalid Address {Address}";
			else if ( string.IsNullOrWhiteSpace ( UserName ) )
				error = "Invalid UserName";
			else if ( string.IsNullOrWhiteSpace ( Password ) )
				error = "Invalid Password";

			return error == null;
		}
	}
}