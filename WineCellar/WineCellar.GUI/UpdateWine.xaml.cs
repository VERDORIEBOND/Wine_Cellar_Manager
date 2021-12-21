using Controller;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WineCellar.Model;

namespace WineCellar
{
    /// <summary>
    /// Interaction logic for UpdateWine.xaml
    /// </summary>
    public partial class UpdateWine : Window
    {
        private byte[] FileContent = null;
        private int ID = 0;
        private int PreviousWindowIndexId;
        private WineRecord WineToUpdate;

        private Dictionary<string, string> placeholders = new Dictionary<string, string>();
        public UpdateWine(int id, int selectedIndex)
        {
            ID = id;
            PreviousWindowIndexId = selectedIndex;
            InitializeComponent();
            SetCountries();
            SetTypes();
            SetDataBinding();
            GetTastingNotes(ID);
        }
        private async void SetDataBinding()
        {
            WineRecord wine = await DataAccess.WineRepo.Get(ID);
            if (wine != null)
            {
                WineToUpdate = wine;
                name.Text = wine.Name;
                type.SelectedValue = wine.TypeId;
                description.Text = wine.Description;
                sell.Text = Convert.ToString(wine.Sell);
                buy.Text = Convert.ToString(wine.Buy);
                country.SelectedValue = wine.CountryId;
                year.Text = Convert.ToString(wine.Year);
                rating.Value = Convert.ToInt32(wine.Rating);
                contents.Text = Convert.ToString(wine.Content);
                alcohol.Text = Convert.ToString(wine.Alcohol);
            }
        }
        private async void GetTastingNotes(int wineId)
        {
            var notes = await DataAccess.NoteRepo.GetByWine(wineId);
            if (notes != null)
            {
                List<ListBoxItem> listBoxItems = new List<ListBoxItem>();
                foreach (var note in notes)
                {
                    ListBoxItem item = new ListBoxItem();
                    item.Content = note.Name;
                    item.FontSize = 10;
                    item.IsSelected = true;
                    item.PreviewMouseLeftButtonDown += PressedItemBox;
                    listBoxItems.Add(item);
                }
                tastingNoteList.ItemsSource = listBoxItems;
            }
        }
        private void PressedItemBox(object sender, RoutedEventArgs e)
        {
            ListBoxItem tag = (ListBoxItem) sender;
            DeleteNoteFromWine(ID, tag.Content.ToString());
        }

        private async void DeleteNoteFromWine(int wineId, string noteName)
        {
            var notes = await GetNotes();
            int? noteId = null;
            Trace.WriteLine(noteName);
            foreach(var note in notes)
            {
                if(note.Name.Equals(noteName, StringComparison.CurrentCultureIgnoreCase))
                {
                    noteId = note.Id; 
                }
            }

            if(noteId != null)
            {
                await DataAccess.NoteRepo.RemoveWine(ID, (int) noteId);
            }
            GetTastingNotes(ID);
        }

        private void CreateTastingNote(object sender, RoutedEventArgs e)
        {
            // Check if note is in Note table
            // If it's in note table, we only need to add wine and note id to Wine_Note table
            // If it's not in Note table we need to add it in note table and add it to Wine_Note
            DoCreateNote();
       
        }

        private async void DoCreateNote()
        {
            var notes = await GetNotes();
            var noteId = GetNoteId(tastingNoteText.Text, notes);
            int id = 0;

            if (noteId > 0) {
                id = noteId;
            } else {
                int insertedId = await DataAccess.NoteRepo.Create(new(0, tastingNoteText.Text));
                id = insertedId;
            }

            await DataAccess.NoteRepo.AddWine(ID, id);
            GetTastingNotes(ID);
        }

        private int GetNoteId(string Name, IEnumerable<NoteRecord> notes)
        {
            foreach(var note in notes)
            {
                if(note.Name == Name)
                {
                    return note.Id;
                }
            }
            return 0;
        }

        private async Task<IEnumerable<NoteRecord>> GetNotes()
        {
            var notes = await DataAccess.NoteRepo.GetAll();
            return notes;
        }

        private async void SetTypes()
        {
            var types = await Data.GetAllTypes();
            type.DisplayMemberPath = "Value";
            type.SelectedValuePath = "Key";
            type.ItemsSource = types;
        }
        private void AttemptAddTastingNote(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("OK");
        }
        private async void SetCountries()
        {
            var countries = await Data.GetAllCountries();
            country.DisplayMemberPath = "Value";
            country.SelectedValuePath = "Key";
            country.ItemsSource = countries;
        }
        private void CancelUpdate(object sender, RoutedEventArgs e)
        {
            DetailedView window = new DetailedView(PreviousWindowIndexId);
            Application.Current.MainWindow = window;
            window.Show();
            Close();
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

        private void AttemptUpdate(object sender, RoutedEventArgs e)
        {
            bool validate = Validation();
            if (validate)
            {
                WineData wine = new WineData();
                wine.ID = ID;
                wine.Name = name.Text;
                wine.Age = Convert.ToInt32(year.Text);
                wine.OriginCountry = country.Text;
                wine.Country = Convert.ToInt32(country.SelectedValue);
                wine.Stock = 0;
                wine.Contents = Convert.ToInt32(contents.Text);
                wine.BuyPrice = Convert.ToDouble(buy.Text);
                wine.SellPrice = Convert.ToDouble(sell.Text);
                wine.Alcohol = Convert.ToDecimal(alcohol.Text);
                wine.Picture = FileContent ?? WineToUpdate.Picture;
                wine.TypeID = Convert.ToInt32(type.SelectedValue);
                wine.Description = description.Text;
                wine.Rating = Convert.ToInt32(rating.Value);
                Data.Update(wine);
                DetailedView window = new DetailedView(PreviousWindowIndexId);
                window.Show();
                Application.Current.MainWindow = window;
                Close();
                MessageBox.Show("Wine bijgewerkt!");
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
