using System.Collections.Immutable;
using System.Linq;
using KomodoRpcClient.Api.Types.MethodParams;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;

namespace KomodoRpcClient.Api.Types
{
	public class Method
	{
		public string                      Name        { get; }
		public string                      Description { get; }
		public ImmutableList<IMethodParam> Params      { get; }

		[JsonConstructor]
		public Method ( string name, string description, ImmutableList<IMethodParam> @params )
		{
			Name        = name;
			Description = description;
			Params      = @params;
		}

		public Method ( string name, string description, params IMethodParam[] @params )
		{
			Name        = name;
			Description = description;
			Params      = @params.ToImmutableList ( );
		}

		public JSchema GetJsonSchema ( )
		{
			var min = Params.Count ( x => !x.Type.HasFlag ( ParamType.Optional ) );
			var schema = new JSchema
			{
				Type                    = JSchemaType.Array,
				ItemsPositionValidation = true,
				MinimumItems            = min,
				MaximumItems            = Params.Count
			};

			if ( !string.IsNullOrWhiteSpace ( Name ) )
				schema.Title = Name;
			if ( !string.IsNullOrWhiteSpace ( Description ) )
				schema.Description = Description;

			foreach ( var param in Params )
				schema.Items.Add ( param.GetJsonSchema ( ) );

			return schema;
		}
	}
}