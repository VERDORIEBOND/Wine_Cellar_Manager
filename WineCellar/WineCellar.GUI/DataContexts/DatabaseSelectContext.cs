using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WineCellar.DataContexts;

public class DatabaseSelectContext
{
    public List<DatabaseInformation> Databases { get; set; }
    public DatabaseInformation SelectedDatabase { get; set; }
}
