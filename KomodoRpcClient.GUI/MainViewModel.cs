using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using DynamicData.Binding;
using JetBrains.Annotations;
using KomodoRpcClient.Api;
using KomodoRpcClient.Api.Types;
using KomodoRpcClient.Rpc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace KomodoRpcClient.GUI
{
	public class MainViewModel : ReactiveObject
	{
		public static readonly string RpcSettingsFileName = "RpcSettings.json";

		private RpcClient _rpc;
		private JSchema   _schema;

		[Reactive]
		public ObservableCollectionExtended<Module> Modules { get; set; }

		[Reactive]
		public Module SelectedModule { get; [UsedImplicitly] set; }

		[Reactive]
		public ObservableCollectionExtended<Method> Methods { get; set; }

		[Reactive]
		public Method SelectedMethod { get; [UsedImplicitly] set; }

		[Reactive]
		public string JsonSchema { get; set; }

		[Reactive]
		public string Params { get; [UsedImplicitly] set; }

		[Reactive]
		public string Response { get; [UsedImplicitly] set; }

		[Reactive]
		public bool IsValid { get; set; }

		[Reactive]
		public string ErrorMessage { get; set; }

		[Reactive]
		public bool IsBusy { get; set; }

		[Reactive]
		public bool IsIdle { get; set; }

		public ReactiveCommand<Unit, Unit> ExecuteCommand { get; set; }

		public MainViewModel ( )
		{
			var komodoApiFile = new KomodoApiFile ( "KomodoApi.json" );
			komodoApiFile.Load ( );
			komodoApiFile.Save ( );

			var api = komodoApiFile.Api;
			InitRpcClient ( );

			Modules = new ObservableCollectionExtended<Module> ( );
			Modules.AddRange ( api.Modules.Values.OrderBy ( x => x.Name ) );
			Methods        = new ObservableCollectionExtended<Method> ( );
			Params         = "[]";
			ExecuteCommand = ReactiveCommand.CreateFromTask ( ExecuteCallAsync );
			ExecuteCommand.IsExecuting.Subscribe ( x => IsBusy = !x );
			ExecuteCommand.IsExecuting.Subscribe ( x => IsIdle = x );

			this.WhenAnyValue ( vm => vm.SelectedModule )
				.Subscribe ( OnModuleSelected );
			this.WhenAnyValue ( vm => vm.SelectedMethod )
				.Subscribe ( OnMethodSelected );

			this.WhenAnyValue ( x => x.Params, x => x.JsonSchema )
				.Subscribe ( x => IsValid = ParamsValidation ( ) );
		}

		private void InitRpcClient ( )
		{
			var settings = new RpcSettings ( );
			if ( !File.Exists ( RpcSettingsFileName ) )
			{
				File.WriteAllText ( RpcSettingsFileName,
									JsonConvert.SerializeObject ( settings, Formatting.Indented ) );
				ErrorMessage = $"{RpcSettingsFileName} was just created and needs to be filled";
				IsValid      = false;
				return;
			}

			var json = File.ReadAllText ( RpcSettingsFileName );
			settings = JsonConvert.DeserializeObject<RpcSettings> ( json );
			if ( !settings.TryValidate ( out var error ) )
			{
				ErrorMessage = error;
				IsValid      = false;
				return;
			}

			_rpc = new RpcClient ( settings );
		}

		private async Task ExecuteCallAsync ( )
		{
			if ( SelectedMethod == null )
			{
				Response = "No method selected";
				return;
			}

			var callResult = await _rpc.CallAsync ( SelectedMethod.Name, Params )
									   .ConfigureAwait ( true );
			if ( callResult.HasError )
			{
				Response = callResult.Error.Message;
				return;
			}

			try
			{
				var temp = JsonConvert.DeserializeObject ( callResult.Response );
				Response = JsonConvert.SerializeObject ( temp, Formatting.Indented );
			}
			catch
			{
				Response = callResult.Response;
			}
		}

		private bool ParamsValidation ( )
		{
			if ( _rpc is null )
			{
				InitRpcClient ( );
				if ( !IsValid )
					return false;
			}

			if ( _schema is null )
				return true;

			try
			{
				var jArray = JArray.Parse ( Params );
				var result = jArray.IsValid ( _schema, out IList<string> errors );
				ErrorMessage = result ? "Valid JSON" : errors[0];

				return result;
			}
			catch ( Exception e )
			{
				ErrorMessage = e.Message;
				return false;
			}
		}

		private void OnModuleSelected ( Module module )
		{
			Methods.Clear ( );
			if ( module != null )
				Methods.AddRange ( module.Methods.OrderBy ( x => x.Name ) );
		}

		private void OnMethodSelected ( Method method )
		{
			_schema = null;
			if ( method is null )
				return;
			_schema    = method.GetJsonSchema ( );
			JsonSchema = _schema.ToString ( );
		}
	}
}