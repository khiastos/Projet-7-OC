using System.Collections;
using Findexium.Controllers;
using Findexium.Domain;
using Findexium.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Findexium.Tests.Controllers
{
    public class BidListControllerTests
    {
        private readonly Mock<IGenericRepository<BidList>> _repo = new();
        private readonly BidListController _controller;

        public BidListControllerTests()
        {
            _controller = new BidListController(_repo.Object);
        }

        // Helpers
        private static BidList MakeEntity(int id = 0) => new() { Id = id, Account = "ACC-1" };

        [Fact]
        public async Task GetAll_returns_200()
        {
            // Arrange
            _repo.Setup(r => r.GetAllAsync())
                 .ReturnsAsync(new List<BidList> { MakeEntity(1), MakeEntity(2) });

            // Act
            var result = await _controller.GetAll();

            // Assert = appelle le repo
            _repo.Verify(r => r.GetAllAsync());

            // Assert = renvoie 200
            var ok = result as OkObjectResult;
            ok.Should().NotBeNull();

            // Assert = renvoie une collection
            ok!.Value.Should().BeAssignableTo<IEnumerable>();

            // Assert = renvoie bien les 2 éléments
            Enumerable.Cast<object>((IEnumerable)ok.Value!).Count().Should().Be(2);
        }

        [Fact]
        public async Task GetById_returns_200()
        {
            // Arrange
            _repo.Setup(r => r.GetByIdAsync(42)).ReturnsAsync(MakeEntity(42));

            // Act
            var result = await _controller.GetById(42);

            // Assert = appelle le repo
            _repo.Verify(r => r.GetByIdAsync(42), Times.Once);

            // Assert = renvoie 200
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
