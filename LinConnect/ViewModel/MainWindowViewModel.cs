using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FirstFloor.ModernUI.Presentation;

namespace LinConnect.ViewModel
{
    class MainWindowViewModel : NotifyPropertyChanged
    {
       // Server server = new Server();
        Utilities _utilities = new Utilities();
        private string _portNumber="";

        private string _ipAddress="";

        private bool? _enabled;

        public MainWindowViewModel()
        {
            PropertyChanged += OnPropertyChanged;
           
        }

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            
        }



        public string IpAddress
        {
            get { return _ipAddress; }
            set
            {
                if (this._ipAddress != value)
                {
                    this._ipAddress = value;
                    OnPropertyChanged("ipAddress");

                }


            }
        }

        public string PortNumber
        {
            get { return _portNumber; }
            set
            {

                if (this._portNumber != value)
                {
                    this._portNumber = value;
                    OnPropertyChanged("portNumber");

                   
                }
            }
        }



        public bool? Enabled
        {
            get { return _enabled; }
            set
            {
                if (this._enabled != value)
                {
                    _enabled = value;
                }
            }
        }

        
    }
}
