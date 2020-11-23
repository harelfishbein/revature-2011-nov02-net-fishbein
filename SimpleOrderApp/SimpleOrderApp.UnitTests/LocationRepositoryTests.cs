using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SimpleOrderApp.Data;
using SimpleOrderApp.Data.Model;
using SimpleOrderApp.Domain;
using Xunit;

namespace SimpleOrderApp.UnitTests
{
    public class LocationRepositoryTests
    {
        // when we're testing code that uses the dbcontext...
        // we want EACH TEST to have a brand new separate DB, so that the tests don't break each other.
        // ... however, within one test method, we usually want multiple instances of the DbContext,
        //   to the SAME database, to effectively test what's going on.
        //    (with sqlite in-memory, you have to keep the connection open or the database will disappear)
        [Fact]
        public void Create_ValidLocation_Inserts()
        {
            // arrange
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SimpleOrderContext>().UseSqlite(connection).Options;
            var location = new Location("abc", 5);
            using (var context = new SimpleOrderContext(options))
            {
                context.Database.EnsureCreated();
                var repo = new LocationRepository(context);

                // act
                repo.Create(location);
            }

            // assert
            using var context2 = new SimpleOrderContext(options);
            LocationEntity locationActual = context2.Locations
                .Include(l => l.Orders)
                .Single(l => l.Name == "abc");
            Assert.Equal(location.Stock, locationActual.Stock);
            Assert.Empty(locationActual.Orders);
        }
    }
}
