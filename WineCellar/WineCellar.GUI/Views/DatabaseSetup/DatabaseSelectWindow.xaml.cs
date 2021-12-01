using Controller;
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

namespace WineCellar.Views.DatabaseSetup
{
    /// <summary>
    /// Interaction logic for DatabaseSelectWindow.xaml
    /// </summary>
    public partial class DatabaseSelectWindow : Window
    {
        private DatabaseNewWindow _DatabaseNewWindow { get; set; }

        public DatabaseSelectWindow()
        {
            InitializeComponent();
        }

        private void ButtonNewConnection_Click(object sender, RoutedEventArgs e)
        {
            _DatabaseNewWindow = new DatabaseNewWindow();
            _DatabaseNewWindow.ShowDialog();
        }

        private async void ButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
