using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using LinConnect.ViewModel;

namespace LinConnect
{
    class Utilities
    {

        private static String _oldTitle="";
        private static String _oldDescriptioon = "";

        readonly GrowlNotifiactions _growlNotifications = new GrowlNotifiactions();
        private const double TopOffset = 20;
        private const double LeftOffset = 380;

        public Utilities()
        {
            _growlNotifications.Top = SystemParameters.WorkArea.Top + TopOffset;
            _growlNotifications.Left = SystemParameters.WorkArea.Left + SystemParameters.WorkArea.Width - LeftOffset;
        }

        public  IPAddress GetIp()
        {

            var re = new Regex(@"(?<=Wireless)(.*?)");

            //Match contentTypeMatch = re.Match(content);
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in interfaces)
            {
                var ipProps = adapter.GetIPProperties();

                foreach (var ip in ipProps.UnicastAddresses)
                {
                    if ((adapter.OperationalStatus == OperationalStatus.Up)
                        && (ip.Address.AddressFamily == AddressFamily.InterNetwork))
                    {
                        Match wirelessMatch = re.Match(adapter.Name);
                        if (wirelessMatch.Success)
                        {
                            Console.Out.WriteLine(ip.Address.ToString() + "|" + adapter.Name);
                            return ip.Address;
                        }
                    }
                }
            }
            return null;
        }



        public String DecodeString(String encoded)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(encoded));
        }




        public void Notify(String title, String image, String description)
        {
            // var data = ((MainWindowViewModel) CurrentWindow().DataContext);
            MainWindowViewModel data = null; // = ((MainWindowViewModel) Application.Current.MainWindow.DataContext);

            System.Windows.Application.Current.Dispatcher.Invoke(
                System.Windows.Threading.DispatcherPriority.Normal, (Action) (()
                    =>
                    data = ((MainWindowViewModel) Application.Current.MainWindow.DataContext)
                    ));


            if (_oldTitle != title || _oldDescriptioon != description)
            {
                if ((bool) data.Enabled)
                {
                    if (image == null)
                        image = "pack://application:,,,/Resources/notification-icon.png";
                    System.Windows.Application.Current.Dispatcher.Invoke(
                        System.Windows.Threading.DispatcherPriority.Normal, (Action) (()
                            =>
                            _growlNotifications.AddNotification(new Notification
                            {
                                Title = title,
                                ImageUrl = image,
                                Message = description
                            }))
                        );
                }

        }
            _oldTitle = title;
            _oldDescriptioon = description;

        }


        public Window CurrentWindow()
        {
            Window win = null;
             System.Windows.Application.Current.Dispatcher.Invoke(
                    System.Windows.Threading.DispatcherPriority.Normal, (Action) (()
                        =>
            win= Application.Current.MainWindow
            ));

            return win;
            //  return Application.Current.Windows.Cast<Window>().Where(wi => wi.Title == "LinConnect").First();
        }


    }
}
