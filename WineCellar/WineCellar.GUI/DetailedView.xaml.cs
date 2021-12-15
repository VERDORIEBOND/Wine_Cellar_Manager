using Controller;
using Model;
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
using WineCellar.Model;

namespace WineCellar
{
    /// <summary>
    /// Interaction logic for DetailedView.xaml
    /// </summary>
    public partial class DetailedView : Window
    {
        private MainWindow mainWindow;
        private WineRecord wineRecord;

        public int IndexID { get; set; }

        public DetailedView(int value, List<IWineData> items)
        {
            InitializeComponent();

            SetData(value, items);
        }

        private void LoadListStart()
        {
            DataLoading.Opacity = 1;
            DataLoading.IsEnabled = true;
        }

        private void LoadListStop()
        {
            DataLoading.Height = 0;
            DataLoading.Width = 0;
            DataLoading.Opacity = 0;
            DataLoading.IsEnabled = false;
        }

        private void SetData(int indexClicked, List<IWineData> lijst)
        {
            LoadListStart();
            int lijstIndex = 0;

            foreach (var item in lijst)
            {
                if (indexClicked == lijstIndex)
                {
                    WineName.DataContext = item.Name;
                    WineDescription.DataContext = item.Description;

                    WineRating.DataContext = Rating(item.Rating);
                    WineType.DataContext = item.Type;
                    WineHarvestYear.DataContext = item.Age;
                    WineVolume.DataContext = item.Alcohol + "%";
                    Taste(item.Taste);
                    WineCountry.DataContext = item.OriginCountry;

                    if (item.StorageLocation.Length > 0)
                    {
                        WineLocation.DataContext = item.StorageLocation[0];
                    }
                    else
                    {
                        WineLocation.DataContext = "Onbekend";
                    }

                    WineBuy.DataContext = item.BuyPrice;
                    WineSell.DataContext = item.SellPrice;
                    WineStock.DataContext = item.Stock;

                    IndexID = item.ID;

                    wineRecord = new WineRecord(item.ID, item.Name, (decimal)item.BuyPrice, (decimal)item.SellPrice, item.TypeID, item.Type, item.CountryID, item.OriginCountry, null, item.Age, item.Stock, (decimal)item.Alcohol, item.Rating, item.Description);
                    break;
                }
                else
                {
                    lijstIndex++;
                }
            }

            LoadListStop();
        }

        public string Rating(int rating)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < rating; i++)
            {
                sb.Append('★');
            }

            return sb.ToString();
        }

        public void Taste(string[] arrayTaste)
        {
            foreach (string item in arrayTaste)
            {
                ListBoxItem listBoxItem = new ListBoxItem { Content = item };
                WineTaste.Items.Add(listBoxItem);
            }
        }

        private void Button_Click_Terug(object sender, RoutedEventArgs e)
        {
            mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
            Close();
        }

        private async void Button_Click_Verwijderen(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Weet u het zeker?", "Verwijderen", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                await Data.DeleteWine(IndexID);

                mainWindow = new MainWindow();
                Application.Current.MainWindow = mainWindow;
                mainWindow.Show();
                Close();
            }
        }

        private void Button_Click_Aanpassen(object sender, RoutedEventArgs e)
        {
            // Hier komt het 'Aanpassen' scherm
        }

        private async void Voorraad_Add(object sender, RoutedEventArgs e)
        {
            WineStock.DataContext = await Data.Add_Stock(IndexID);
        }

        private async void Voorraad_Remove(object sender, RoutedEventArgs e)
        {
            WineStock.DataContext = await Data.Remove_Stock(IndexID);
        }
    }
}
