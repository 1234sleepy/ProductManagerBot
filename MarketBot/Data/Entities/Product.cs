using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace ProductManagerBot.Data.Entities
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public int Barcode { get; set; }
        public double Weight { get; set; }
        public double Calories { get; set; }
        public string DateOfManufacture  { get; set; }
        public string DateOfUse { get; set; }
        public string ProductContent{ get; set; }
        public int ManufacturerId { get; set; }
        public string Name{ get; set; }
        public DateTime AddedDate{ get; set; }
        public int UsweId{ get; set; }
        public int CategoryId{ get; set; }
    }
}
