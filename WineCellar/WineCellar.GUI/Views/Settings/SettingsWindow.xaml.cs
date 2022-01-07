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
using System.Windows.Shapes;

namespace WineCellar.Views.Settings
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private CountrySettingsControl _CountrySettings { get; set; }
        private TypeSettingsControl _TypeSettings { get; set; }

        public SettingsWindow()
        {
            InitializeComponent();

            // Create the different menus
            _CountrySettings = new();
            _TypeSettings = new();

            // Set a default menu to be opened
            ContentHolder.Content = _CountrySettings;
        }

        private void Button_Country_Click(object sender, RoutedEventArgs e)
        {
            ContentHolder.Content = _CountrySettings;
        }

        private void Button_Type_Click(object sender, RoutedEventArgs e)
        {
            ContentHolder.Content = _TypeSettings;
        }
    }
}
