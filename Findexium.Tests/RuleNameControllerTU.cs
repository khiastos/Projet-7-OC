using Findexium.Controllers;
using Findexium.Domain;
using Findexium.DTOs;
using Findexium.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Findexium.Tests.Controllers;

public class RuleNameController_UnitTests
{
    // Helpers
    private static RuleName MakeEntity(int id, string name) => new() { Id = id, Name = name };
    private static List<RuleName> Seed() => new()
    {
        MakeEntity(1, "Rule-1"),
        MakeEntity(2, "Rule-2"),
        MakeEntity(3, "Rule-3"),
    };

    [Fact]
    public async Task GetAll_returnsOK200_with_items()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<RuleName>>();
        repo.Setup(r => r.GetAllAsync()).ReturnsAsync(Seed());
        var controller = new RuleNameController(repo.Object);

        // Act
        var action = await controller.GetAll();

        // Assert
        var ok = Assert.IsType<OkObjectResult>(action);
        var items = Assert.IsAssignableFrom<IEnumerable<RuleNameDTO>>(ok.Value);
        Assert.Equal(3, items.Count());
        repo.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetById_returnsOK200()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<RuleName>>();
        repo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(MakeEntity(2, "Rule-2"));
        var controller = new RuleNameController(repo.Object);

        // Act
        var action = await controller.GetById(2);

        // Assert
        var ok = Assert.IsType<OkObjectResult>(action);
        var dto = Assert.IsType<RuleNameDTO>(ok.Value);
        Assert.Equal(2, dto.Id);
        Assert.Equal("Rule-2", dto.Name);
        repo.Verify(r => r.GetByIdAsync(2), Times.Once);
    }

    [Fact]
    public async Task GetById_returnsNotFound404()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<RuleName>>();
        repo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((RuleName?)null);
        var controller = new RuleNameController(repo.Object);

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
        var repo = new Mock<IGenericRepository<RuleName>>();
        RuleName? added = null;
        repo.Setup(r => r.AddAsync(It.IsAny<RuleName>()))
            .Callback<RuleName>(e =>
            {
                added = e;
                e.Id = 42;
            })
            .Returns(Task.CompletedTask);

        var controller = new RuleNameController(repo.Object);
        var newDto = new RuleNameDTO { Name = "Rule-New" };

        // Act
        var action = await controller.Create(newDto);

        // Assert
        var created = Assert.IsType<CreatedAtActionResult>(action);
        var dto = Assert.IsType<RuleNameDTO>(created.Value);
        Assert.Equal("Rule-New", dto.Name);
        Assert.Equal(42, dto.Id);
        repo.Verify(r => r.AddAsync(It.IsAny<RuleName>()), Times.Once);
        Assert.NotNull(added);
        Assert.Equal("Rule-New", added!.Name);
    }

    [Fact]
    public async Task Create_returns400_when_model_invalid()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<RuleName>>();
        var controller = new RuleNameController(repo.Object);
        controller.ModelState.AddModelError("Name", "Required");

        // Act
        var action = await controller.Create(new RuleNameDTO { Name = null! });

        // Assert
        Assert.IsType<BadRequestObjectResult>(action);
        repo.Verify(r => r.AddAsync(It.IsAny<RuleName>()), Times.Never);
    }

    [Fact]
    public async Task Update_returnsOK200()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<RuleName>>();
        var existing = MakeEntity(2, "Rule-2");
        repo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(existing);
        repo.Setup(r => r.UpdateAsync(It.IsAny<RuleName>())).Returns(Task.CompletedTask);
        var controller = new RuleNameController(repo.Object);

        // Act
        var action = await controller.Update(2, new RuleNameDTO { Name = "Name-Updated" });

        // Assert
        var ok = Assert.IsType<OkObjectResult>(action);
        var dto = Assert.IsType<RuleNameDTO>(ok.Value);
        Assert.Equal(2, dto.Id);
        Assert.Equal("Name-Updated", dto.Name);
        repo.Verify(r => r.GetByIdAsync(2), Times.Once);
        repo.Verify(r => r.UpdateAsync(It.Is<RuleName>(r => r.Id == 2 && r.Name == "Name-Updated")), Times.Once);
    }

    [Fact]
    public async Task Update_returnsNotFound404()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<RuleName>>();
        repo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((RuleName?)null);
        var controller = new RuleNameController(repo.Object);

        // Act
        var action = await controller.Update(99, new RuleNameDTO { Name = "Name-Updated" });

        // Assert
        var notFound = Assert.IsType<NotFoundObjectResult>(action);
        Assert.Equal("RuleName not found", notFound.Value);
        repo.Verify(r => r.GetByIdAsync(99), Times.Once);
        repo.Verify(r => r.UpdateAsync(It.IsAny<RuleName>()), Times.Never);
    }

    [Fact]
    public async Task Update_returns400_when_model_invalid()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<RuleName>>();
        var controller = new RuleNameController(repo.Object);
        controller.ModelState.AddModelError("Name", "Required");

        // Act
        var action = await controller.Update(2, new RuleNameDTO { Name = null! });

        // Assert
        Assert.IsType<BadRequestObjectResult>(action);
        repo.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Never);
        repo.Verify(r => r.UpdateAsync(It.IsAny<RuleName>()), Times.Never);
    }

    [Fact]
    public async Task Delete_returnsNoContent204()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<RuleName>>();
        repo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(MakeEntity(2, "Rule-2"));
        repo.Setup(r => r.DeleteAsync(2)).Returns(Task.CompletedTask);
        var controller = new RuleNameController(repo.Object);

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
        var repo = new Mock<IGenericRepository<RuleName>>();
        repo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((RuleName?)null);
        var controller = new RuleNameController(repo.Object);

        // Act
        var action = await controller.Delete(99);

        // Assert
        var notFound = Assert.IsType<NotFoundObjectResult>(action);
        Assert.Equal("RuleName not found", notFound.Value);
        repo.Verify(r => r.GetByIdAsync(99), Times.Once);
        repo.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Never);
    }
}
