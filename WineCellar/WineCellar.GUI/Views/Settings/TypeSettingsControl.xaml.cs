using Controller;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WineCellar.Model;

namespace WineCellar.Views.Settings
{
    /// <summary>
    /// Interaction logic for CountrySettingsControl.xaml
    /// </summary>
    public partial class TypeSettingsControl : UserControl
    {
        private List<WineType> WineTypes { get; set; }

        public TypeSettingsControl()
        {
            InitializeComponent();
        }

        private async void UserControl_Initialized(object sender, EventArgs e)
        {
            await RefreshList();
        }

        private async Task RefreshList()
        {
            WineTypes = (await DataAccess.TypeRepo.GetAll()).ToList();
            DataHolder.ItemsSource = WineTypes;
        }

        private async void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            TextBox_Add.Text = TextBox_Add.Text.Trim();
            string text = TextBox_Add.Text;

            if (text.Length > 0)
            {
                foreach (var type in WineTypes)
                {
                    if (type.Name.Equals(text))
                    {
                        MessageBox.Show("Deze waarde bestaat al!", "Verkeerde waarde ingevoerd", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                await DataAccess.TypeRepo.Create(new(0, text));
                TextBox_Add.Text = string.Empty;
                MessageBox.Show($"Waarde \"{text}\" is toegevoegd!", "Succesvol toegevoegd", MessageBoxButton.OK, MessageBoxImage.Information);

                await RefreshList();
            }
            else
            {
                MessageBox.Show("Er is geen tekst ingevoerd", "Verkeerde waarde ingevoerd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DataHolder_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (!e.EditAction.HasFlag(DataGridEditAction.Commit))
                return;

            var item = e.Row.Item as WineType;
            if (item is not null)
            {
                item.Name = item.Name.Trim();
                await DataAccess.TypeRepo.Update(item);
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
