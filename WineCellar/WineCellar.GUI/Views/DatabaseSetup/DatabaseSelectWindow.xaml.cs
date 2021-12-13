using Controller;
using Microsoft.Extensions.Configuration;
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using WineCellar.DataContexts;

namespace WineCellar.Views.DatabaseSetup
{
    /// <summary>
    /// Interaction logic for DatabaseSelectWindow.xaml
    /// </summary>
    public partial class DatabaseSelectWindow : Window
    {
        private DatabaseNewWindow _DatabaseNewWindow { get; set; }
        private DatabaseSelectContext _DatabaseSelectContext { get; set; }
        private IConfiguration _Configuration { get; init; }

        public DatabaseSelectWindow(IConfiguration config)
        {
            InitializeComponent();

            _Configuration = config;

            _DatabaseSelectContext = new DatabaseSelectContext();
            DataContext = _DatabaseSelectContext;
        }

        private async void ButtonNewConnection_Click(object sender, RoutedEventArgs e)
        {
            _DatabaseNewWindow = new DatabaseNewWindow();
            bool? success = _DatabaseNewWindow.ShowDialog();
            if (success == true)
            {
                _DatabaseSelectContext.Databases = await ConfigurationAccess.GetDatabasesAsync();
                _DatabaseSelectContext.SelectedDatabase = null;
            }
        }

        private async void ButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            if (_DatabaseSelectContext.SelectedDatabase is not null)
            {
                progressConnect.Visibility = Visibility.Visible;

                bool success = await DataAccess.CheckConnectionFor(_DatabaseSelectContext.SelectedDatabase.ConnectionString);

                progressConnect.Visibility = Visibility.Hidden;

                if (success)
                {
                    DialogResult = true;
                    Close();
                }
                else
                {
                    MessageBox.Show("Couldn't connect to the database!", "Connection failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public DatabaseInformation GetSelectedDatabase()
        {
            return _DatabaseSelectContext.SelectedDatabase;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (DialogResult == false)
                Application.Current.Shutdown();
        }

        private void Database_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnConnect.IsEnabled = _DatabaseSelectContext.SelectedDatabase is not null;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _DatabaseSelectContext.Databases = await ConfigurationAccess.GetDatabasesAsync();
        }
    }
}
