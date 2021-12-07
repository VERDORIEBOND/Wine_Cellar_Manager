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

namespace WineCellar
{
    /// <summary>
    /// Interaction logic for DetailedView.xaml
    /// </summary>
    public partial class DetailedView : Window
    {
        private MainWindow mainWindow;
        private int indexID;
        private string testID;

        private IConfiguration? Configuration { get; set; }

        public DetailedView(int value)
        {
            InitializeComponent();

            Configuration = ConfigurationUtility.BuildConfiguration();

            // DataAccess requires the configuration to create SqlConnections
            DataAccess.SetConfiguration(Configuration);

            SetData(value);

            mainWindow = new MainWindow();
        }

        private async void SetData(int index)
        {
            List<IWineData> items = await Data.GetAllWines();
            int lijstIndex = 0;

            foreach (var item in items)
            {
                if (index == lijstIndex)
                {
                    WineName.DataContext = item.Name;
                    WineDescription.DataContext = item.Description;

                    WineRating.DataContext = Rating(item.Rating);
                    WineType.DataContext = item.Type;
                    WineHarvestYear.DataContext = item.HarvestYear;
                    WineVolume.DataContext = item.Alcohol + "%";
                    Taste(item.Taste);
                    WineCountry.DataContext = item.OriginCountry;
                    //WineLocation.DataContext = item.StorageLocation[0];

                    WineBuy.DataContext = item.BuyPrice;
                    WineSell.DataContext = item.SellPrice;
                    WineStock.DataContext = item.Stock;

                    indexID = item.ID;
                    testID = item.Name;
                    break;
                }
                else
                {
                    lijstIndex++;
                }
            }
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
            Close();
            mainWindow.Show();
        }

        private async void Button_Click_Verwijderen(object sender, RoutedEventArgs e)
        {
            await Data.DeleteWine(indexID); //System.Data.SqlClient.SqlException: 'The DELETE statement conflicted with the REFERENCE constraint "FK_Wine_ToType". The conflict occurred in database "WineDB", table "dbo.Wine", column 'TypeId'.
            Close();
            mainWindow.Show();
        }

        private void Button_Click_Aanpassen(object sender, RoutedEventArgs e)
        {

        }
    }
}
