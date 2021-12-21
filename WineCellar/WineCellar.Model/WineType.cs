namespace WineCellar.Model;

public class WineType
{
    public int Id { get; set; }
    public string Name { get; set; }

    public WineType() : this(0, string.Empty)
    {

    }

    public WineType(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
