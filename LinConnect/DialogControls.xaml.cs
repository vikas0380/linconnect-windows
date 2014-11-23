using System.Windows.Controls;
using System.Windows.Input;

namespace LinConnect
{
    /// <summary>
    /// Interaction logic for DailogControls.xaml
    /// </summary>
    public partial class DialogControls : UserControl
    {
        public DialogControls()
        {
            InitializeComponent();
        }

        private void PortText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;

            }
        }
    }
}
