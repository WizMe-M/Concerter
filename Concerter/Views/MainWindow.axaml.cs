using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Concerter.ViewModels;

namespace Concerter.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            this.AttachDevTools();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}