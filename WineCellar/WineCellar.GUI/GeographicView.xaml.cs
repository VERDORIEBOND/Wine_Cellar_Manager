using System.Windows;
using Mapsui.Utilities;
using Mapsui.Layers;

namespace WineCellar;

public partial class GeographicView : Window
{
    public GeographicView()
    {
        InitializeComponent();
        MyMapControl.Map.Layers.Add(OpenStreetMap.CreateTileLayer());
    }
    
    void GeographicView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        MainWindow window = new MainWindow();
        window.Show();
        Application.Current.MainWindow = window;
    }
    
}