using Microsoft.AspNetCore.Mvc;
using Moq;
using Stupify.Controllers;
using Stupify.Model;
using Stupify.Model.Artists;
using Stupify.Services;
using System.Collections.Generic;
using Xunit;

namespace StupifyTest
{
    public class UnitTest1
    {
        public static List<Artist> GetTestArtists()
        {
            return new List<Artist>
            {
                new Artist { Id = 1, Name = "Исполнитель 1" },
                new Artist { Id = 2, Name = "Исполнитель 2" }
            };
        }

        [Fact]
        public void Test1()
        {
            Mock<IRepository<Artist>> artistService = new Mock<IRepository<Artist>>();
            artistService.Setup(repo => repo.GetList()).Returns(GetTestArtists());
            var controller = new ArtistController(artistService.Object);

            var result = controller.Index();

            var actionResult = Assert.IsType<ViewResult>(result);
            Assert.True(actionResult.ViewData.ContainsKey("Artists"));
        }

        [Fact]
        public void Test2()
        {
            //Arrange
            Mock<IRepository<Artist>> artistService = new Mock<IRepository<Artist>>();
            artistService.Setup(repo => repo.GetList()).Returns(GetTestArtists());
            var controller = new MusiciansController(artistService.Object);

            //Act
            var result = controller.Get();

            //Assert
            var actionResult = Assert.IsType<ActionResult<List<ArtistToRead>>>(result);
            var Artists = Assert.IsType<List<ArtistToRead>>(actionResult.Value);
            Assert.Equal(GetTestArtists().Count, Artists.Count);
        }
    }
}
