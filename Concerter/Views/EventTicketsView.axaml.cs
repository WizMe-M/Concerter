using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Concerter.Views;

public partial class EventTicketsView : UserControl
{
    public EventTicketsView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}