using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerBot.Data.Entities
{
    public class User : AddedDateEntity, IEntity
    {
        public int Id { get; set; }
        [Required]
        public long TgId { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Username { get; set; }
        [MaxLength(15)]
        public string? Phone { get; set; }
        [MaxLength(25)]
        public string? Email { get; set; }

        public List<Product> Products { get; set; } = new();
        public List<FavoriteProduct> FavoriteProducts { get; set; } = new();

        public override string ToString()
        {
            return $"Id : {Id} Name : {Name} Username : {Username} Phone : {Phone} Email : {Email}";
        }
    }
}
