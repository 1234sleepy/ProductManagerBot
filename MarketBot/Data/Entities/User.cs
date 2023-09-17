using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerBot.Data.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
