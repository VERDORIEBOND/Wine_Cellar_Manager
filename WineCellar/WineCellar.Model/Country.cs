namespace Model;

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Country() : this(0, string.Empty)
    {

    }

    public Country(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
