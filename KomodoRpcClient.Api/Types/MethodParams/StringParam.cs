using Newtonsoft.Json;
using Newtonsoft.Json.Schema;

namespace KomodoRpcClient.Api.Types.MethodParams
{
	public class StringParam : IMethodParam
	{
		[JsonConstructor]
		public StringParam ( string name, string description, ParamType type )
		{
			Name        = name;
			Description = description;
			Type        = type | ParamType.String;
		}

		public string    Name        { get; }
		public string    Description { get; }
		public ParamType Type        { get; }

		public JSchema GetJsonSchema ( )
		{
			var schema = new JSchema {Type = JSchemaType.String};
			if ( !string.IsNullOrWhiteSpace ( Name ) )
				schema.Title = Name;
			if ( !string.IsNullOrWhiteSpace ( Description ) )
				schema.Description = Description;

			return schema;
		}
	}
}