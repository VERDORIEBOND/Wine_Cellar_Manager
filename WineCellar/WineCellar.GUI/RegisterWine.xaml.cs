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

namespace WineCellar
{
    /// <summary>
    /// Interaction logic for RegisterWine.xaml
    /// </summary>
    public partial class RegisterWine : Window
    {
        private TextBox FilePath;
        private List<String> countries = new List<String> {
            "Netherlands",
            "Italy",
            "Spain",
            "China",
            "America",
            "France",
            "Portugal",
            "Mexico",
            "Taiwan",
            "Belgium",
            "Nigeria"
        };

        public RegisterWine()
        {
            InitializeComponent();

            this.FilePath = filePath;
            this.country.ItemsSource = countries;
        }

        private void BrowseFiles(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            var openDi = openFileDialog.ShowDialog();

            if (openDi == true)
            {
                var filePath = openFileDialog.FileName;
                var fileStream = openFileDialog.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    var fileContent = reader.ReadToEnd();
                }
                FilePath.Text = openFileDialog.FileName;
            }
        }

        private bool isValidString(String toValidate)
        {
            if(toValidate != null)
            {
                if (toValidate.Length > 0 && toValidate.Length < 255)
                {
                    return true;
                }
            }
            return false;
        }

        private bool isInList(String toValidate, List<String> list)
        {
            if(toValidate != null)
            {
                if (!list.Contains(toValidate))
                {
                    return false;
                }
            }
            return true;
        }

        private bool isYear(String toValidate, int digits)
        {
            try
            {
                DateTime dateTime;
                DateTime.TryParse(string.Format("1/1/{0}", toValidate), out dateTime);
            } catch (Exception)
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
                MessageBox.Show("Wine geregistreerd!");
            }
        }

        private bool Validation()
        {
            if(!this.isValidString(this.name.Text))
            {
                MessageBox.Show("De naam is te lang of te kort");
                return false;
            }

            if(this.country.SelectedItem == null)
            {
                MessageBox.Show("Selecteer een land");
                return false;
            }

            if (!this.isInList(this.country.SelectedItem.ToString(), this.countries))
            {
                MessageBox.Show("Selecteer een geldig land");
                return false;
            }

            return true;

        }
        
    }
}
