using Controller;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Geocoding;
using Geocoding.Google;

namespace WineCellar
{
    /// <summary>
    /// Interaction logic for RegisterWine.xaml
    /// </summary>
    public partial class RegisterWine : Window
    {
        private byte[] FileContent = null;
        private Dictionary<string, string> placeholders = new Dictionary<string, string>();

        public RegisterWine()
        {
            InitializeComponent();
            SetCountries();
            SetTypes();
        }

        private async void SetTypes()
        {
            var types = await Data.GetAllTypes();
            type.DisplayMemberPath = "Value";
            type.SelectedValuePath = "Key";
            type.ItemsSource = types;
        }

        private async void SetCountries()
        {
            var countries = await Data.GetAllCountries();
            country.DisplayMemberPath = "Value";
            country.SelectedValuePath = "Key";
            country.ItemsSource = countries;

        }
        private void CancelRegister(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }
        
        void RegisterWine_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            Application.Current.MainWindow = window;
        }
        
        private async void SearchAdress_OnClick(object sender, RoutedEventArgs e)
        {
            IGeocoder geocoder = new GoogleGeocoder() { ApiKey = "AIzaSyCLIvh79Byf16A4-ZIKoSSeKJq36-fbPYA" };
            IEnumerable<Address> addresses = await geocoder.GeocodeAsync(adress.Text);
            Console.WriteLine("Formatted: " + addresses.First().FormattedAddress); //Formatted: 1600 Pennsylvania Ave SE, Washington, DC 20003, USA
            double lat = addresses.First().Coordinates.Latitude;
            double lng = addresses.First().Coordinates.Longitude;
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = $"https://www.google.com/maps/search/{lat.ToString().Replace(',', '.')}+{lng.ToString().Replace(',', '.')}",
                UseShellExecute = true
            });
        }

        private void PlaceholderFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (placeholders.ContainsValue(textBox.Text))
            {
                textBox.Text = "";
            }
            else
            {
                if (!placeholders.ContainsKey(textBox.Name))
                {
                    placeholders[textBox.Name] = textBox.Text;
                    textBox.Text = "";
                }
            }
        }

        private void PlaceholderLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Length == 0)
            {
                textBox.Text = placeholders[textBox.Name];
            }
        }

        private async void BrowseFiles(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            var openDi = openFileDialog.ShowDialog();

            if (openDi == true)
            {
                var filePath = openFileDialog.FileName;
                byte[] fileContent = await File.ReadAllBytesAsync(filePath);
                FileContent = fileContent;
            }
        }

        private bool isValidString(string toValidate)
        {
            return toValidate != null
                && toValidate.Length > 0
                && toValidate.Length < 255;
        }

        private bool isInList(string toValidate, List<string> list)
        {
            if (toValidate != null && !list.Contains(toValidate))
            {
                return false;
            }
            return true;
        }

        private bool isYear(string toValidate)
        {
            int iyear;
            if (!int.TryParse(toValidate, out iyear))
            {
                return false;
            }

            if (iyear < 1000 || iyear > 2100)
            {
                return false;
            }

            return true;
        }

        private void AttemptRegister(object sender, RoutedEventArgs e)
        {
            bool validate = Validation();
            if (validate)
            {

                WineData wine = new WineData();
                    
                wine.Name = name.Text;
                wine.Age = Convert.ToInt32(year.Text);
                wine.OriginCountry = country.Text;
                wine.Country = country.SelectedIndex;
                wine.Stock = 0;
                wine.Contents = Convert.ToInt32(contents.Text);
                wine.BuyPrice = Convert.ToDouble(buy.Text);
                wine.SellPrice = Convert.ToDouble(sell.Text);
                wine.Alcohol = Convert.ToDecimal(alcohol.Text);
                wine.Picture = FileContent;
                wine.TypeID = type.SelectedIndex;
                wine.Description = description.Text;
                wine.Rating = 5;

                Data.Create(wine);
                MainWindow window = new MainWindow();   

                window.Show();
                Application.Current.MainWindow = window;
                Close();
                MessageBox.Show("Wine geregistreerd!");
            }
        }

        private bool Validation()
        {
            if (!isValidString(this.name.Text))
            {
                MessageBox.Show("De naam is te lang of te kort");
                return false;
            }

            if (country.SelectedItem == null)
            {
                MessageBox.Show("Selecteer een land");
                return false;
            }

            if (!isYear(year.Text))
            {
                MessageBox.Show("Ongeldig jaartal");
                return false;
            }

            return true;

        }

    }
}
