using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace ProductManagerBot.Data.Entities
{
    public class Product : AddedDateEntity, IEntity
    {
        public int Id { get; set; }
        public string? Barcode { get; set; }
        public double Weight { get; set; }
        public double? Calories { get; set; }
        public DateTime DateOfManufacture  { get; set; }
        public DateTime DateOfUse { get; set; }
        public string? ProductContent { get; set; }
        public int ManufacturerId { get; set; }
        [Required]
        [MaxLength(25)]
        public string? Name { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }

    }
}
