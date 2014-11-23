using System;
using System.Drawing;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using FirstFloor.ModernUI.Windows.Controls;
using LinConnect.ViewModel;
using UserControl = System.Windows.Controls.UserControl;

namespace LinConnect
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : UserControl
    {
        
        Utilities _utilities = new Utilities();
        private ModernDialog _dialog;
        private readonly DialogControls _content = new DialogControls();
        readonly MainPageViewModel _dataContext = new MainPageViewModel();
        public MainPage()
        {
            InitializeComponent();
            DataContext = _dataContext;
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
          
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
           
            Application.Current.Shutdown();
        }

        private void TextPort_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
          

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            _dialog = new ModernDialog
            {
                Title = "Change Port",
                Content = _content,

            };

            
           _dialog.Buttons = new Button[] { _dialog.OkButton, _dialog.CloseButton };



            
            _dialog.OkButton.Click += OkButton_Click;
            _dialog.ShowDialog();
            
        }

        void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var portValue = (TextBox)_content.FindName("PortText");
            if (portValue != null)
            {
                try
                {
                    int value = int.Parse(portValue.Text);

                    if (value < 1100 || value > 65535)
                    {
                        throw new Exception();
                    }
                    _dataContext.PortNumber = portValue.Text; 

                }
                catch (Exception ex)
                {
                    ModernDialog.ShowMessage("Port Number must be between 1100-65535", "Wrong Port", MessageBoxButton.OK);

                }
                finally
                {
                    portValue.Text = "";
                }

               
            }
            
        }

        
    }
}
