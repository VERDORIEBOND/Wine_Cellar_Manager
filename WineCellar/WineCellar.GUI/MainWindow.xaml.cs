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
            SetTastingNotesAsync();
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

        private void SetFilter(List<IWineData> filter, List<IWineData> filteredList, List<int> wineIds)
        {
            foreach (var item in filter)
            {
                if (!wineIds.Contains(item.ID))
                {
                    filteredList.Add(item);
                    wineIds.Add(item.ID);
                }
            }
        }

        private bool GetNameFilter()
        {
            return tbWineName.Text != "";
        }

        private bool GetPriceFilter()
        {
            return slPriceFrom.Value > slPriceFrom.Minimum || slPriceTo.Value < slPriceTo.Maximum;
        }

        private bool GetTypeFilter()
        {
            return CbWinetype.Text != "";
        }

        private bool GetStorageFilter()
        {
            return CbStorageLocation.Text != "";
        }

        private bool GetAgeFilter()
        {
            return SlYearFrom.Value > SlYearFrom.Minimum || SlYearTo.Value < SlYearTo.Maximum;
        }

        private bool GetTasteFilter()
        {
            return LbTastingNotes.SelectedItems.Count > 0;
        }

        private bool GetRatingFilter()
        {
            return RbWineRating.Value > 0;
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
                        notes.Add(tasteNote);
                        ListBoxItem listBoxItem = new ListBoxItem { Content = tasteNote };
                        listBoxItems.Add(listBoxItem);
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

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            List<IWineData> filteredList = new List<IWineData>();
            List<int> wineIds = new List<int>();
            wineIds.Clear();

            if (GetNameFilter())
            {
                var filterName = Data.FilterWineName(allItems, tbWineName.Text);
                SetFilter(filterName, filteredList, wineIds);
            }
            else if (GetPriceFilter())
            {
                var filterPrice = Data.FilterWinePrice(allItems, slPriceFrom.Value, slPriceTo.Value);
                SetFilter(filterPrice, filteredList, wineIds);
            }
            else if (GetTypeFilter())
            {
                var filterType = Data.FilterWineType(allItems, CbWinetype.Text);
                SetFilter(filterType, filteredList, wineIds);
            }
            else if (GetStorageFilter())
            {
                var filterStorage = Data.FilterWineStorage(allItems, CbStorageLocation.Text);
                SetFilter(filterStorage, filteredList, wineIds);
            }
            else if (GetAgeFilter())
            {
                var filterAge = Data.FilterWineAge(allItems, SlYearFrom.Value, SlYearTo.Value);
                SetFilter(filterAge, filteredList, wineIds);
            }
            else if (GetTasteFilter())
            {
                var filterTaste = Data.FilterWineTaste(allItems, SelectedTastingNotes());
                SetFilter(filterTaste, filteredList, wineIds);
            }
            else if (GetRatingFilter())
            {
                var filterRating = Data.FilterWineRating(allItems, RbWineRating.Value);
                SetFilter(filterRating, filteredList, wineIds);
            }

            items = filteredList;
            WineDataBinding.ItemsSource = items;

            ICollectionView view = CollectionViewSource.GetDefaultView(WineDataBinding.ItemsSource);
            view.Refresh();
        }
    }
}
