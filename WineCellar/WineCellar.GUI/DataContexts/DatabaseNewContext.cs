using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WineCellar.DataContexts;

public class DatabaseNewContext
{
    public string InputName { get; set; } = string.Empty;
    public string InputHost { get; set; } = string.Empty;
    public string InputPort { get; set; } = "1433";
    public string InputUsername { get; set; } = string.Empty;
    public string InputPassword { get; set; } = string.Empty;
    public string InputDatabase { get; set; } = string.Empty;
}
