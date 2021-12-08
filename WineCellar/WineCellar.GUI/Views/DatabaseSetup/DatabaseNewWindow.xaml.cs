using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for DatabaseNewWindow.xaml
    /// </summary>
    public partial class DatabaseNewWindow : Window
    {
        private static readonly Regex _numberRegex = new("[^0-9.-]+"); // Only allow numeric values

        private DatabaseNewContext _DatabaseNewContext { get; set; }

        public DatabaseNewWindow()
        {
            InitializeComponent();

            _DatabaseNewContext = new DatabaseNewContext();

            DataContext = _DatabaseNewContext;
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrEmpty(_DatabaseNewContext.InputName) ||
                string.IsNullOrEmpty(_DatabaseNewContext.InputHost) ||
                string.IsNullOrEmpty(_DatabaseNewContext.InputPort) ||
                string.IsNullOrEmpty(_DatabaseNewContext.InputUsername) ||
                string.IsNullOrEmpty(_DatabaseNewContext.InputPassword) ||
                string.IsNullOrEmpty(_DatabaseNewContext.InputDatabase))
                return false;

            return true;
        }

        private async void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateFields())
                return;

            progressConnection.Visibility = Visibility.Visible;

            // Build the ConnectionString based on the provided information
            SqlConnectionStringBuilder builder = new();
            builder.DataSource = $"{_DatabaseNewContext.InputHost},{_DatabaseNewContext.InputPort}";
            builder.UserID = _DatabaseNewContext.InputUsername;
            builder.Password = _DatabaseNewContext.InputPassword;
            builder.InitialCatalog = _DatabaseNewContext.InputDatabase;
            builder.Encrypt = false;
            builder.ConnectTimeout = 10;
            
            try
            {
                // Check if the provided settings actually work
                bool success = await DataAccess.CheckConnectionFor(builder.ConnectionString);
                if (success)
                    btnAddConnection.IsEnabled = true;
                else
                    MessageBox.Show("Couldn't connect to the database!", "Connection failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }

            progressConnection.Visibility = Visibility.Hidden;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Extra check for field validation
            if (!ValidateFields())
                return;

            progressConnection.Visibility = Visibility.Visible;

            // Add the provided settings to the database
            ConfigurationAccess.AddDatabase(new DatabaseInformation(
                _DatabaseNewContext.InputName,
                _DatabaseNewContext.InputHost,
                _DatabaseNewContext.InputPort,
                _DatabaseNewContext.InputUsername,
                _DatabaseNewContext.InputPassword,
                _DatabaseNewContext.InputDatabase));

            progressConnection.Visibility = Visibility.Hidden;

            // Set the result to success and close the application
            DialogResult = true;
            Close();
        }

        private void PortTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _numberRegex.IsMatch(e.Text);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Disable the AddConnection button to force user to verify provided settings again
            btnAddConnection.IsEnabled = false;
        }
    }
}
