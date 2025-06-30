using Microsoft.EntityFrameworkCore;
using WebAPI.DataModel.Entity;

namespace WebAPI.DataModel.Database
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Order> Order { get; set; }

    }
}
