using System.Diagnostics.CodeAnalysis;
using KomodoRpcClient.Api;
using KomodoRpcClient.Api.Types;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;

namespace KomodoRpcClient.Tests
{
	[SuppressMessage ( "ReSharper", "StringLiteralTypo" )]
	public class ApiParamValidationTests
	{
		private KomodoApi _api;

		[SetUp]
		public void Setup ( )
		{
			var komodoApiFile = new KomodoApiFile ( "KomodoApi.json" );
			komodoApiFile.Save ( );
			komodoApiFile.Load ( );

			_api = komodoApiFile.Api;
		}

		private void TestMethod ( string module, string command, string @params, bool passing = true )
		{
			var method = _api.Modules[module]
							 .FindMethod ( command );
			var schema = method.GetJsonSchema ( );
			var jArray = JArray.Parse ( @params );
			Assert.AreEqual ( jArray.IsValid ( schema ), passing );
		}

		[Test]
		public void TestGetAddressBalance ( )
		{
			TestMethod ( "Address",
						 "getaddressbalance",
						 "[{\"addresses\": [\"RTTg3izdeVnqkTTxjzsPFrdUQexgqCy1qb\"]}]" );
		}

		[Test]
		public void TestGetSnapshot ( )
		{
			TestMethod ( "Address", "getsnapshot", "[]" );
			TestMethod ( "Address", "getsnapshot", "[\"5\"]" );
			TestMethod ( "Address", "getsnapshot", "[\"5\", \"unexpected\"]", false );
		}

		[Test]
		public void TestGetAddedNodeInfo ( )
		{
			TestMethod ( "Network", "getaddednodeinfo", "[true, \"78.47.205.239\"]" );
			TestMethod ( "Network", "getaddednodeinfo", "[\"78.47.205.239\", true]", false );
			TestMethod ( "Network", "getaddednodeinfo", "[true]" );
			TestMethod ( "Network", "getaddednodeinfo", "[]", false );
		}

		[Test]
		public void TestCreateRawTransaction ( )
		{
			TestMethod ( "RawTransactions",
						 "createrawtransaction",
						 "[[{\"txid\":\"9f44dc664882198b14e9a8c466d466efcdd070ccb6f57be8e2884aa11e7b2a30\",\"vout\":0}], {\"RHCXHfXCZQpbUbihNHh5gTwfr7NXmJXmHi\":0.01} ]" );
		}
	}
}