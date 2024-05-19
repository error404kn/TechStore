using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechStore.Models;

namespace TechStore.Data
{
    public class TechStoreContext : DbContext
    {
        public TechStoreContext (DbContextOptions<TechStoreContext> options)
            : base(options)
        {
        }

        public DbSet<Technics> Technics { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }
}
