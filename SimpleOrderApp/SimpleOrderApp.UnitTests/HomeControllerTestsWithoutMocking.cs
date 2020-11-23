using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using SimpleOrderApp.Domain;
using SimpleOrderApp.WebApp.Controllers;
using SimpleOrderApp.WebApp.ViewModels;
using Xunit;

namespace SimpleOrderApp.UnitTests
{
    public class HomeControllerTestsWithoutMocking
    {
        [Fact]
        public void Index_WithLocations_DisplaysLocations()
        {
            // arrange
            var controller = new HomeController(new FakeLocationRepository(), new NullLogger<HomeController>());

            // act
            IActionResult actionResult = controller.Index();

            // assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(actionResult);
            var locations = Assert.IsAssignableFrom<IEnumerable<LocationViewModel>>(viewResult.Model);
            var locationList = locations.ToList();
            Assert.Equal(2, locationList.Count);
            Assert.Equal("abc", locationList[0].Name);
            Assert.Equal(2, locationList[1].Stock);
            Assert.Null(viewResult.ViewName);
        }
    }

    class FakeLocationRepository : ILocationRepository
    {
        public void Create(Location location)
        {
            throw new NotImplementedException();
        }

        public void Delete(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Location> GetAll()
        {
            return new[] { new Location("abc", 1), new Location("def", 2) };
        }

        public void Update(Location location)
        {
            throw new NotImplementedException();
        }
    }
}
