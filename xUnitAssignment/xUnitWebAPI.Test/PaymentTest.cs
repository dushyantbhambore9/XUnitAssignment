using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DataModel.Database;
using WebAPI.DataModel.Dtos;
using WebAPI.DataModel.Entity;
using WebAPI.DataModel.ResponseModel;
using WebAPI.Repository.Implementation;

namespace xUnitWebAPI.Test
{
    public class PaymentTest
    {
        private OrderDbContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<OrderDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new OrderDbContext(options);
        }

        [Fact]
        public async Task ProcessPaymentAsync_NullDto_Throws()
        {
            // Arrange
            var ctx = CreateContext(nameof(ProcessPaymentAsync_NullDto_Throws));
            var repo = new PaymentRepo(ctx);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => repo.ProcessPaymentAsync(null!));
        }

        [Fact]
        public async Task ProcessPaymentAsync_OrderAlreadyPaid_ThrowsInvalidOperation()
        {
            // Arrange
            var ctx = CreateContext(nameof(ProcessPaymentAsync_OrderAlreadyPaid_ThrowsInvalidOperation));
            // seed one already-paid order
            ctx.Order.Add(new Order { OrderId = 123, IsPaid = true });
            await ctx.SaveChangesAsync();

            var dto = new PaymentDto
            {
                OrderId = 123,
                UserId = 1,
                OrderItem = 2,
                IsPaid = false
            };
            var repo = new PaymentRepo(ctx);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(
                () => repo.ProcessPaymentAsync(dto));
            Assert.Equal("Order already paid", ex.Message);
        }

        [Fact]
        public async Task ProcessPaymentAsync_UnpaidOrder_ReturnsSuccessJsonModel()
        {
            // Arrange
            var ctx = CreateContext(nameof(ProcessPaymentAsync_UnpaidOrder_ReturnsSuccessJsonModel));
            // seed one unpaid order
            ctx.Order.Add(new Order { OrderId = 456, IsPaid = false });
            await ctx.SaveChangesAsync();

            var dto = new PaymentDto
            {
                OrderId = 456,
                UserId = 2,
                OrderItem = 3,
                IsPaid = false
            };
            var repo = new PaymentRepo(ctx);

            // Act
            JsonModel result = await repo.ProcessPaymentAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal("Payment Processed Successfully", result.message);

            // The Order entity returned should now be marked paid
            var paidOrder = Assert.IsType<Order>(result.Data);
            Assert.True(paidOrder.IsPaid);

            // And if you re‐fetch from the context, it should also be updated
            var fromDb = await ctx.Order.FirstAsync(o => o.OrderId == 456);
            Assert.True(fromDb.IsPaid);
        }
    }
}

