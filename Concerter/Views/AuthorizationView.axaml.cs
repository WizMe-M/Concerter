using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Concerter.Views;

public partial class AuthorizationView : UserControl
{
    public AuthorizationView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void AuthorizationButton_OnClick(object? sender, RoutedEventArgs e)
    {
        
    }
}