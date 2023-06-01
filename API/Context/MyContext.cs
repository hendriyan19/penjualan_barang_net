using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        public DbSet<customers> customers { get; set; }
        public DbSet<items> items { get; set; }
        public DbSet<orders> orders { get; set; }
        public DbSet<orders_item> orders_Items { get; set; }
    }
}
