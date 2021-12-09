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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Controller;
using Model;

namespace WineCellar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FillList();
            var StorageData = (ComboBox)FindName("StorageData");
        }

        public async void FillList()
        {
            //var items = await Data.GetAllWines();
            WineDataBinding.ItemsSource = null;
        }

        public void RegisterWineButton(object sender, RoutedEventArgs e)
        {
            RegisterWine window = new RegisterWine(); 
            window.Show();
            this.Close();
        }
    }
}