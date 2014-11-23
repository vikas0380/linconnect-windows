using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using FirstFloor.ModernUI.Presentation;

namespace LinConnect.ViewModel
{
    class MainPageViewModel : NotifyPropertyChanged
    {
        readonly Utilities _utilities = new Utilities();
        readonly string _data = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        readonly string _name = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        string _path;
        private MainWindowViewModel _mwvViewModel;
        private IPAddress _currentAddress;
        private string _portNumber;
        private string _ipAddress;
        private bool? _enabled;



        public MainPageViewModel()
        {
            PropertyChanged += OnPropertyChanged;
            SyncIp();
            SyncPort();
            SetValues();

        }

        private void SetValues()
        {
          _mwvViewModel = _utilities.CurrentWindow().DataContext as MainWindowViewModel;
             
            if (_mwvViewModel != null)
            {
                _mwvViewModel.IpAddress = _ipAddress;
                _mwvViewModel.PortNumber = _portNumber;
                _enabled = true;
                _mwvViewModel.Enabled = _enabled;
                
                Server.Instance.StartServer(_ipAddress, _portNumber);
            }
            
        }




        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "portNumber")
            SyncPort();

            if (e.PropertyName == "ipAddress")
            SyncIp();
        }

        private void SyncPort()
        {
            _path = Path.Combine(_data, _name);
            if (Directory.Exists(_path))
            {
               _portNumber=(String)Properties.Settings.Default["Port"];

            }
            else
            {
                // create the directory on first run
               
                AddFireWallException();
                Directory.CreateDirectory(_path);
                Properties.Settings.Default["Port"] = "9090";
                Properties.Settings.Default.Save();
                _portNumber = "9090";
            }
          
        }

        private void SyncIp()
        {
            _currentAddress = _utilities.GetIp();
            _ipAddress = _currentAddress != null ? _currentAddress.ToString() : "Wireless Not Connected";
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

                if (this._portNumber != value&& _portNumber!="")
                {
                    this._portNumber = value;
                    Properties.Settings.Default["Port"] = value;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("portNumber");
                    Server.Instance.RestartServer(_ipAddress,_portNumber);

                }
            }
        }





        public  bool? Enabled
        {
            get { return _enabled; }
            set
            {
                if (this._enabled != value)
                {
                    _enabled = value;
                    _mwvViewModel.Enabled = _enabled;
                }
            }
        }



        private void AddFireWallException()
        {
           // String fullAddress = AppDomain.CurrentDomain.BaseDirectory +System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

            String fullAddress = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName;

            string args = string.Format(@"advfirewall firewall add rule name=\""{0}\"" dir=in action=allow program=\""{1}\"" enable=yes profile=all","LinConnect",fullAddress+".exe");
            
            ProcessStartInfo psi = new ProcessStartInfo("netsh", args)
            {
                Verb = "runas",
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false
            };


            var process = Process.Start(psi);
            if (process != null) process.WaitForExit();
            
        }

    }
}
