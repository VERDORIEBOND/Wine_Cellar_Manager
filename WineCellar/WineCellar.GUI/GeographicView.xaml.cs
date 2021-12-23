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

namespace WineCellar;

public partial class GeographicView : Window
{
    public GeographicView()
    {
        InitializeComponent();

        MyMapControl.Map = CreateMap();
    }

    public static Map CreateMap()
    {
        var map = new Map();

        map.Layers.Add(OpenStreetMap.CreateTileLayer());
        map.Layers.Add(CreatePointLayer());

        return map;
    }

    private static MemoryLayer CreatePointLayer()
    {
        MemoryProvider memoryProvider = new MemoryProvider(CreatePoint());

        return new MemoryLayer
        {
            Name = "Points",
            DataSource = memoryProvider,
            IsMapInfoLayer = true,
            Style = null
        };
    }

    // Misschien iets met https://github.com/Mapsui/Mapsui/issues/136

    private static SymbolStyle PointStyle()
    {
        SymbolStyle pointStyle = new SymbolStyle()
        {
            //BitmapId = GetBitmapIdForEmbeddedResource("Loading.CatenaMalbec.png"),
            SymbolScale = 0.3,
            Fill = new Brush(Color.FromString("Red"))
        };

        return pointStyle;
    }

    private static int GetBitmapIdForEmbeddedResource(string imagePath)
    {
        var assembly = typeof(GeographicView).GetTypeInfo().Assembly;
        var image = assembly.GetManifestResourceStream(imagePath);
        return BitmapRegistry.Instance.Register(image);
    }

    private static List<Feature> CreatePoint()
    {
        List<Feature> points = new List<Feature>();
        var feature = new Feature();
        var p1 = SphericalMercator.FromLonLat(6.079559, 52.500767);

        feature.Styles.Add(PointStyle());
        feature.Geometry = p1;
        points.Add(feature);

        return points;
    }

    void GeographicView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        MainWindow window = new MainWindow();
        window.Show();
        Application.Current.MainWindow = window;
    }
    
}