using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Controller;
using Model;
using WineCellar.Views.Settings;

namespace WineCellar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<IWineData> items;
        private List<IWineData> allItems;

        //Bottom and top filter variables
        private double _edgePriceFrom = 9999999;
        private double _edgePriceTo;
        private double _edgeYearFrom = 9999999;
        private double _edgeYearTo;
        List<string> _contentWineTypes = new List<string>();
        List<string> _contentWineLocation = new List<string>();
        List<string> contentWineNotes = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            FillList();
            _ = FilterOuters();
        }

        public void RegisterWine(object sender, RoutedEventArgs e)
        {
            RegisterWine wine = new RegisterWine();
            wine.Show();
            Application.Current.MainWindow = wine;
            Close();
        }

        public void GeographicView(object sender, RoutedEventArgs e)
        {
            GeographicView GeoView = new GeographicView();
            GeoView.Show();
            Application.Current.MainWindow = GeoView;
            Close();
        }

        public async void FillList()
        {
            LoadListStart();
            items = await Data.GetAllWines();
            allItems = items;
            WineDataBinding.ItemsSource = items;
            LoadListStop();
        }

        private void LoadListStart()
        {
            ListViewZone.Opacity = 0;
            DataLoading.Opacity = 1;
            DataLoading.IsEnabled = true;
        }

        private void LoadListStop()
        {
            ListViewZone.Opacity = 1;
            DataLoading.Opacity = 0;
            DataLoading.IsEnabled = false;
        }

        private List<string> SelectedTastingNotes()
        {
            var tastingNotes = new List<string>();

            foreach (var item in LbTastingNotes.SelectedItems)
            {
                tastingNotes.Add((item as ListBoxItem).Content.ToString());
            }

            return tastingNotes;
        }

        private async Task FilterOuters()
        {
            foreach (var item in await Data.GetAllWines())
            {
                if (item.BuyPrice < _edgePriceFrom)
                {
                    _edgePriceFrom = item.BuyPrice;
                }
                if (item.BuyPrice > _edgePriceTo)
                {
                    _edgePriceTo = item.BuyPrice;
                }
                if (item.Age < _edgeYearFrom)
                {
                    _edgeYearFrom = item.Age;
                }
                if (item.Age > _edgeYearTo)
                {
                    _edgeYearTo = item.Age;
                }
                if (_contentWineTypes.Contains(item.Type) == false)
                {
                    _contentWineTypes.Add(item.Type);
                }
                _contentWineTypes.Sort();
                foreach (var t in item.StorageLocation)
                {
                    if (_contentWineLocation.Contains(t) == false)
                    {
                        _contentWineLocation.Add(t);
                    }
                }
                _contentWineLocation.Sort();
            }
            _contentWineTypes.Add("");
            _contentWineLocation.Add("");

            slPriceFrom.Minimum = _edgePriceFrom;
            slPriceFrom.Value = _edgePriceFrom;
            slPriceFrom.Maximum = _edgePriceTo;
            slPriceTo.Minimum = _edgePriceFrom;
            slPriceTo.Value = _edgePriceTo;
            slPriceTo.Maximum = _edgePriceTo;
            CbWinetype.ItemsSource = _contentWineTypes;
            CbStorageLocation.ItemsSource = _contentWineLocation;
            SlYearFrom.Minimum = _edgeYearFrom;
            SlYearFrom.Value = _edgeYearFrom;
            SlYearFrom.Maximum = _edgeYearTo;
            SlYearTo.Minimum = _edgeYearFrom;
            SlYearTo.Value = _edgeYearTo;
            SlYearTo.Maximum = _edgeYearTo;
            LbTastingNotes.ItemsSource = contentWineNotes;

            SetTastingNotesAsync();
        }

        private async void SetTastingNotesAsync()
        {
            items = await Data.GetAllWines();
            List<string> notes = new List<string>();
            List<ListBoxItem> listBoxItems = new List<ListBoxItem>();

            foreach (var item in items)
            {
                foreach (string tasteNote in item.Taste)
                {
                    if (!notes.Contains(tasteNote))
                    {
                        ListBoxItem listBoxItem = new ListBoxItem { Content = tasteNote };
                        listBoxItems.Add(listBoxItem);
                        notes.Add(tasteNote);
                    }
                }
            }
            LbTastingNotes.ItemsSource = listBoxItems;
        }

        private int GetItemID(int indexClicked, List<IWineData> list)
        {
            int id = 0;

            foreach (var item in list)
            {
                if (indexClicked == id)
                {
                    id = item.ID;
                    break;
                }
                else
                {
                    id++;
                }
            }

            return id;
        }

        private void ListViewItem_Clicked(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;

            if (item != null)
            {
                int id = GetItemID(WineDataBinding.SelectedIndex, items);
                DetailedView detailedView = new DetailedView(id);

                Application.Current.MainWindow = detailedView;
                detailedView.Show();
                Close();
            }
        }

        private void Button_Click_Sorteer(object sender, RoutedEventArgs e)
        {
            items = Data.SortWine(SortingBox.Text, items, (bool)Aflopend.IsChecked);
            WineDataBinding.ItemsSource = items;

            ICollectionView view = CollectionViewSource.GetDefaultView(WineDataBinding.ItemsSource);
            view.Refresh();
        }

        private void Button_Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settings = new();
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settings.ShowDialog();
        }

        private void ResetRating_Click(object sender, RoutedEventArgs e)
        {
            RbWineRating.Value = 0;
        }

        private void ResetList_Click(object sender, RoutedEventArgs e)
        {
            FillList();

            ICollectionView view = CollectionViewSource.GetDefaultView(WineDataBinding.ItemsSource);
            view.Refresh();
        }

        private HashSet<int> FillHashSetWithID(List<IWineData> wines)
        {
            HashSet<int> filter = new HashSet<int>();

            foreach (var item in wines)
            {
                filter.Add(item.ID);
            }

            return filter;
        }

        private List<IWineData> FillList(HashSet<int> filter)
        {
            List<IWineData> filteredList = new List<IWineData>();

            foreach (var id in filter)
            {
                foreach (var item in allItems)
                {
                    if (item.ID == id)
                    {
                        filteredList.Add(item);
                    }
                }
            }

            return filteredList;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            HashSet<int> filteredSet = new HashSet<int>();

            foreach (var item in allItems)
            {
                filteredSet.Add(item.ID);
            }

            if (tbWineName.Text != "")
            {
                var filterName = Data.FilterWineName(allItems, tbWineName.Text);
                filteredSet.IntersectWith(FillHashSetWithID(filterName));
            }

            if (slPriceFrom.Value > slPriceFrom.Minimum || slPriceTo.Value < slPriceTo.Maximum)
            {
                var filterPrice = Data.FilterWinePrice(allItems, slPriceFrom.Value, slPriceTo.Value);
                filteredSet.IntersectWith(FillHashSetWithID(filterPrice));
            }

            if (CbWinetype.Text != "")
            {
                var filterType = Data.FilterWineType(allItems, CbWinetype.Text);
                filteredSet.IntersectWith(FillHashSetWithID(filterType));
            }

            if (CbStorageLocation.Text != "")
            {
                var filterStorage = Data.FilterWineStorage(allItems, CbStorageLocation.Text);
                filteredSet.IntersectWith(FillHashSetWithID(filterStorage));
            }

            if (SlYearFrom.Value > SlYearFrom.Minimum || SlYearTo.Value < SlYearTo.Maximum)
            {
                var filterAge = Data.FilterWineAge(allItems, SlYearFrom.Value, SlYearTo.Value);
                filteredSet.IntersectWith(FillHashSetWithID(filterAge));
            }

            if (LbTastingNotes.SelectedItems.Count > 0)
            {
                var filterTaste = Data.FilterWineTaste(allItems, SelectedTastingNotes());
                filteredSet.IntersectWith(FillHashSetWithID(filterTaste));
            }

            if (RbWineRating.Value > 0)
            {
                var filterRating = Data.FilterWineRating(allItems, RbWineRating.Value);
                filteredSet.IntersectWith(FillHashSetWithID(filterRating));
            }

            items = FillList(filteredSet);
            WineDataBinding.ItemsSource = items;

            ICollectionView view = CollectionViewSource.GetDefaultView(WineDataBinding.ItemsSource);
            view.Refresh();
        }
    }
}
