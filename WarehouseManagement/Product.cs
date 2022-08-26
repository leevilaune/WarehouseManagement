using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagement
{
    internal class Product
    {
        [Key]
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? ProductPrice { get; set; }
        public string? ProductAmount { get; set; }
    }
}
