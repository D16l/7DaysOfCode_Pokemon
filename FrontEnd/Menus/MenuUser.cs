namespace _7DaysOfCode_Pokemon.FrontEnd.Menus;
internal class MenuUser : MenuWrapper
{
    public MenuUser() : base(" BEM VINDO(A) "){}
    public override void Start()
    {
        base.Start();
        while (UserName == null)
        {
            Console.Clear();
            TitleOfApplication.Show();
            Console.WriteLine("Qual é seu nome?");
            UserName = Console.ReadLine();
        }
        UserMenuChoice = 1;
    }
}