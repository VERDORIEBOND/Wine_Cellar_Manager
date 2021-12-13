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
        private DetailedView detailedView;

        public MainWindow()
        {
            InitializeComponent();

            FillList();
        }

        public async void FillList()
        {
            List<IWineData> items = await Data.GetAllWines();
            WineDataBinding.ItemsSource = items;
        }

        private void ListViewItem_Clicked(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;

            if (item != null)
            {
                detailedView = new DetailedView(WineDataBinding.SelectedIndex);

                Application.Current.MainWindow = detailedView;
                detailedView.Show();
                Close();
            }
        }
    }
}