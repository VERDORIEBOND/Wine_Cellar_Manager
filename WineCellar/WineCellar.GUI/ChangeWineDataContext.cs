using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WineCellar.Model;

namespace WineCellar
{
    public class ChangeWineDataContext
    {
        public string WineName { get; set; }
        public double BuyPrice { get; set; }
        public double SellPrice { get; set; }
        public string OriginCountry { get; set; }
        public int HarvestYear { get; set; }
        public double Alcohol { get; set; }
        public string Description { get; set; }

        public ChangeWineDataContext()
        {

        }

        public void SetData(WineRecord wineRecord)
        {
            WineName = wineRecord.Name;
            Description = wineRecord.Description;
            BuyPrice = (double)wineRecord.Buy;
            SellPrice = (double)wineRecord.Sell;
            OriginCountry = wineRecord.Country;
            HarvestYear = wineRecord.Year;
            Alcohol = (double)wineRecord.Alcohol;
        }
    }
}
