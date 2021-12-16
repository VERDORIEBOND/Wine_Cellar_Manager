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
        public int WineID { get; set; }

        public DetailedView(int id)
        {
            InitializeComponent();

            WineID = id;
            _ = SetData(id);
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

        private async Task SetData(int id)
        {
            LoadListStart();
            WineData data = await Data.GetWine(id);
            LoadListStop();

            WineName.DataContext = data.Name;
            WineDescription.DataContext = data.Description;

            WineRating.DataContext = Rating(data.Rating);
            WineType.DataContext = data.Type;
            WineHarvestYear.DataContext = data.Age;
            WineVolume.DataContext = data.Alcohol + "%";
            Taste(data.Taste);
            WineCountry.DataContext = data.OriginCountry;

            _ = data.StorageLocation.Length > 0 ? WineLocation.DataContext = data.StorageLocation[0] : WineLocation.DataContext = "Onbekend";

            WineBuy.DataContext = data.BuyPrice;
            WineSell.DataContext = data.SellPrice;
            WineStock.DataContext = data.Stock;
        }

        public static string Rating(int rating)
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
                await Data.DeleteWine(WineID);

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
            WineStock.DataContext = await Data.Add_Stock(WineID);
        }

        private async void Voorraad_Remove(object sender, RoutedEventArgs e)
        {
            WineStock.DataContext = await Data.Remove_Stock(WineID);
        }
    }
}
