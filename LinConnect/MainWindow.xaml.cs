using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using LinConnect.ViewModel;


namespace LinConnect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        Utilities _utilities = new Utilities();
        readonly NotifyIcon _notify = new NotifyIcon();
        string _data = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string _name = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
      
       
        public MainWindow()
        {
            InitializeComponent();
            
            Closed += MainWindow_Closed;
            DataContext = new MainWindowViewModel();
           
            WindowState = WindowState.Minimized;
            ShowInTaskbar = false;

        
            _notify.Icon = new Icon(AppDomain.CurrentDomain.BaseDirectory +"/images/linconnect.ico");
            
            _notify.Visible = true;
            _notify.MouseClick += notify_MouseClick;
            _notify.Text = @"LinConnect";
            
            
        }



        void MainWindow_Closed(object sender, EventArgs e)
        {

            this.Hide();
            ShowInTaskbar = false;

        }

       

        void notify_MouseClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = WindowState.Normal;
            ShowInTaskbar = true;

        }

        private void ModernWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
             

        }


      
       
        private void ModernWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
                e.Cancel = true;
                this.Hide();
            ShowInTaskbar = false;
        }

        private void ModernWindow_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.Hide();
            }
        }


    }
}
