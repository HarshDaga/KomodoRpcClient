using Newtonsoft.Json;
using Newtonsoft.Json.Schema;

namespace KomodoRpcClient.Api.Types.MethodParams
{
	public class NumericParam : IMethodParam
	{
		[JsonConstructor]
		public NumericParam ( string name, string description, ParamType type )
		{
			Name        = name;
			Description = description;
			Type        = type | ParamType.Numeric;
		}

		public string    Name        { get; }
		public string    Description { get; }
		public ParamType Type        { get; }

		public JSchema GetJsonSchema ( )
		{
			var type = JSchemaType.Number;
			if ( Type.HasFlag ( ParamType.String ) )
				type |= JSchemaType.String;

			var schema = new JSchema {Type = type};
			if ( !string.IsNullOrWhiteSpace ( Name ) )
				schema.Title = Name;
			if ( !string.IsNullOrWhiteSpace ( Description ) )
				schema.Description = Description;

			return schema;
		}
	}
}