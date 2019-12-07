using Newtonsoft.Json;
using Newtonsoft.Json.Schema;

namespace KomodoRpcClient.Api.Types.MethodParams
{
	public class BoolParam : IMethodParam
	{
		[JsonConstructor]
		public BoolParam ( string name, string description, ParamType type )
		{
			Name        = name;
			Description = description;
			Type        = type | ParamType.Bool;
		}

		public string    Name        { get; }
		public string    Description { get; }
		public ParamType Type        { get; }

		public JSchema GetJsonSchema ( )
		{
			var schema = new JSchema {Type = JSchemaType.Boolean};
			if ( !string.IsNullOrWhiteSpace ( Name ) )
				schema.Title = Name;
			if ( !string.IsNullOrWhiteSpace ( Description ) )
				schema.Description = Description;

			return schema;
		}
	}
}