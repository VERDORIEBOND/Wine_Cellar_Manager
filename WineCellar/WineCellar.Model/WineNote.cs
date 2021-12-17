namespace WineCellar.Model;

public class WineNote
{
    public int Id { get; set; }
    public string Name { get; set; }

    public WineNote() : this(0, string.Empty)
    {

    }

    public WineNote(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
