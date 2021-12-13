using System;
using System.Collections;
using System.Collections.Generic;
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
            FilterOuters();
        }

        public void registerWine()
        {
            RegisterWine wine = new RegisterWine();
            wine.Show();
            this.Close();
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

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var tastingNotes = new List<string>();
            foreach (var tastingNote in LbTastingNotes.SelectedItems)
            {
                tastingNotes.Add(tastingNote.ToString());
            }
            items = Data.FilterWine(allItems, tbWineName.Text, slPriceFrom.Value, slPriceTo.Value, CbWinetype.Text, CbStorageLocation.Text, SlYearFrom.Value, SlYearTo.Value, tastingNotes, RbWineRating.Value);
            WineDataBinding.ItemsSource = items;
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
    }
}
