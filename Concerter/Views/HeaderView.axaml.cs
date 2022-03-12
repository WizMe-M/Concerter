using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Concerter.Views;

public partial class HeaderView : UserControl
{
    public HeaderView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}