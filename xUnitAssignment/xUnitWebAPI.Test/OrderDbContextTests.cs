using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DataModel.Database;
using WebAPI.DataModel.Entity;

namespace xUnitWebAPI.Test
{
    public class OrderDbContextTests
    {
        private readonly OrderDbContext _dbContext;

        public OrderDbContextTests()
        {
            // Create a fresh in‑memory database for each test
            var options = new DbContextOptionsBuilder<OrderDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            _dbContext = new OrderDbContext(options);
        }
        [Fact]
        public async Task AddOrder_ShouldBeSavedAndRetrievable()
        {
            // Arrange: create a new order
            var newOrder = new Order
            {
                OrderId = 1,
                OrderItem = 10,
                IsPaid = false,
                UserId = 1,
                CreatedAt = DateTime.UtcNow
            };

            // Act: add and save
            _dbContext.Order.Add(newOrder);
            await _dbContext.SaveChangesAsync();

            // Assert: retrieve it back
            var fetched = await _dbContext.Order
                .FirstOrDefaultAsync(o => o.OrderId == newOrder.OrderId);

            Assert.NotNull(fetched);
            Assert.Equal(newOrder.OrderId, fetched!.OrderId);
            Assert.Equal(newOrder.OrderItem, fetched.OrderItem);
            Assert.Equal(newOrder.IsPaid, fetched.IsPaid);
            Assert.Equal(newOrder.UserId, fetched.UserId);
            Assert.Equal(newOrder.CreatedAt, fetched.CreatedAt);
        }

    }
}
