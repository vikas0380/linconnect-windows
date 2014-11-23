using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LinConnect.ViewModel;

namespace LinConnect
{
    /// <summary>
    /// Interaction logic for SettingsFont.xaml
    /// </summary>
    public partial class SettingsFont : UserControl
    {
        public SettingsFont()
        {
            InitializeComponent();
            this.DataContext = new SettingsFontViewModel();
        }
    }
}
