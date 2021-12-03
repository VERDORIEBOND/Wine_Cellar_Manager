using Controller;
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

        public DatabaseSelectWindow(List<DatabaseInformation> databases)
        {
            InitializeComponent();

            DatabaseSelectContext context = new();
            context.Databases = databases;

            _DatabaseSelectContext = context;
            DataContext = context;
        }

        private void ButtonNewConnection_Click(object sender, RoutedEventArgs e)
        {
            _DatabaseNewWindow = new DatabaseNewWindow();
            _DatabaseNewWindow.ShowDialog();
        }

        private void ButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            if (_DatabaseSelectContext.SelectedDatabase is not null)
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Select a database before proceeding.");
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
    }
}
