using Newtonsoft.Json;
using Newtonsoft.Json.Schema;

namespace KomodoRpcClient.Api.Types.MethodParams
{
	public class DictParam : IMethodParam
	{
		public IMethodParam Key   { get; }
		public IMethodParam Value { get; }

		[JsonConstructor]
		public DictParam ( string name, string description, ParamType type, IMethodParam key, IMethodParam value )
		{
			Name        = name;
			Description = description;
			Type        = type | ParamType.Array;
			Key         = key;
			Value       = value;
		}

		public string    Name        { get; }
		public string    Description { get; }
		public ParamType Type        { get; }

		public JSchema GetJsonSchema ( )
		{
			var schema = new JSchema
			{
				Type                 = JSchemaType.Object,
				AdditionalProperties = Value.GetJsonSchema ( )
			};
			if ( !string.IsNullOrWhiteSpace ( Name ) )
				schema.Title = Name;
			if ( !string.IsNullOrWhiteSpace ( Description ) )
				schema.Description = Description;

			return schema;
		}
	}
}