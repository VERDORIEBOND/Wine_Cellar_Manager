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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model;

namespace WineCellar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DetailedView detailedView;

        public MainWindow()
        {
            InitializeComponent();
            var items = Controller.Data.GetWineData();
            
            WineDataBinding.ItemsSource = items;
            
            //var StorageData = (ComboBox)FindName("StorageData");
            foreach (var wine in items)
            {
                foreach (var storagelocation in wine.StorageLocation)
                {
                    
                }
            }
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            detailedView = new DetailedView();

            if (sender is ListViewItem item)
            {
                this.Close();
                detailedView.Show();
            }
        }
    }
}