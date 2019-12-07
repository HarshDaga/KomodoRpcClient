using System.Collections.Immutable;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;

namespace KomodoRpcClient.Api.Types.MethodParams
{
	public class ObjectParam : IMethodParam
	{
		public ImmutableList<IMethodParam> Properties { get; }

		[JsonConstructor]
		public ObjectParam ( string name, string description, ParamType type, ImmutableList<IMethodParam> properties )
		{
			Name        = name;
			Description = description;
			Type        = type | ParamType.Object;
			Properties  = properties;
		}

		public string    Name        { get; }
		public string    Description { get; }
		public ParamType Type        { get; }

		public JSchema GetJsonSchema ( )
		{
			var schema =
				new JSchema {Type = JSchemaType.Object};
			if ( !string.IsNullOrWhiteSpace ( Name ) )
				schema.Title = Name;
			if ( !string.IsNullOrWhiteSpace ( Description ) )
				schema.Description = Description;

			foreach ( var property in Properties )
			{
				schema.Properties[property.Name] = property.GetJsonSchema ( );
				if ( !property.Type.HasFlag ( ParamType.Optional ) )
					schema.Required.Add ( property.Name );
			}

			return schema;
		}
	}
}