using System.Diagnostics;

namespace WineCellar.Model;

public class StorageLocation : IEquatable<StorageLocation>
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

    public bool Equals(StorageLocation? other)
    {
        if (other is null)
            return false;

        return IdWine == other.IdWine
            && Shelf.Equals(other.Shelf)
            && Row == other.Row
            && Col == other.Col;
    }

    public override string ToString()
    {
        return $"[StorageLocation] IdWine: {IdWine} | Shelf: {Shelf} | Row: {Row} | Col: {Col}";
    }
}
