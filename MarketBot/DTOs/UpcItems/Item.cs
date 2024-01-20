using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerBot.DTOs.UpcItems
{
    public class Item
    {
        public string ean { get; set; }
        public string title { get; set; }
        public string upc { get; set; }
        public string gtin { get; set; }
        public string asin { get; set; }
        public string description { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public string dimension { get; set; }
        public string weight { get; set; }
        public string currency { get; set; }
        public float lowest_recorded_price { get; set; }
        public int highest_recorded_price { get; set; }
        public string[] images { get; set; }
        public Offer[] offers { get; set; }
    }
}
