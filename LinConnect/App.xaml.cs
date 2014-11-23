using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LinConnect
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Startup+=App_Startup;
        }
        
        void App_Startup(object sender, StartupEventArgs e)
        {
            MainWindow window = new MainWindow();
            this.MainWindow = window;
            window.Show();
        }
    }
}
