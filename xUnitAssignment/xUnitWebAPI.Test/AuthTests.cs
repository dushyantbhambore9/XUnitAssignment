using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebAPI.DataModel.Database;
using WebAPI.DataModel.Dtos;
using WebAPI.DataModel.Entity;
using WebAPI.Repository.Implementation;
using WebAPI.Service;

namespace xUnitWebAPI.Test
{
    public class AuthTests
    {
        private readonly OrderDbContext _dbContext;
        private readonly AuthRespo _authService;

        public AuthTests()
        {
            // 1. In‑memory EF Core context
            var options = new DbContextOptionsBuilder<OrderDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _dbContext = new OrderDbContext(options);
            _authService = new AuthRespo(_dbContext);
        }


        [Fact]
        public async Task IsEmailRegisteredAsync_ShouldReturn400_WhenEmailAlreadyExists()
        {
            // arrange: seed a user with that email
            var existing = new User { UserId = 1, Email = "test@foo.com", Password = "Whatever1", FirstName = "X", LastName = "Y" };
            _dbContext.User.Add(existing);
            await _dbContext.SaveChangesAsync();

            var dto = new RegisterDto
            {
                UserId = 2,
                Email = "test@foo.com",
                Password = "Abcdef12",
                FirstName = "New",
                LastName = "User"
            };

            // act
            var result = await _authService.IsEmailRegisteredAsync(dto);

            // assert
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Email already exists", result.message);
            Assert.IsType<object>(result.Data);
        }

        [Theory]
        [InlineData("", 400, "Password cannot be empty")]
        [InlineData("short", 400, "Password must be at least 8 characters long")]
        [InlineData("alllowercase1", 400, "Password must contain at least one uppercase letter")]
        [InlineData("NOLOWERCASE", 400, "Password must contain at least one number")]
        public async Task IsEmailRegisteredAsync_ShouldReturn400_OnInvalidPassword(
            string badPwd, int expectedCode, string expectedMsg)
        {
            // arrange: ensure no existing email
            var dto = new RegisterDto
            {
                UserId = 1,
                Email = "unique@bar.com",
                Password = badPwd,
                FirstName = "Any",
                LastName = "Body"
            };

            // act
            var result = await _authService.IsEmailRegisteredAsync(dto);

            // assert
            Assert.Equal(expectedCode, result.StatusCode);
            Assert.Equal(expectedMsg, result.message);
            Assert.False((bool)result.Data);
        }

        [Fact]
        public async Task IsEmailRegisteredAsync_ShouldReturn200_AndAddUser_WhenAllValid()
        {
            // arrange: a fresh email + valid password
            var dto = new RegisterDto
            {
                UserId = 99,
                Email = "new@domain.com",
                Password = "GoodPass1",
                FirstName = "First",
                LastName = "Last"
            };

            // act
            var result = await _authService.IsEmailRegisteredAsync(dto);

            // assert JsonModel
            Assert.Equal(200, result.StatusCode);
            Assert.Equal("User Registered Successfully", result.message);
            var added = Assert.IsType<User>(result.Data);
            Assert.Equal(dto.UserId, added.UserId);
            Assert.Equal(dto.Email, added.Email);

            // assert actually in DB
            var inDb = await _dbContext.User.FindAsync(dto.UserId);
            Assert.NotNull(inDb);
            Assert.Equal(dto.Email, inDb!.Email);
        }

        [Theory]
        [InlineData("", 400, "Password cannot be empty", false)]
        [InlineData("short", 400, "Password must be at least 8 characters long", false)]
        [InlineData("nocaps123", 400, "Password must contain at least one uppercase letter", false)]
        [InlineData("NOdigits!", 400, "Password must contain at least one number", false)]
        [InlineData("Valid123", 200, "Password is valid", true)]
        public void IsValidPassword_ShouldReturnExpected(
            string pwd, int expCode, string expMsg, bool expData)
        {
            // act
            var result = _authService.IsValidPassword(pwd);

            // assert
            Assert.Equal(expCode, result.StatusCode);
            Assert.Equal(expMsg, result.message);
            Assert.Equal(expData, (bool)result.Data);
        }
        /// <summary>
        /// Here I have used the Data Driven approach for checking the password , that instead of Passing in the  InlineData i have used the MemberData and i have created a class called TextDataProvider and Readed the data from Text File (TextDataProvider.txt)
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="expCode"></param>
        /// <param name="expMsg"></param>
        /// <param name="expData"></param>
        [Theory]
        [MemberData(nameof(TextDataProvider.GetPasswordTestData), MemberType = typeof(TextDataProvider))]
        public void AccessDataThrough_TextFile_ShouldReturnExpected(
            string pwd, int expCode, string expMsg, bool expData)
        {
            // act
            var result = _authService.IsValidPassword(pwd);

            // assert
            Assert.Equal(expCode, result.StatusCode);
            Assert.Equal(expMsg, result.message);
            Assert.Equal(expData, (bool)result.Data);
        }



    }

}

