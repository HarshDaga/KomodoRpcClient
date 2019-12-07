using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Media;
using System.Xml;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using MahApps.Metro.Controls;
using ReactiveUI;

namespace KomodoRpcClient.GUI
{
	// ReSharper disable once RedundantExtendsListEntry
	public partial class MainWindow : MetroWindow, IViewFor<MainViewModel>
	{
		public MainWindow ( )
		{
			InitializeComponent ( );
			ViewModel = new MainViewModel ( );

			SetSyntaxHighlighting ( );

			this.WhenActivated ( disposable =>
			{
				var textChanged = Observable
								  .FromEventPattern ( Params, nameof ( Params.TextChanged ) )
								  .Throttle ( TimeSpan.FromSeconds ( 1 ) )
								  .ObserveOn ( this );

				this.OneWayBind ( ViewModel, vm => vm.Modules, v => v.ModuleName.ItemsSource )
					.DisposeWith ( disposable );
				this.Bind ( ViewModel, vm => vm.SelectedModule, v => v.ModuleName.SelectedItem )
					.DisposeWith ( disposable );
				this.OneWayBind ( ViewModel, vm => vm.Methods, v => v.MethodName.ItemsSource )
					.DisposeWith ( disposable );
				this.Bind ( ViewModel, vm => vm.SelectedMethod, v => v.MethodName.SelectedItem )
					.DisposeWith ( disposable );
				this.Bind ( ViewModel, vm => vm.JsonSchema, v => v.JsonSchema.Text )
					.DisposeWith ( disposable );
				this.Bind ( ViewModel, vm => vm.Params, v => v.Params.Text, textChanged )
					.DisposeWith ( disposable );
				this.Bind ( ViewModel, vm => vm.ErrorMessage, v => v.StatusText.Text )
					.DisposeWith ( disposable );
				this.Bind ( ViewModel, vm => vm.Response, v => v.Response.Text )
					.DisposeWith ( disposable );
				this.OneWayBind ( ViewModel, vm => vm.IsBusy, v => v.ExecuteButton.IsEnabled )
					.DisposeWith ( disposable );
				this.OneWayBind ( ViewModel, vm => vm.IsIdle, v => v.IsBusyRing.IsActive )
					.DisposeWith ( disposable );

				this.BindCommand ( ViewModel, vm => vm.ExecuteCommand, v => v.ExecuteButton )
					.DisposeWith ( disposable );

				ViewModel.WhenAnyValue ( x => x.IsValid )
						 .Subscribe ( OnValidationChanged )
						 .DisposeWith ( disposable );
			} );
		}

		object IViewFor.ViewModel
		{
			get => ViewModel;
			set => ViewModel = (MainViewModel) value;
		}

		public MainViewModel ViewModel { get; set; }

		private void SetSyntaxHighlighting ( )
		{
			var info = Application.GetResourceStream ( new Uri ( "/JsonHighlighter.xml", UriKind.Relative ) );
			if ( info != null )
			{
				using var reader       = new XmlTextReader ( info.Stream );
				var       highlighting = HighlightingLoader.Load ( reader, HighlightingManager.Instance );

				JsonSchema.SyntaxHighlighting = highlighting;
				Params.SyntaxHighlighting     = highlighting;
				Response.SyntaxHighlighting   = highlighting;
			}
		}

		private void OnValidationChanged ( bool isValid )
		{
			StatusBar.Background = new SolidColorBrush ( isValid ? Colors.ForestGreen : Colors.OrangeRed );
		}
	}
}