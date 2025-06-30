using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DataModel.Database;
using WebAPI.DataModel.Entity;
using WebAPI.DataModel.ResponseModel;
using WebAPI.Repository.Implementation;
using WebAPI.Repository.Interface;
using WebAPI.Service.User;

namespace xUnitWebAPI.Test
{
    public class UserTest
    {

        private readonly OrderDbContext _dbContext;

        private readonly Mock<UserRepo> _mockUserRepo;

        public UserTest()
        {
            var options = new DbContextOptionsBuilder<OrderDbContext>()
             .UseInMemoryDatabase(Guid.NewGuid().ToString())
             .Options;
            _dbContext = new OrderDbContext(options);
            //_mockUserRepo = new Mock<UserRepo>(_dbContext);
            _mockUserRepo = new Mock<UserRepo>(_dbContext);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(999)]
        public async Task GetUserByIdAsync_ShouldReturn404_WhenUserDoesNotExist(int missingId)
        {

            var result = await _mockUserRepo.Object.GetUserByIdAsync(missingId);

            // assert
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("User not found", result.message);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturn200AndUser_WhenUserExists()
        {
            // arrange
            var user = new User
            {
                UserId = 42,
                // fill other required properties if any...
            };
            _dbContext.User.Add(user);
            await _dbContext.SaveChangesAsync();

            // act
            var result = await _mockUserRepo.Object.GetUserByIdAsync(42);

            // assert
            Assert.Equal(200, result.StatusCode);
            Assert.Equal("User found", result.message);

            // you can cast Data back to your User entity
            var returned = Assert.IsType<User>(result.Data);
            Assert.Equal(42, returned.UserId);
        }
    }
}
