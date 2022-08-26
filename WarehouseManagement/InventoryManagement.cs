using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagement
{
    internal class InventoryManagement : DbContext
    {
        public DbSet<Product>? Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Console.Write("DB Password: ");
            string password = Console.ReadLine();

            string connection = "Data Source=BC-5CD026D1Y5\\CS10DOTNET6;" +
                                "Initial Catalog=InventoryManagement;" +
                                "User Id=sa;" + $"Password={password};" +
                                "MultipleActiveResultSets=true;";

            optionsBuilder.UseSqlServer(connection);
        }
    }
}
