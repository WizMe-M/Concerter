using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Concerter.ViewModels;

namespace Concerter.Views;

public partial class DialogErrorWindow : ReactiveWindow<DialogErrorWindowViewModel>
{
    public DialogErrorWindow()
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