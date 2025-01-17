﻿using Controller;
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
        public int WineID { get; set; }
        private DetailsContext _DetailsContext { get; set; }

        public DetailedView(int id)
        {
            InitializeComponent();

            LoadListStart();

            _DetailsContext = new();
            DataContext = _DetailsContext;
            
            WineID = id;
        }

        private void LoadListStart()
        {
            DataLoading.Opacity = 1;
            DataLoading.IsEnabled = true;
            DataLoading.Visibility = Visibility.Visible;
        }

        private void LoadListStop()
        {
            DataLoading.Height = 0;
            DataLoading.Width = 0;
            DataLoading.Opacity = 0;
            DataLoading.IsEnabled = false;
            DataLoading.Visibility = Visibility.Hidden;
        }

        private async Task SetData(int id)
        {
            LoadListStart();
            _DetailsContext.Wine = await Data.GetWine(id);
            _DetailsContext.Rating = Rating(_DetailsContext.Wine.Rating);
            LoadListStop();
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
            UpdateWine updateWine = new UpdateWine(WineID);
            Application.Current.MainWindow = updateWine;
            updateWine.Show();
            Close();
        }

        private async Task RefreshLocations()
        {
            _DetailsContext.Locations = (await DataAccess.LocationRepo.GetByWine(WineID))
                .Select((entity) => new EntityWithCheck<StorageLocation>(entity, false))
                .ToList();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await SetData(WineID);
            
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
                        int idWine = WineID;
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
                    IdWine = WineID,
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
