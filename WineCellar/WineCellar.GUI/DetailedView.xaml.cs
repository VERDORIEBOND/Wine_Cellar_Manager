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
using WineCellar.DataContexts;
using System.Diagnostics;
using System.ComponentModel;

namespace WineCellar
{
    /// <summary>
    /// Interaction logic for DetailedView.xaml
    /// </summary>
    public partial class DetailedView : Window
    {
        private MainWindow mainWindow;
        private Wine wineRecord;

        private DetailsContext _DetailsContext { get; set; }
        public List<IWineData> Items { get; set; }
        public int IndexID { get; set; }
        public int IndexClicked { get; set; }

        public DetailedView(int value)
        {
            InitializeComponent();

            IndexClicked = value;

            _DetailsContext = new DetailsContext();
            DataContext = _DetailsContext;
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

                    IndexID = item.ID;

                    wineRecord = new(item.ID, item.Name, (decimal)item.BuyPrice, (decimal)item.SellPrice, item.TypeID, item.Type, item.CountryID, item.OriginCountry, null, item.Age, item.Stock, (decimal)item.Alcohol, item.Rating, item.Description);
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
            UpdateWine updateWine = new UpdateWine(IndexID, IndexClicked);
            Application.Current.MainWindow = updateWine;
            updateWine.Show();
            Close();
        }

        private async Task RefreshLocations()
        {
            _DetailsContext.Locations = (await DataAccess.LocationRepo.GetByWine(IndexID))
                .Select((entity) => new EntityWithCheck<StorageLocation>(entity, false))
                .ToList();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Items = await Data.GetAllWines();
            SetData(IndexClicked, Items);

            await RefreshLocations();
        }

        private async void LocationAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_DetailsContext.AddShelf) || _DetailsContext.AddShelf.Length > 10)
            {
                MessageBox.Show("Het veld \"Kast\" mag niet leeg en/of langer dan 10 karakters zijn!", "Verkeerde waarde ingevoerd", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (_DetailsContext.IsMultipleAdd)
            {
                // Parse the row and column fields, only add entry if these fields contain integers
                bool didParseRow = int.TryParse(_DetailsContext.AddRow, out int row);
                bool didParseCol = int.TryParse(_DetailsContext.AddColumn, out int column);
                bool didParseRowTo = int.TryParse(_DetailsContext.AddRowTo, out int rowTo);
                bool didParseColTo = int.TryParse(_DetailsContext.AddColumnTo, out int columnTo);

                if (didParseRow && didParseCol && didParseRowTo && didParseColTo)
                {
                    if (row > rowTo || column > columnTo)
                    {
                        MessageBox.Show("De velden \"Rij\" en \"Kolom\" mogen niet groter zijn dan hun \"t/m\" velden", "Verkeerde waarde ingevoerd", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        int idWine = IndexID;
                        string shelf = _DetailsContext.AddShelf;

                        int counter = 0;
                        for (int r = row; r <= rowTo; r++)
                        {
                            for (int c = column; c <= columnTo; c++)
                            {
                                StorageLocation newLocation = new(idWine, shelf, r, c);
                                if (!_DetailsContext.Locations.Any((loc) => loc.Entity.Equals(newLocation)))
                                {
                                    await DataAccess.LocationRepo.Create(newLocation);
                                    counter++;
                                }
                            }
                        }
                        await RefreshLocations();
                        MessageBox.Show($"Er zijn {counter} locaties toegevoegd!", "Locaties toegevoegd", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("De velden \"Rij\" en \"Kolom\" mogen alleen getallen bevatten!", "Verkeerde waarde ingevoerd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                // Create a location object based on what is valid already
                StorageLocation location = new()
                {
                    IdWine = IndexID,
                    Shelf = _DetailsContext.AddShelf
                };

                // Parse the row and column fields, only add entry if these fields contain integers
                bool didParseRow = int.TryParse(_DetailsContext.AddRow, out int row);
                bool didParseCol = int.TryParse(_DetailsContext.AddColumn, out int column);

                if (didParseRow && didParseCol)
                {
                    location.Row = row;
                    location.Col = column;

                    // Check if the location is already known
                    if (_DetailsContext.Locations.Any((loc) => loc.Entity.Equals(location)))
                    {
                        MessageBox.Show("De ingevoerde locatie bestaat al!", "Locatie bestaat al", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        await DataAccess.LocationRepo.Create(location);
                        await RefreshLocations();
                    }
                }
                else
                {
                    MessageBox.Show("De velden \"Rij\" en \"Kolom\" mogen alleen getallen bevatten!", "Verkeerde waarde ingevoerd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            Debug.WriteLine(LocationGrid.ItemsSource);
            Debug.WriteLine(_DetailsContext.Locations);
            Debug.WriteLine($"Shelf: {_DetailsContext.AddShelf} | Row: {_DetailsContext.AddRow} to {_DetailsContext.AddRowTo} | Column {_DetailsContext.AddColumn} to {_DetailsContext.AddColumnTo}");
        }

        private async void LocationRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            int counter = 0;

            foreach (var location in _DetailsContext.Locations)
            {
                if (location.IsEntityChecked)
                {
                    await DataAccess.LocationRepo.Delete(location.Entity);
                    counter++;
                }
            }

            await RefreshLocations();
            MessageBox.Show($"Er zijn {counter} locaties verwijderd!", "Locaties verwijderd", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
