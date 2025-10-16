using Findexium.Controllers;
using Findexium.Domain;
using Findexium.DTOs;
using Findexium.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Findexium.Tests.Controllers;

public class RatingController_UnitTests
{
    // Helpers
    private static Rating MakeEntity(int id, string moodys) => new() { Id = id, MoodysRating = moodys };
    private static List<Rating> Seed() => new()
    {
        MakeEntity(1, "AAA"),
        MakeEntity(2, "AA"),
        MakeEntity(3, "A"),
    };

    [Fact]
    public async Task GetAll_returnsOK200_with_items()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<Rating>>();
        repo.Setup(r => r.GetAllAsync()).ReturnsAsync(Seed());
        var controller = new RatingController(repo.Object);

        // Act
        var action = await controller.GetAll();

        // Assert
        var ok = Assert.IsType<OkObjectResult>(action);
        var items = Assert.IsAssignableFrom<IEnumerable<RatingDTO>>(ok.Value);
        Assert.Equal(3, items.Count());
        repo.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetById_returnsOK200()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<Rating>>();
        repo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(MakeEntity(2, "AA"));
        var controller = new RatingController(repo.Object);

        // Act
        var action = await controller.GetById(2);

        // Assert
        var ok = Assert.IsType<OkObjectResult>(action);
        var dto = Assert.IsType<RatingDTO>(ok.Value);
        Assert.Equal(2, dto.Id);
        Assert.Equal("AA", dto.MoodysRating);
        repo.Verify(r => r.GetByIdAsync(2), Times.Once);
    }

    [Fact]
    public async Task GetById_returnsNotFound404()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<Rating>>();
        repo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Rating?)null);
        var controller = new RatingController(repo.Object);

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
        var repo = new Mock<IGenericRepository<Rating>>();
        Rating? added = null;
        repo.Setup(r => r.AddAsync(It.IsAny<Rating>()))
            .Callback<Rating>(e =>
            {
                added = e;
                e.Id = 42;
            })
            .Returns(Task.CompletedTask);

        var controller = new RatingController(repo.Object);
        var newDto = new RatingDTO { MoodysRating = "B" };

        // Act
        var action = await controller.Create(newDto);

        // Assert
        var created = Assert.IsType<CreatedAtActionResult>(action);
        var dto = Assert.IsType<RatingDTO>(created.Value);
        Assert.Equal("B", dto.MoodysRating);
        Assert.Equal(42, dto.Id);
        repo.Verify(r => r.AddAsync(It.IsAny<Rating>()), Times.Once);
        Assert.NotNull(added);
        Assert.Equal("B", added!.MoodysRating);
    }

    [Fact]
    public async Task Create_returns400_when_model_invalid()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<Rating>>();
        var controller = new RatingController(repo.Object);
        controller.ModelState.AddModelError("MoodysRating", "Required");

        // Act
        var action = await controller.Create(new RatingDTO { MoodysRating = null! });

        // Assert
        Assert.IsType<BadRequestObjectResult>(action);
        repo.Verify(r => r.AddAsync(It.IsAny<Rating>()), Times.Never);
    }

    [Fact]
    public async Task Update_returnsOK200()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<Rating>>();
        var existing = MakeEntity(2, "AA");
        repo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(existing);
        repo.Setup(r => r.UpdateAsync(It.IsAny<Rating>())).Returns(Task.CompletedTask);
        var controller = new RatingController(repo.Object);

        // Act
        var action = await controller.Update(2, new RatingDTO { MoodysRating = "B" });

        // Assert
        var ok = Assert.IsType<OkObjectResult>(action);
        var dto = Assert.IsType<RatingDTO>(ok.Value);
        Assert.Equal(2, dto.Id);
        Assert.Equal("B", dto.MoodysRating);
        repo.Verify(r => r.GetByIdAsync(2), Times.Once);
        repo.Verify(r => r.UpdateAsync(It.Is<Rating>(r => r.Id == 2 && r.MoodysRating == "B")), Times.Once);
    }

    [Fact]
    public async Task Update_returnsNotFound404()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<Rating>>();
        repo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Rating?)null);
        var controller = new RatingController(repo.Object);

        // Act
        var action = await controller.Update(99, new RatingDTO { MoodysRating = "B" });

        // Assert
        var notFound = Assert.IsType<NotFoundObjectResult>(action);
        Assert.Equal("Rating not found", notFound.Value);
        repo.Verify(r => r.GetByIdAsync(99), Times.Once);
        repo.Verify(r => r.UpdateAsync(It.IsAny<Rating>()), Times.Never);
    }

    [Fact]
    public async Task Update_returns400_when_model_invalid()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<Rating>>();
        var controller = new RatingController(repo.Object);
        controller.ModelState.AddModelError("MoodysRating", "Required");

        // Act
        var action = await controller.Update(2, new RatingDTO { MoodysRating = null! });

        // Assert
        Assert.IsType<BadRequestObjectResult>(action);
        repo.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Never);
        repo.Verify(r => r.UpdateAsync(It.IsAny<Rating>()), Times.Never);
    }

    [Fact]
    public async Task Delete_returnsNoContent204()
    {
        // Arrange
        var repo = new Mock<IGenericRepository<Rating>>();
        repo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(MakeEntity(2, "AA"));
        repo.Setup(r => r.DeleteAsync(2)).Returns(Task.CompletedTask);
        var controller = new RatingController(repo.Object);

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
        var repo = new Mock<IGenericRepository<Rating>>();
        repo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Rating?)null);
        var controller = new RatingController(repo.Object);

        // Act
        var action = await controller.Delete(99);

        // Assert
        var notFound = Assert.IsType<NotFoundObjectResult>(action);
        Assert.Equal("Rating not found", notFound.Value);
        repo.Verify(r => r.GetByIdAsync(99), Times.Once);
        repo.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Never);
    }
}
