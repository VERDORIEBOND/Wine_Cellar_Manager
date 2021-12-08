using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WineCellar.DataContexts;

public class DatabaseSelectContext : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private List<DatabaseInformation> _Databases;
    public List<DatabaseInformation> Databases {
        get => _Databases;
        set {
            _Databases = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Databases)));
        }
    }

    private DatabaseInformation _SelectedDatabase;
    public DatabaseInformation SelectedDatabase {
        get => _SelectedDatabase;
        set
        {
            _SelectedDatabase = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedDatabase)));
        }
    }
}
