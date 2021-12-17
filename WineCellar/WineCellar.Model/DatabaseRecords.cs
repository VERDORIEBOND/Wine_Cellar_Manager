namespace WineCellar.Model;

public record WineRecord(int Id, string Name, decimal Buy, decimal Sell, int TypeId, string Type, 
    int CountryId, string Country, byte[]? Picture, int Year, int Content, decimal Alcohol, 
    int Rating, string Description);

public record CountryRecord(int Id, string Name);
public record TypeRecord(int Id, string Name);
