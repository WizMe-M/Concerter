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
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}