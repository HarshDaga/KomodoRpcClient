using System.Collections.Generic;
using System.IO;
using KomodoRpcClient.Api.Defaults;
using KomodoRpcClient.Api.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KomodoRpcClient.Api
{
	public class KomodoApiFile
	{
		public string    FileName { get; set; }
		public KomodoApi Api      { get; private set; }

		public JsonSerializerSettings SerializerSettings { get; }

		public KomodoApiFile ( string fileName )
		{
			FileName = fileName;
			Api      = DefaultApi.GetDefaultApi ( );

			SerializerSettings = new JsonSerializerSettings
			{
				Formatting       = Formatting.Indented,
				TypeNameHandling = TypeNameHandling.Auto,
				Converters       = new List<JsonConverter> {new StringEnumConverter ( )}
			};
		}

		public void Save ( )
		{
			var json = JsonConvert.SerializeObject ( Api, SerializerSettings );
			File.WriteAllText ( FileName, json );
		}

		public bool Load ( )
		{
			if ( !File.Exists ( FileName ) )
				return false;

			var json = File.ReadAllText ( FileName );
			Api = JsonConvert.DeserializeObject<KomodoApi> ( json, SerializerSettings );
			return true;
		}
	}
}