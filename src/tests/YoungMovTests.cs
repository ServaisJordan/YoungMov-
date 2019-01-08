using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DAL;
using model;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace tests
{
    [TestClass]
    public class YoungMovTests
    {
        private static IConfiguration configuration;
        private DataAccess dataAccess;
        [TestInitialize]
        public void Setup() {
            dataAccess = new DataAccess(GetContext());
        }
        
        [AssemblyInitialize]
        public void Configure(TestContext context) {
            configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                    .AddJsonFile("appsettings.json").Build();
        }
        
        protected virtual YoungMovContext GetContext() {
            var ConfigurationBuilder = new DbContextOptionsBuilder<YoungMovContext>();
            string connectionString = configuration.GetConnectionString("SmartCity");
            if (connectionString == null)
                throw new NotSupportedException("where is the connection string");
            ConfigurationBuilder.UseSqlServer(connectionString);
            return new YoungMovContext(ConfigurationBuilder.Options);
        }

        [TestMethod]
        public async Task ListingUserAsync() {
            IEnumerable<User> users = await dataAccess.GetUsers(null, 0, null);
            Assert.IsTrue(users.Count() > 0);
        }

        [TestMethod]
        public async Task UpdateUser() {
            IEnumerable<User> users = await dataAccess.GetUsers(null, 0, null);
            User user = users.Take(1).Single();
            user.Email = "bidon@hotmail.be";
            string userId = user.Id;
            await dataAccess.SetUser(user, user.Timestamp);
            user = await dataAccess.GetUser(userId);
            Assert.IsTrue(user.Email == "bidon@hotmail.be");
        }

        [TestMethod]
        public async Task ConcurencyAccess() {
            IEnumerable<User> users = await dataAccess.GetUsers(null, 0, null);
            User user = users.Take(1).SingleOrDefault();
            user.Email = "bidon@hotmail.be";
            string userId = user.Id;
            await dataAccess.SetUser(user, user.Timestamp);
            user.Email = "bidondon@henallux.be";
            await dataAccess.SetUser(user, user.Timestamp);
            user = await dataAccess.GetUser(userId);
            Assert.IsTrue(user.Email == "bidon@hotmail.be");
        }
    }
}
