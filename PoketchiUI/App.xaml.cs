using System.Configuration;
using System.Data;
using System.Windows;

namespace PoketchiUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.DispatcherUnhandledException += (sender, args) =>
            {
                MessageBox.Show(args.Exception.ToString(), "Erro");
                args.Handled = true;
            };
        }
    }

}
