using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Concerter.Views;

public partial class ChangePasswordView : UserControl
{
    public ChangePasswordView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}