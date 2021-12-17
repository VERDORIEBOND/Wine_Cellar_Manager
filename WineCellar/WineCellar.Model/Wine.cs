namespace WineCellar.Model;

public class Wine
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Buy { get; set; }
    public decimal Sell { get; set; }
    public int TypeId { get; set; }
    public string Type { get; set; }
    public int CountryId { get; set; }
    public string Country { get; set; }
    public byte[]? Picture { get; set; }
    public int Year { get; set; }
    public int Content { get; set; }
    public decimal Alcohol { get; set; }
    public int Rating { get; set; }
    public string Description { get; set; }

    public Wine() : this(0, string.Empty, 0, 0,
        0, string.Empty, 0, string.Empty,
        null, 0, 0, 0,
        0, string.Empty)
    {

    } 

    public Wine(int id, string name, decimal buy, decimal sell,
        int typeId, string type, int countryId, string country,
        byte[]? picture, int year, int content, decimal alcohol,
        int rating, string description)
    {
        Id = id;
        Name = name;
        Buy = buy;
        Sell = sell;
        TypeId = typeId;
        Type = type;
        CountryId = countryId;
        Country = country;
        Picture = picture;
        Year = year;
        Content = content;
        Alcohol = alcohol;
        Rating = rating;
        Description = description;
    }
}
