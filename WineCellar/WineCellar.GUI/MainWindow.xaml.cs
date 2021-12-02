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
            //var items = Controller.Data.GetWineData();
            //WineDataBinding.ItemsSource = items;
            
            
            InitializeComponent();
            FillList();
            var StorageData = (ComboBox)FindName("StorageData");
            
        }

        public async void FillList()
        {
            var items = await Data.GetAllWines();
            foreach (var VARIABLE in items)
            {
                Debug.Print(VARIABLE.Name);
            }
            WineDataBinding.ItemsSource = items;
            foreach (var wine in items)
            {
                foreach (var storagelocation in wine.StorageLocation)
                {
                    
                }
            }
        }
    }
}