using _7DaysOfCode_Pokemon.Models;

namespace _7DaysOfCode_Pokemon.FrontEnd;
internal class MenuWrapper : User
{
    public string? Name { get; set; }

    public MenuWrapper(string? name)
    {
        Name = name;
    }

    public virtual void Start()
    {
        Console.Clear();
        TitleOfApplication.Show();
        const int totalWidth = 108;

        if (string.IsNullOrWhiteSpace(Name))
        {
            Name = "----";
        }

        int padding = (totalWidth - Name.Length) / 2;

        if (padding < 0) padding = 0;

        string title = new string('-', padding) + Name + new string('-', totalWidth - padding - Name.Length);

        Console.WriteLine(title);
    }
}