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
            Window = this;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        public static MainWindow Window { get; private set; } = null!;
    }
}