using System;
using System.Collections.Generic;
using System.Net;
using System.Security;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KomodoRpcClient.Rpc
{
	public class RpcClient
	{
		private long         _requestId;
		public  Uri          Address        { get; set; }
		public  string       UserName       { get; set; }
		public  SecureString Password       { get; set; }
		public  string       JsonRpcVersion { get; set; } = "1.0";

		[UsedImplicitly]
		public RpcClient ( Uri address, string userName, SecureString password )
		{
			Address  = address;
			UserName = userName;
			Password = password;
			Password.MakeReadOnly ( );
		}

		[UsedImplicitly]
		public RpcClient ( Uri address, string userName, string password )
			: this ( address, userName, ToSecureString ( password ) ) { }

		public RpcClient ( RpcSettings settings )
		{
			Address        = new Uri ( settings.Address );
			UserName       = settings.UserName;
			Password       = ToSecureString ( settings.Password );
			JsonRpcVersion = settings.JsonRpcVersion;
		}

		public async Task<CallResult> CallAsync ( string method, string @params )
		{
			var result = new CallResult ( );
			try
			{
				using var client = new WebClient {Credentials = new NetworkCredential ( UserName, Password )};
				var       body   = CreateRequestBody ( method, @params );

				result.Response = await client.UploadStringTaskAsync ( Address, "POST", body )
											  .ConfigureAwait ( false );
			}
			catch ( Exception e )
			{
				result.Error = e;
			}

			return result;
		}

		public string Call ( string method, string @params, out Exception exception )
		{
			try
			{
				exception = null;
				using var client   = new WebClient {Credentials = new NetworkCredential ( UserName, Password )};
				var       body     = CreateRequestBody ( method, @params );
				var       response = client.UploadString ( Address, "POST", body );
				return response;
			}
			catch ( Exception e )
			{
				exception = e;
			}

			return null;
		}

		public string Call ( string method, string @params )
		{
			return Call ( method, @params, out _ );
		}

		private static SecureString ToSecureString ( string str )
		{
			var secure = new SecureString ( );
			foreach ( var c in str )
				secure.AppendChar ( c );
			secure.MakeReadOnly ( );

			return secure;
		}

		private string CreateRequestBody ( string method, string @params )
		{
			var jArray = JArray.Parse ( @params );
			var requestParams = new Dictionary<string, object>
			{
				["jsonrpc"] = JsonRpcVersion,
				["id"]      = $"{++_requestId}",
				["method"]  = method,
				["params"]  = jArray
			};
			return JsonConvert.SerializeObject ( requestParams, Formatting.None );
		}
	}
}