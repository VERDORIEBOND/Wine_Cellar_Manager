using Controller;
using Controller.Repositories;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using WineCellar.Model;

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
            this.type.DisplayMemberPath = "Value";
            this.type.SelectedValuePath = "Key";
            this.type.ItemsSource = types;
        }

        private async void SetCountries()
        {
            var countries = await Data.GetAllCountries();
            this.country.DisplayMemberPath = "Value";
            this.country.SelectedValuePath = "Key";
            this.country.ItemsSource = countries;

        }
        private void CancelRegister(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void PlaceholderFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (this.placeholders.ContainsValue(textBox.Text))
            {
                textBox.Text = "";
            }
            else
            {
                if (!this.placeholders.ContainsKey(textBox.Name))
                {
                    this.placeholders[textBox.Name] = textBox.Text;
                    textBox.Text = "";
                }
            }
        }

        private void PlaceholderLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Length == 0)
            {
                textBox.Text = this.placeholders[textBox.Name];
            }
        }

        private void BrowseFiles(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            var openDi = openFileDialog.ShowDialog();

            if (openDi == true)
            {
                var filePath = openFileDialog.FileName;
                byte[] fileContent = System.IO.File.ReadAllBytes(filePath);
                FileContent = fileContent;
            }
        }

        private bool isValidString(String toValidate)
        {
            return toValidate != null
                && toValidate.Length > 0
                && toValidate.Length < 255;
        }

        private bool isInList(String toValidate, List<String> list)
        {
            if (toValidate != null && !list.Contains(toValidate))
            {
                return false;
            }
            return true;
        }

        private bool isYear(String toValidate)
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
            bool validate = this.Validation();
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
                wine.TypeId = type.SelectedIndex;
                wine.Description = description.Text;
                wine.Rating = 5;

                Controller.Data.Create(wine);
                MainWindow window = new MainWindow();   

                window.Show();
                this.Close();
                MessageBox.Show("Wine geregistreerd!");
            }
        }

        private bool Validation()
        {
            if (!this.isValidString(this.name.Text))
            {
                MessageBox.Show("De naam is te lang of te kort");
                return false;
            }

            if (this.country.SelectedItem == null)
            {
                MessageBox.Show("Selecteer een land");
                return false;
            }

            if (!this.isYear(year.Text))
            {
                MessageBox.Show("Ongeldig jaartal");
                return false;
            }

            return true;

        }

    }
}
