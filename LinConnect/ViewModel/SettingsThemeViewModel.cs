using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstFloor.ModernUI.Presentation;

namespace LinConnect.ViewModel
{
    class SettingsThemeViewModel : NotifyPropertyChanged
    {
        private LinkCollection themes = new LinkCollection();
        private Link selectedTheme;
       
        public SettingsThemeViewModel()
        {
            this.themes.Add(new Link {DisplayName = "Dark", Source = AppearanceManager.DarkThemeSource});
            this.themes.Add(new Link {DisplayName = "Light", Source = AppearanceManager.LightThemeSource});


            SyncTheme();

            AppearanceManager.Current.PropertyChanged += OnAppearanceManagerPropertyChanged;
        }


        private void SyncTheme()
        {
            
            // synchronizes the selected viewmodel theme with the actual theme used by the appearance manager.
            this.SelectedTheme = this.themes.FirstOrDefault(l => l.Source.Equals(AppearanceManager.Current.ThemeSource));

            if (selectedTheme == null)
                SelectedTheme = themes[1];

        }

        private void OnAppearanceManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ThemeSource")
            {
                SyncTheme();
            }
        }

        public LinkCollection Themes
        {
            get { return this.themes; }
        }


        public Link SelectedTheme
        {
            get { return this.selectedTheme; }
            set
            {
                if (this.selectedTheme != value)
                {
                    this.selectedTheme = value;
                    OnPropertyChanged("SelectedTheme");

                    // and update the actual theme
                    AppearanceManager.Current.ThemeSource = value.Source;
                }
            }
        }

    }
}
