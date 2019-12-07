using System;
using System.Collections.Immutable;
using System.Linq;
using Newtonsoft.Json;

namespace KomodoRpcClient.Api.Types
{
	public class Module
	{
		public string                Name    { get; }
		public ImmutableList<Method> Methods { get; }

		[JsonConstructor]
		public Module ( string name, ImmutableList<Method> methods )
		{
			Name    = name;
			Methods = methods;
		}

		public Module ( string name, params Method[] methods )
		{
			Name    = name;
			Methods = methods.ToImmutableList ( );
		}

		public Method FindMethod ( string name )
		{
			return Methods.FirstOrDefault ( x => x.Name.Equals ( name, StringComparison.OrdinalIgnoreCase ) );
		}
	}
}