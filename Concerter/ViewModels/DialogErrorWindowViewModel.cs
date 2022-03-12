namespace Concerter.ViewModels;

public class DialogErrorWindowViewModel
{
    public DialogErrorWindowViewModel(string main, string additional)
    {
        Name = main;
        Rule = additional;
    }

    public string Name { get; set; }
    public string Rule { get; set; }
}