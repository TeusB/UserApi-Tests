using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;
using user;
using user.DTO;
using user.Models;
using Microsoft.Extensions.Configuration;

namespace Minimal.IntegrationTests
{
    public class UserController2Tests
    {
        private HttpClient _client;
        private WebApplicationFactory<Program> _factory;
        // private string _userListFile;

        [SetUp]
        public void Setup()
        {
            _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                // Use the test configuration file
                builder.ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Test.json"));
                });
            });
            _client = _factory.CreateClient();
            // _userListFile = ReadFromFile("userList.json");
        }

        [Test]
        public async Task GetAll_UserList_ReturnsOkResult()
        {
            // Act
            var response = await _client.GetAsync("/user");
            var content = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<User>>(content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            users.Should().NotBeNull();
            users.Should().BeOfType<List<User>>();
        }

        [Test]
        public async Task AddUser_ReturnsCreatedResult()
        {
            // Arrange
            var insertUser = new InsertUser
            {
                Email = "heuy",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "1234567890",
                UserName = "johndoe"
            };

            var serializedUser = JsonConvert.SerializeObject(insertUser);
            var content = new StringContent(serializedUser, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/user", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public async Task AddUser_ReturnsBadrequest()
        {
            // Arrange
            var insertUser = new InsertUser
            {
                Email = "heuy",
                // FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "1234567890",
                UserName = "johndoe"
            };

            var serializedUser = JsonConvert.SerializeObject(insertUser);
            var content = new StringContent(serializedUser, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/user", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        // [Test]
        public async Task UpdateUser_ReturnsOkResult()
        {
            // Arrange
            var updateUser = new UpdateUser
            {
                Email = "heuy",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "1234567890",
                UserName = "johndoe"
            };

            var serializedUser = JsonConvert.SerializeObject(updateUser);
            var content = new StringContent(serializedUser, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("/user/2", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        // [Test]
        public async Task UpdateUser_ReturnsBadrequest()
        {
            // Arrange
            var updateUser = new UpdateUser
            {
                Email = "heuy",
                FirstName = "John",
                // LastName = "Doe",
                PhoneNumber = "1234567890",
                UserName = "johndoe"
            };

            var serializedUser = JsonConvert.SerializeObject(updateUser);
            var content = new StringContent(serializedUser, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("/user/2", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        // [Test]
        public async Task DeleteUser_Returns404()
        {
            // Act
            var response = await _client.DeleteAsync("/user/0");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        // [Test]
        public async Task DeleteUser_Returns_Ok()
        {
            // Act
            var response = await _client.DeleteAsync("/user/1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        private string ReadFromFile(string fileName)
        {
            try
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                return File.ReadAllText(filePath);
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException($"Failed to read file '{fileName}' from the root directory.", ex);
            }
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}
