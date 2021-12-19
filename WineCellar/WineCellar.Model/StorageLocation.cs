namespace WineCellar.Model;

public class StorageLocation
{
    public int IdWine { get; set; }
    public string Shelf { get; set; }
    public int Row { get; set; }
    public int Col { get; set; } 

    public StorageLocation() : this (0, string.Empty, 0, 0)
    {

    }

    public StorageLocation(int idWine, string shelf, int row, int col)
    {
        IdWine = idWine;
        Shelf = shelf;
        Row = row;
        Col = col;
    }
}
