using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WineCellar.DataContexts;

public class DatabaseNewContext
{
    public string InputHost { get; set; }
    public string InputPort { get; set; } = "1433";
    public string InputUsername { get; set; }
    public string InputPassword { get; set; }
}
