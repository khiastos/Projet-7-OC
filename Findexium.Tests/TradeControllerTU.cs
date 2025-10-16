using Findexium.Controllers;
using Findexium.Domain;
using Findexium.DTOs;
using Findexium.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Findexium.Tests.Controllers;

public class TradeController_UnitTests
{
    // Helpers
    private static Trade MakeEntity(int id, string account) => new() { Id = id, Account = account };
    private static List<Trade> Seed() => new()
    {
        MakeEntity(1, "Acc-1"),
        MakeEntity(2, "Acc-2"),
        MakeEntity(3, "Acc-3"),
    };

    [Fact]
    public async Task GetAll_returnsOK200_with_items()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<Trade>>();
        repo.Setup(r => r.GetAllAsync()).ReturnsAsync(Seed());
        var controller = new TradeController(repo.Object);

        // Act
        var action = await controller.GetAll();

        // Assert
        var ok = Assert.IsType<OkObjectResult>(action);
        var items = Assert.IsAssignableFrom<IEnumerable<TradeDTO>>(ok.Value);
        Assert.Equal(3, items.Count());
        repo.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetById_returnsOK200()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<Trade>>();
        repo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(MakeEntity(2, "Acc-2"));
        var controller = new TradeController(repo.Object);

        // Act
        var action = await controller.GetById(2);

        // Assert
        var ok = Assert.IsType<OkObjectResult>(action);
        var dto = Assert.IsType<TradeDTO>(ok.Value);
        Assert.Equal(2, dto.Id);
        Assert.Equal("Acc-2", dto.Account);
        repo.Verify(r => r.GetByIdAsync(2), Times.Once);
    }

    [Fact]
    public async Task GetById_returnsNotFound404()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<Trade>>();
        repo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Trade?)null);
        var controller = new TradeController(repo.Object);

        // Act
        var action = await controller.GetById(99);

        // Assert
        Assert.IsType<NotFoundResult>(action);
        repo.Verify(r => r.GetByIdAsync(99), Times.Once);
    }

    [Fact]
    public async Task Create_returnsCreated201()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<Trade>>();
        Trade? added = null;
        repo.Setup(r => r.AddAsync(It.IsAny<Trade>()))
            .Callback<Trade>(e =>
            {
                added = e;
                e.Id = 42;
            })
            .Returns(Task.CompletedTask);

        var controller = new TradeController(repo.Object);
        var newDto = new TradeDTO { Account = "Acc-New" };

        // Act
        var action = await controller.Create(newDto);

        // Assert
        var created = Assert.IsType<CreatedAtActionResult>(action);
        var dto = Assert.IsType<TradeDTO>(created.Value);
        Assert.Equal("Acc-New", dto.Account);
        Assert.Equal(42, dto.Id);
        repo.Verify(r => r.AddAsync(It.IsAny<Trade>()), Times.Once);
        Assert.NotNull(added);
        Assert.Equal("Acc-New", added!.Account);
    }

    [Fact]
    public async Task Create_returns400_when_model_invalid()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<Trade>>();
        var controller = new TradeController(repo.Object);
        controller.ModelState.AddModelError("Account", "Required");

        // Act
        var action = await controller.Create(new TradeDTO { Account = null! });

        // Assert
        Assert.IsType<BadRequestObjectResult>(action);
        repo.Verify(r => r.AddAsync(It.IsAny<Trade>()), Times.Never);
    }

    [Fact]
    public async Task Update_returnsOK200()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<Trade>>();
        var existing = MakeEntity(2, "Acc-2");
        repo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(existing);
        repo.Setup(r => r.UpdateAsync(It.IsAny<Trade>())).Returns(Task.CompletedTask);
        var controller = new TradeController(repo.Object);

        // Act
        var action = await controller.Update(2, new TradeDTO { Account = "Acc-Updated" });

        // Assert
        var ok = Assert.IsType<OkObjectResult>(action);
        var dto = Assert.IsType<TradeDTO>(ok.Value);
        Assert.Equal(2, dto.Id);
        Assert.Equal("Acc-Updated", dto.Account);
        repo.Verify(r => r.GetByIdAsync(2), Times.Once);
        repo.Verify(r => r.UpdateAsync(It.Is<Trade>(t => t.Id == 2 && t.Account == "Acc-Updated")), Times.Once);
    }

    [Fact]
    public async Task Update_returnsNotFound404()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<Trade>>();
        repo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Trade?)null);
        var controller = new TradeController(repo.Object);

        // Act
        var action = await controller.Update(99, new TradeDTO { Account = "Acc-Updated" });

        // Assert
        var notFound = Assert.IsType<NotFoundObjectResult>(action);
        Assert.Equal("Trade not found", notFound.Value);
        repo.Verify(r => r.GetByIdAsync(99), Times.Once);
        repo.Verify(r => r.UpdateAsync(It.IsAny<Trade>()), Times.Never);
    }

    [Fact]
    public async Task Update_returns400_when_model_invalid()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<Trade>>();
        var controller = new TradeController(repo.Object);
        controller.ModelState.AddModelError("Account", "Required");

        // Act
        var action = await controller.Update(2, new TradeDTO { Account = null! });

        // Assert
        Assert.IsType<BadRequestObjectResult>(action);
        repo.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Never);
        repo.Verify(r => r.UpdateAsync(It.IsAny<Trade>()), Times.Never);
    }

    [Fact]
    public async Task Delete_returnsNoContent204()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<Trade>>();
        repo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(MakeEntity(2, "Acc-2"));
        repo.Setup(r => r.DeleteAsync(2)).Returns(Task.CompletedTask);
        var controller = new TradeController(repo.Object);

        // Act
        var action = await controller.Delete(2);

        // Assert
        Assert.IsType<NoContentResult>(action);
        repo.Verify(r => r.GetByIdAsync(2), Times.Once);
        repo.Verify(r => r.DeleteAsync(2), Times.Once);
    }

    [Fact]
    public async Task Delete_returnsNotFound404()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<Trade>>();
        repo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Trade?)null);
        var controller = new TradeController(repo.Object);

        // Act
        var action = await controller.Delete(99);

        // Assert
        var notFound = Assert.IsType<NotFoundObjectResult>(action);
        Assert.Equal("Trade not found", notFound.Value);
        repo.Verify(r => r.GetByIdAsync(99), Times.Once);
        repo.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Never);
    }
}
