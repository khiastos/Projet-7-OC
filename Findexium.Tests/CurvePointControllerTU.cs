using Findexium.Controllers;
using Findexium.Domain;
using Findexium.DTOs;
using Findexium.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Findexium.Tests.Controllers;

public class CurvePointController_UnitTests
{
    // Helpers
    private static CurvePoint MakeEntity(int id, double value) => new() { Id = id, CurvePointValue = value };
    private static List<CurvePoint> Seed() => new()
    {
        MakeEntity(1, 3.1d),
        MakeEntity(2, 3.2d),
        MakeEntity(3, 3.3d),
    };

    [Fact]
    public async Task GetAll_returnsOK200_with_items()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<CurvePoint>>();
        repo.Setup(r => r.GetAllAsync()).ReturnsAsync(Seed());
        var controller = new CurvePointController(repo.Object);

        // Act
        var action = await controller.GetAll();

        // Assert
        var ok = Assert.IsType<OkObjectResult>(action);
        var items = Assert.IsAssignableFrom<IEnumerable<CurvePointDTO>>(ok.Value);
        Assert.Equal(3, items.Count());
        repo.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetById_returnsOK200()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<CurvePoint>>();
        repo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(MakeEntity(2, 3.2d));
        var controller = new CurvePointController(repo.Object);

        // Act
        var action = await controller.GetById(2);

        // Assert
        var ok = Assert.IsType<OkObjectResult>(action);
        var dto = Assert.IsType<CurvePointDTO>(ok.Value);
        Assert.Equal(2, dto.Id);
        Assert.Equal(3.2d, dto.CurvePointValue);
        repo.Verify(r => r.GetByIdAsync(2), Times.Once);
    }

    [Fact]
    public async Task GetById_returnsNotFound404()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<CurvePoint>>();
        repo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((CurvePoint?)null);
        var controller = new CurvePointController(repo.Object);

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
        var repo = new Mock<IGenericRepository<CurvePoint>>();
        CurvePoint? added = null;
        repo.Setup(r => r.AddAsync(It.IsAny<CurvePoint>()))
            .Callback<CurvePoint>(e =>
            {
                added = e;
                e.Id = 42;
            })
            .Returns(Task.CompletedTask);

        var controller = new CurvePointController(repo.Object);
        var newDto = new CurvePointDTO { CurvePointValue = 3.4d };

        // Act
        var action = await controller.Create(newDto);

        // Assert
        var created = Assert.IsType<CreatedAtActionResult>(action);
        var dto = Assert.IsType<CurvePointDTO>(created.Value);
        Assert.Equal(3.4d, dto.CurvePointValue);
        Assert.Equal(42, dto.Id);
        repo.Verify(r => r.AddAsync(It.IsAny<CurvePoint>()), Times.Once);
        Assert.NotNull(added);
        Assert.Equal(3.4d, added!.CurvePointValue);
    }

    [Fact]
    public async Task Create_returns400_when_model_invalid()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<CurvePoint>>();
        var controller = new CurvePointController(repo.Object);
        controller.ModelState.AddModelError("CurvePointValue", "Required");

        // Act
        var action = await controller.Create(new CurvePointDTO { CurvePointValue = -1d });

        // Assert
        Assert.IsType<BadRequestObjectResult>(action);
        repo.Verify(r => r.AddAsync(It.IsAny<CurvePoint>()), Times.Never);
    }

    [Fact]
    public async Task Update_returnsOK200()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<CurvePoint>>();
        var existing = MakeEntity(2, 3.2d);
        repo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(existing);
        repo.Setup(r => r.UpdateAsync(It.IsAny<CurvePoint>())).Returns(Task.CompletedTask);
        var controller = new CurvePointController(repo.Object);

        // Act
        var action = await controller.Update(2, new CurvePointDTO { CurvePointValue = 3.4d });

        // Assert
        var ok = Assert.IsType<OkObjectResult>(action);
        var dto = Assert.IsType<CurvePointDTO>(ok.Value);
        Assert.Equal(2, dto.Id);
        Assert.Equal(3.4d, dto.CurvePointValue);
        repo.Verify(r => r.GetByIdAsync(2), Times.Once);
        repo.Verify(r => r.UpdateAsync(It.Is<CurvePoint>(c => c.Id == 2 && c.CurvePointValue == 3.4d)), Times.Once);
    }

    [Fact]
    public async Task Update_returnsNotFound404()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<CurvePoint>>();
        repo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((CurvePoint?)null);
        var controller = new CurvePointController(repo.Object);

        // Act
        var action = await controller.Update(99, new CurvePointDTO { CurvePointValue = 3.4d });

        // Assert
        var notFound = Assert.IsType<NotFoundObjectResult>(action);
        Assert.Equal("CurvePoint not found", notFound.Value);
        repo.Verify(r => r.GetByIdAsync(99), Times.Once);
        repo.Verify(r => r.UpdateAsync(It.IsAny<CurvePoint>()), Times.Never);
    }

    [Fact]
    public async Task Update_returns400_when_model_invalid()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<CurvePoint>>();
        var controller = new CurvePointController(repo.Object);
        controller.ModelState.AddModelError("CurvePointValue", "Required");

        // Act
        var action = await controller.Update(2, new CurvePointDTO { CurvePointValue = -1d });

        // Assert
        Assert.IsType<BadRequestObjectResult>(action);
        repo.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Never);
        repo.Verify(r => r.UpdateAsync(It.IsAny<CurvePoint>()), Times.Never);
    }

    [Fact]
    public async Task Delete_returnsNoContent204()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<CurvePoint>>();
        repo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(MakeEntity(2, 3.2d));
        repo.Setup(r => r.DeleteAsync(2)).Returns(Task.CompletedTask);
        var controller = new CurvePointController(repo.Object);

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
        var repo = new Mock<IGenericRepository<CurvePoint>>();
        repo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((CurvePoint?)null);
        var controller = new CurvePointController(repo.Object);

        // Act
        var action = await controller.Delete(99);

        // Assert
        var notFound = Assert.IsType<NotFoundObjectResult>(action);
        Assert.Equal("CurvePoint not found", notFound.Value);
        repo.Verify(r => r.GetByIdAsync(99), Times.Once);
        repo.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Never);
    }
}
