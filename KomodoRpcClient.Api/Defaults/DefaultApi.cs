using KomodoRpcClient.Api.Types;

namespace KomodoRpcClient.Api.Defaults
{
	public partial class DefaultApi
	{
		public static KomodoApi GetDefaultApi ( )
		{
			var modules = new[]
			{
				GetDefaultAddressModule ( ),
				GetDefaultBlockchainModule ( ),
				GetDefaultControlModule ( ),
				GetGenerateModule ( ),
				GetMiningModule ( ),
				GetNetworkModule ( ),
				GetRawTransactionsModule ( )
			};

			return new KomodoApi ( modules );
		}
	}
}