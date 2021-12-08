using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model;

public class DatabaseInformation
{
    public string Name { get; set; }
    public string Host { get; set; }
    public string Port { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
    public string Database { get; set; }
    public string ConnectionString => $"Data Source={Host},{Port};Initial Catalog={Database};User ID={User};Password={Password};Connect Timeout=30";


    public DatabaseInformation() : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)
    {

    }

    public DatabaseInformation(string name, string host, string port, string user, string password, string database)
    {
        Name = name;
        Host = host;
        Port = port;
        User = user;
        Password = password;
        Database = database;
    }
}
