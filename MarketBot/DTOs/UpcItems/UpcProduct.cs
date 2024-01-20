using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerBot.DTOs.UpcItems
{
    class UpcProduct
    {
        public string code { get; set; }
        public int total { get; set; }
        public int offset { get; set; }
        public Item[] items { get; set; }
    }
}
