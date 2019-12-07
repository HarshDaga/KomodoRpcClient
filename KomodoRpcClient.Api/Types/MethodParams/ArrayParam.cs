using Newtonsoft.Json;
using Newtonsoft.Json.Schema;

namespace KomodoRpcClient.Api.Types.MethodParams
{
	public class ArrayParam : IMethodParam
	{
		public IMethodParam Element { get; }

		[JsonConstructor]
		public ArrayParam ( string name, string description, ParamType type, IMethodParam element )
		{
			Name        = name;
			Description = description;
			Type        = type | ParamType.Array;
			Element     = element;
		}

		public string    Name        { get; }
		public string    Description { get; }
		public ParamType Type        { get; }

		public JSchema GetJsonSchema ( )
		{
			return new JSchema
			{
				Type  = JSchemaType.Array,
				Items = {Element.GetJsonSchema ( )}
			};
		}
	}
}