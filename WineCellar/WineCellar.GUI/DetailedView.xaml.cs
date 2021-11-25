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

namespace WineCellar
{
    /// <summary>
    /// Interaction logic for DetailedView.xaml
    /// </summary>
    public partial class DetailedView : Window
    {
        private MainWindow mainWindow = new MainWindow();

        public DetailedView()
        {
            InitializeComponent();

        }

        private void Button_Click_Terug(object sender, RoutedEventArgs e)
        {
            this.Close();
            mainWindow.Show();
        }

        private void Button_Click_Verwijderen(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Aanpassen(object sender, RoutedEventArgs e)
        {

        }
    }
}
