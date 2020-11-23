using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using SimpleOrderApp.Domain;
using SimpleOrderApp.WebApp.Controllers;
using SimpleOrderApp.WebApp.ViewModels;
using Xunit;

namespace SimpleOrderApp.UnitTests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_WithLocations_DisplaysLocations()
        {
            // arrange
            var mockRepository = new Mock<ILocationRepository>();

            // setup the mock
            mockRepository.Setup(r => r.GetAll())
                .Returns(new[] { new Location("abc", 1), new Location("def", 2) });
            //mockRepository.Setup(r => r.Create(It.IsAny<Location>()))
            //    .Verifiable(); // we can mock methods that take parameters kinda like this

            var controller = new HomeController(mockRepository.Object, new NullLogger<HomeController>());

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

            // moq can verify method calls like this
            //mockRepository.Verify(r => r.Create(It.IsAny<Location>()), Times.Once);
        }
    }
}
