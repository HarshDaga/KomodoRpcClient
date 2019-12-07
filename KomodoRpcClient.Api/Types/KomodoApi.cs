using System.Collections.Immutable;
using Newtonsoft.Json;

namespace KomodoRpcClient.Api.Types
{
	public class KomodoApi
	{
		public ImmutableDictionary<string, Module> Modules { get; }

		[JsonConstructor]
		public KomodoApi ( ImmutableDictionary<string, Module> modules )
		{
			Modules = modules;
		}

		public KomodoApi ( params Module[] modules )
		{
			Modules = modules.ToImmutableDictionary ( x => x.Name );
		}
	}
}