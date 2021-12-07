using Controller;
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
            if (_DatabaseNewContext.InputHost.Length == 0 ||
                _DatabaseNewContext.InputPort.Length == 0 ||
                _DatabaseNewContext.InputUsername.Length == 0 ||
                _DatabaseNewContext.InputPassword.Length == 0)
                return false;

            return true;
        }

        private async void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateFields())
                return;

            progressConnection.Visibility = Visibility.Visible;

            SqlConnectionStringBuilder builder = new();
            builder.DataSource = $"{_DatabaseNewContext.InputHost},{_DatabaseNewContext.InputPort}";
            builder.UserID = _DatabaseNewContext.InputUsername;
            builder.Password = _DatabaseNewContext.InputPassword;
            builder.Encrypt = false;
            builder.ConnectTimeout = 10;
            
            try
            {
                await DataAccess.CheckConnectionFor(builder.ConnectionString);
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

        }

        private void PortTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _numberRegex.IsMatch(e.Text);
        }
    }
}
