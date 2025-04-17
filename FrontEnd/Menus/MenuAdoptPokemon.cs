namespace _7DaysOfCode_Pokemon.FrontEnd.Menus;
internal class MenuAdoptPokemon : MenuWrapper
{
    public MenuAdoptPokemon() : base(" ADOÇÃO ") { }

    public override void Start()
    {
        base.Start();
        Console.WriteLine($"{UserName}, mascote adotado com sucesso, o ovo está chocando:");
        Console.WriteLine(@"
        ▓▓▓▓▓▓       
     ▓▓░░░░░░░░▓▓     
  ▓▓▒░░░░░░░░░░░░▒▓▓  
 ▓▒░░░░░░░░░░░░░░░░▒▓ 
▓▒▒▒▒▒░░░░░░░░░░░░░░▒▓
▓▒▒▒▒▒░░░░░▒▒▒░░░░░░░▓
▓░░▒▒░░░░░░░░░░░▒▒▒░░▓
█▒░░░░░░░░░░░░░▒▒▒▒▒▒█
█▓░░░░░░░░░░░░░▒▒▒▒▒▓█
 █▓▒▒▒▒▒░░░░░░▒▒▒▒▒▓█ 
   █▓▒▒▒▒▒▒▒▒▒▒▒▒▓█   
      ██████████");
        Console.ReadKey();
        UserMenuChoice = 1;
    }
}