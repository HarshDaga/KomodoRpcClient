using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using KomodoRpcClient.Api.Types;
using KomodoRpcClient.Api.Types.MethodParams;

namespace KomodoRpcClient.Api.Defaults
{
	[SuppressMessage ( "ReSharper", "UnusedMember.Local" )]
	public partial class DefaultApi
	{
		private static ParamType MakeOptional ( ParamType type, bool isOptional )
		{
			return isOptional ? type | ParamType.Optional : type;
		}

		private static ArrayParam Array ( string name,
										  string description,
										  IMethodParam element,
										  bool isOptional = false )
		{
			return new ArrayParam ( name, description, MakeOptional ( ParamType.Array, isOptional ), element );
		}

		private static ArrayParam Array ( string name,
										  IMethodParam element,
										  bool isOptional = false )
		{
			return Array ( name, "", element, isOptional );
		}

		private static ArrayParam Array ( IMethodParam element,
										  bool isOptional = false )
		{
			return Array ( "", "", element, isOptional );
		}

		private static ObjectParam Object ( string name,
											string description,
											IEnumerable<IMethodParam> properties,
											bool isOptional = false )
		{
			return new ObjectParam ( name, description, MakeOptional ( ParamType.Object, isOptional ),
									 properties.ToImmutableList ( ) );
		}

		private static ObjectParam Object ( string name,
											IEnumerable<IMethodParam> properties,
											bool isOptional = false )
		{
			return Object ( name, "", properties, isOptional );
		}

		private static ObjectParam Object ( IEnumerable<IMethodParam> properties,
											bool isOptional = false )
		{
			return Object ( "", "", properties, isOptional );
		}

		private static ObjectParam Object ( bool isOptional,
											params IMethodParam[] properties )
		{
			return Object ( "", "", properties, isOptional );
		}

		private static ObjectParam Object ( params IMethodParam[] properties )
		{
			return Object ( "", "", properties );
		}

		private static DictParam Dict ( string name,
										string description,
										IMethodParam key,
										IMethodParam value,
										bool isOptional = false )
		{
			return new DictParam ( name, description, MakeOptional ( ParamType.Dict, isOptional ), key, value );
		}

		private static DictParam Dict ( string name,
										IMethodParam key,
										IMethodParam value,
										bool isOptional = false )
		{
			return Dict ( name, "", key, value, isOptional );
		}

		private static DictParam Dict ( IMethodParam key,
										IMethodParam value,
										bool isOptional = false )
		{
			return Dict ( "", "", key, value, isOptional );
		}

		private static StringParam String ( string name, string description = "", bool isOptional = false )
		{
			return new StringParam ( name, description, MakeOptional ( ParamType.String, isOptional ) );
		}

		private static NumericParam Numeric ( string name, string description = "", bool isOptional = false )
		{
			return new NumericParam ( name, description, MakeOptional ( ParamType.Numeric, isOptional ) );
		}

		private static NumericParam NumericString ( string name, string description = "", bool isOptional = false )
		{
			return new NumericParam ( name, description,
									  MakeOptional ( ParamType.Numeric | ParamType.String, isOptional ) );
		}

		private static BoolParam Bool ( string name, string description = "", bool isOptional = false )
		{
			return new BoolParam ( name, description, MakeOptional ( ParamType.Bool, isOptional ) );
		}
	}
}