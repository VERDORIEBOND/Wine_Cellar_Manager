using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WineCellar.Model;

namespace WineCellar.DataContexts;

public class DetailsContext : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private bool _IsMultipleAdd;
    public bool IsMultipleAdd {
        get => _IsMultipleAdd;
        set
        {
            _IsMultipleAdd = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsMultipleAdd)));
        }
    }

    private string _AddShelf;
    public string AddShelf {
        get => _AddShelf;
        set
        {
            _AddShelf = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddShelf)));
        }
    }

    public string _AddRow;
    public string AddRow {
        get => _AddRow;
        set 
        {
            _AddRow = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddRow)));
        }
    }

    public string _AddColumn;
    public string AddColumn {
        get => _AddColumn;
        set
        {
            _AddColumn = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddColumn)));
        }
    }

    public string _AddRowTo;
    public string AddRowTo {
        get => _AddRowTo;
        set
        {
            _AddRowTo = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddRowTo)));
        }
    }

    public string _AddColumnTo;
    public string AddColumnTo {
        get => _AddColumnTo;
        set
        {
            _AddColumnTo = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddColumnTo)));
        }
    }

    public List<EntityWithCheck<StorageLocation>> _Locations;
    public List<EntityWithCheck<StorageLocation>> Locations {
        get => _Locations;
        set
        {
            _Locations = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Locations)));
        }
    }
}
