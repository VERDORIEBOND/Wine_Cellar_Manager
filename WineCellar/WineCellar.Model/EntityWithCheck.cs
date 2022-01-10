using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WineCellar.Model;

public class EntityWithCheck<T> : INotifyPropertyChanged
{
    private T? _Entity;
    public T? Entity
    {
        get => _Entity;
        set
        {
            if (_Entity?.Equals(value) == false)
            {
                _Entity = value;
                RaisePropertyChanged(nameof(Entity));
            }
        }
    }

    private bool _IsEntityChecked;
    public bool IsEntityChecked
    {
        get => _IsEntityChecked;
        set
        {
            if (_IsEntityChecked != value)
            {
                _IsEntityChecked = value;
                RaisePropertyChanged(nameof(IsEntityChecked));
            }
        }
    }

    public EntityWithCheck()
    {

    }

    public EntityWithCheck(T entity, bool isEntityChecked)
    {
        _Entity = entity;
        _IsEntityChecked = isEntityChecked;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void RaisePropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
