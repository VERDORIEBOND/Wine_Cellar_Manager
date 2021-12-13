namespace Model
{
    public interface IWineData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double BuyPrice { get; set; }
        public double SellPrice { get; set; }
        public string Type { get; set; }
        public int TypeID { get; set; }
        public string OriginCountry { get; set; }
        public int CountryID { get; set; }
        public int Age { get; set; }
        public string[] Taste { get; set; }
        public decimal Alcohol { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public byte[]? Picture { get; set; }
        public string[] StorageLocation { get; set; }
    }
}