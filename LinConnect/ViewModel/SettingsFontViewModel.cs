using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstFloor.ModernUI.Presentation;

namespace LinConnect.ViewModel
{
    class SettingsFontViewModel:NotifyPropertyChanged
    {
        private const string FontSmall = "Small";
        private const string FontLarge = "Large";
        private string selectedFontSize;

        public SettingsFontViewModel()
        {
            this.SelectedFontSize = AppearanceManager.Current.FontSize == FontSize.Large ? FontLarge : FontSmall;

        }
        public string[] FontSizes
        {
            get { return new string[] { FontSmall, FontLarge }; }
        }

        public string SelectedFontSize
        {
            get { return this.selectedFontSize; }
            set
            {
                if (this.selectedFontSize != value)
                {
                    this.selectedFontSize = value;
                    OnPropertyChanged("SelectedFontSize");

                    AppearanceManager.Current.FontSize = value == FontLarge ? FontSize.Large : FontSize.Small;
                }
            }
        }

    }
}
