using System.Windows;
using Mapsui;
using Mapsui.Utilities;
using Mapsui.Layers;
using Mapsui.Providers;
using System.Collections.Generic;
using Mapsui.Projection;
using Mapsui.Styles;
using System.IO;
using System.Reflection;
using System.Windows.Controls;
using Mapsui.UI.Wpf;
using System.Diagnostics;
using WineCellar.Model;
using Controller;
using System.Threading.Tasks;

namespace WineCellar;

public partial class GeographicView : Window
{
    private struct Cords
    {
        public int id;
        public double x, y;
    }

    private List<Feature> points = new List<Feature>();
    private List<Cords> cords = new List<Cords>();
    private bool wineClicked = false;
    private double clickBoundry = 0.05;

    public GeographicView()
    {
        InitializeComponent();
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        await AddPoints(); // AddPoints before map creation!
        MyMapControl.Map = CreateMap();
    }

    public Map CreateMap()
    {
        var map = new Map();

        map.Layers.Add(OpenStreetMap.CreateTileLayer());
        map.Layers.Add(CreatePointLayer(points));

        return map;
    }

    public async Task AddPoints()
    {
        IEnumerable<Wine> wines = await DataAccess.WineRepo.GetAll();

        foreach (Wine wine in wines)
        {
            points.Add(CreatePoint(wine.Longitude, wine.Latitude, wine.Id));
        }
    }

    private MemoryLayer CreatePointLayer(List<Feature> points)
    {
        MemoryProvider memoryProvider = new MemoryProvider(points);

        return new MemoryLayer
        {
            Name = "Points",
            DataSource = memoryProvider,
            IsMapInfoLayer = true,
            Style = null
        };
    }

    private SymbolStyle PointStyle()
    {
        SymbolStyle pointStyle = new SymbolStyle()
        {
            //BitmapId = GetBitmapIdForEmbeddedResource("pointer.jpg"),
            SymbolScale = 0.4,
            Fill = new Brush(Color.FromString("Red"))
        };

        return pointStyle;
    }

    private int GetBitmapIdForEmbeddedResource(string imagePath)
    {
        var assembly = typeof(GeographicView).GetTypeInfo().Assembly;
        var image = assembly.GetManifestResourceStream(imagePath);
        return BitmapRegistry.Instance.Register(image);
    }

    private Feature CreatePoint(double longitude, double latitude, int id)
    {
        var feature = new Feature();
        var point = SphericalMercator.FromLonLat(longitude, latitude);

        feature.Styles.Add(PointStyle());
        feature.Geometry = point;

        var pCords = new Cords();
        pCords.id = id;
        pCords.x = longitude; 
        pCords.y = latitude;

        cords.Add(pCords);

        return feature;
    }

    void GeographicView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (!wineClicked)
        {

            MainWindow window = new MainWindow();
            Application.Current.MainWindow = window;
            window.Show();
        }
    }

    private void MyMapControl_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        var screenPosition = e.GetPosition(MyMapControl);
        var worldPosition = MyMapControl.Viewport.ScreenToWorld(screenPosition.X, screenPosition.Y);
        var pointPosition = SphericalMercator.ToLonLat(worldPosition.X, worldPosition.Y);

        foreach (var item in cords)
        {
            if ((pointPosition.X > (item.x - clickBoundry) && pointPosition.X < (item.x + clickBoundry)) && (pointPosition.Y > (item.y - clickBoundry) && pointPosition.Y < (item.y + clickBoundry)))
            {
                wineClicked = true;

                DetailedView detailedView = new DetailedView(item.id);
                Application.Current.MainWindow = detailedView;
                detailedView.Show();
                Close();
            }
        }
    }
}