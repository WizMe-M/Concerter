using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Concerter.Views;

public partial class EventView : UserControl
{
    public EventView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}