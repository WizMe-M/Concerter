using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Concerter.Views;

public partial class EventListView : UserControl
{
    public EventListView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}