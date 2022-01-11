using Controller;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Geocoding;
using Geocoding.Google;
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
        private double lat = 0;
        private double lng = 0;
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
            Close();
        }
        
        private void CreateTastingNote(object sender, RoutedEventArgs e)
        {
            bool unique = true;
            foreach(ListBoxItem tastingNote in tastingNoteList.Items)
            {
                if (String.Equals(tastingNote.Content.ToString(), tastingNoteText.Text, StringComparison.OrdinalIgnoreCase))
                {
                    unique = false;
                    continue;
                }
            }
            if(unique && tastingNoteText.Text.Length > 0)
            {
                ListBoxItem listBox = new ListBoxItem();
                listBox.Content = tastingNoteText.Text;
                listBox.FontSize = 10;
                listBox.IsSelected = true;
                listBox.PreviewMouseLeftButtonDown += ClickRemoveItemBox;
                tastingNoteText.Text = "";
                tastingNoteList.Items.Add(listBox);
            }
        }

        private void ClickRemoveItemBox(object sender, RoutedEventArgs e)
        {
            ListBoxItem boxitem = (ListBoxItem) sender;
            tastingNoteList.Items.Remove(boxitem);
        }

        void RegisterWine_Closing(object sender, CancelEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            Application.Current.MainWindow = window;
        }
        
        private async void SearchAdress_OnClick(object sender, RoutedEventArgs e)
        {
            IGeocoder geocoder = new GoogleGeocoder() { ApiKey = "" };
            IEnumerable<Address> addresses = await geocoder.GeocodeAsync(adress.Text);
            Console.WriteLine("Formatted: " + addresses.First().FormattedAddress); //Formatted: 1600 Pennsylvania Ave SE, Washington, DC 20003, USA
            lat = addresses.First().Coordinates.Latitude;
            lng = addresses.First().Coordinates.Longitude;
            Process.Start(new ProcessStartInfo
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
        private async void CreateNewTastingNotes()
        {
            var notes = await DataAccess.NoteRepo.GetAll();
            // Going through the list of tastingnotes to add. When we find
            // a note that isn't in the database yet, we'll add it. 
            foreach(ListBoxItem item in tastingNoteList.Items)
            {
                // by default it's unique, unless it's found in the database. 
                // we could use a query for this, but we won't. 
                var unique = true;
                string itemName = item.Content.ToString();
                foreach(var note in notes)
                {
                    // We check if it's there by comparing the name
                    if (String.Equals(note.Name, itemName, StringComparison.OrdinalIgnoreCase))
                    {
                        // It's there, so we set a var to true so we can add it after the loop
                        unique = false;
                        Trace.WriteLine(itemName);
                    }
                }
                if(unique)
                {
                    // Because the note wasn't found in the existing table of notes, we will now add
                    // it to the Note table. That way, when we add our relations, we can use the note.
                    await DataAccess.NoteRepo.Create(new(0, itemName));
                }
            }
        }
        private async void AttemptTastingNoteCreation(int wineId)
        {
            CreateNewTastingNotes();
            var updatedNotes = await DataAccess.NoteRepo.GetAll();
            foreach(ListBoxItem item in tastingNoteList.Items)
            {
                var noteId = GetNoteId(item.Content.ToString(), updatedNotes);
                if(noteId != 0 && wineId != 0)
                {
                    await DataAccess.NoteRepo.AddWine(wineId, noteId);
                }
            }
        }
        private int GetNoteId(string Name, IEnumerable<WineNote> notes)
        {
            foreach (var note in notes)
            {
                Trace.WriteLine(note);
                if (note.Name.Equals(Name, StringComparison.OrdinalIgnoreCase))
                {
                    return note.Id;
                }
            }
            return 0;
        }
        private void AttemptRegister(object sender, RoutedEventArgs e)
        {
            bool validate = Validation();
            if (validate)
            {
                Wine wine = new Wine();
                wine.Name = name.Text;
                wine.Year = Convert.ToInt32(year.Text);
                wine.CountryId = Convert.ToInt32(country.SelectedValue);
                wine.Content = Convert.ToInt32(contents.Text);
                wine.Buy = Convert.ToDecimal(buy.Text);
                wine.Sell = Convert.ToDecimal(sell.Text);
                wine.Alcohol = Convert.ToDecimal(alcohol.Text);
                wine.Picture = FileContent;
                wine.TypeId = Convert.ToInt32(type.SelectedValue);
                wine.Description = description.Text;
                wine.Rating = rating.Value;
                wine.Latitude = lat;
                wine.Longitude = lng;
                CreateWine(wine);
                MainWindow window = new MainWindow();
                window.Show();
                Application.Current.MainWindow = window;
                Close();
                MessageBox.Show("Wine geregistreerd!");
            }
        }
        private async void CreateWine(Wine wine)
        {
            var createdWine = await DataAccess.WineRepo.Create(wine);
            AttemptTastingNoteCreation(createdWine);
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