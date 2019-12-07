using System;

namespace KomodoRpcClient.Rpc
{
	public class CallResult
	{
		public string    Response { get; set; }
		public Exception Error    { get; set; }

		public bool HasError => Error != null;
	}
}