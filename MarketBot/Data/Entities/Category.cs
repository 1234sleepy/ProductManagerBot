using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerBot.Data.Entities
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(15)]
        public string? Name { get; set; }

        public List<Product> Products { get; set; } = new();
    }
}
