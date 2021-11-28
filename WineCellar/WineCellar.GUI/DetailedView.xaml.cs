using Controller;
using Microsoft.Extensions.Configuration;
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
using WineCellar.ControllerTest.Utilities;

namespace WineCellar
{
    /// <summary>
    /// Interaction logic for DetailedView.xaml
    /// </summary>
    public partial class DetailedView : Window
    {
        private MainWindow mainWindow;

        public DetailedView(int value)
        {
            InitializeComponent();

            SetData(value);

            mainWindow = new MainWindow();
        }

        private void SetData(int index)
        {
            var items = Data.GetWineData();

            foreach (var item in items)
            {
                if (index == item.ID)
                {
                    WineName.DataContext = item.Name;
                    WineDescription.DataContext = item.Description;

                    WineRating.DataContext = item.Rating;
                    WineType.DataContext = item.Type;
                    WineHarvestYear.DataContext = item.HarvestYear;
                    WineVolume.DataContext = item.Alcohol;
                    WineTaste.DataContext = item.Taste;
                    WineCountry.DataContext = item.OriginCountry;
                    WineLocation.DataContext = item.StorageLocation[0];

                    WineBuy.DataContext = item.BuyPrice;
                    WineSell.DataContext = item.SellPrice;
                    WineStock.DataContext = item.Stock;
                }
            }
        }


        private void Button_Click_Terug(object sender, RoutedEventArgs e)
        {
            Close();
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
