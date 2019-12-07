using System;

namespace KomodoRpcClient.Api.Types
{
	[Flags]
	public enum ParamType
	{
		Optional = 1,
		String   = 1 << 1,
		Numeric  = 1 << 2,
		Bool     = 1 << 3,
		Array    = 1 << 4,
		Object   = 1 << 5,
		Dict     = 1 << 6
	}
}