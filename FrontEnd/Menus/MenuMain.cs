namespace _7DaysOfCode_Pokemon.FrontEnd.Menus;
internal class MenuMain : MenuWrapper
{
    public MenuMain() : base(" MENU ") { }
    public override void Start()
    {
        base.Start();
        Console.WriteLine($"{UserName} você deseja:");
        Console.WriteLine("1 - Adotar um mascote virtual");
        Console.WriteLine("2 - Ver seus mascotes");
        Console.WriteLine("3 - Sair");

        switch (Console.ReadLine())
        {
            case "1":
                UserMenuChoice = 4;
                break;
            case "2":
                UserMenuChoice = 3;
                break;
            case "3":
                UserMenuChoice = 0;
                Environment.Exit(0);
                break;
            default:
                Console.Clear();
                Console.WriteLine("Opção inválida");
                UserMenuChoice = 1;
                break;
        }
    }
}