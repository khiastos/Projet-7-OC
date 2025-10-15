using Findexium.Controllers;
using Findexium.Data;
using Findexium.Domain;
using Findexium.DTOs;
using Findexium.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Findexium.Tests.Controllers
{
    public class CurvePointControllerTests
    {
        // Helper pour créer un DbContext en mémoire avec des données initiales
        private static LocalDbContext GetDbContext(params CurvePoint[] seed)
        {
            var options = new DbContextOptionsBuilder<LocalDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var ctx = new LocalDbContext(options);
            if (seed?.Length > 0)
            {
                ctx.CurvePoints.AddRange(seed);
                ctx.SaveChanges();
            }
            return ctx;
        }

        // Helper pour obtenir des entités initiales
        private CurvePoint[] GetInitialDbEntities()
        {
            return new CurvePoint[]
             {
                new CurvePoint {Id = 1, CurvePointValue=3.1d},
                new CurvePoint {Id = 2, CurvePointValue=3.2d},
                new CurvePoint {Id = 3, CurvePointValue=3.3d},
            };
        }

        // Helper pour créer un controller avec une base de données en mémoire, avec un tuple de retour
        private (CurvePointController controller, LocalDbContext ctx) GetControllerWithInMemoryDb(params CurvePoint[] seed)
        {
            var ctx = GetDbContext(seed);
            var repo = new GenericRepository<CurvePoint>(ctx);
            var controller = new CurvePointController(repo);
            return (controller, ctx);
        }

        [Fact]
        public async Task GetAll_returnsOK200()
        {
            // Arrange = crée le controller avec une base de données initialisée
            var (controller, ctx) = GetControllerWithInMemoryDb(GetInitialDbEntities());
            // Assure la suppression du contexte après le test (dispose)
            using var _ = ctx;

            // Act = appelle la méthode GetAll du controller
            var action = await controller.GetAll();

            // Assert = vérifie que la réponse est bien 200
            var ok = Assert.IsType<OkObjectResult>(action);
            // Assert = vérifie que le contenu est bien du bon type
            var items = Assert.IsAssignableFrom<IEnumerable<CurvePointDTO>>(ok.Value);
            // Assert = vérifie que le nombre d'éléments est correct
            Assert.Equal(3, items.Count());
        }

        [Fact]
        public async Task GetById_returnsOK200()
        {
            // Arrange = crée le controller avec une base de données initialisée
            var (controller, ctx) = GetControllerWithInMemoryDb(GetInitialDbEntities());
            // Assure la suppression du contexte après le test (dispose)
            using var _ = ctx;

            // Act
            var action = await controller.GetById(2);

            // Assert = vérifie que la réponse est bien 200
            var ok = Assert.IsType<OkObjectResult>(action);
            // Assert = vérifie que le contenu est bien du bon type
            var dto = Assert.IsType<CurvePointDTO>(ok.Value);
            // Assert = vérifie que le contenu est correct
            Assert.Equal(2, dto.Id);
            Assert.Equal(3.2d, dto.CurvePointValue);
        }

        [Fact]
        public async Task GetById_returnsNotFound404()
        {
            // Arrange = crée le controller avec une base de données initialisée
            var (controller, ctx) = GetControllerWithInMemoryDb(GetInitialDbEntities());
            // Assure la suppression du contexte après le test (dispose)
            using var _ = ctx;

            // Act
            var action = await controller.GetById(99);

            // Assert = vérifie que la réponse est bien 404
            Assert.IsType<NotFoundResult>(action);
        }

        [Fact]
        public async Task Create_returnsCreated201()
        {
            // Arrange = crée le controller avec une base de données initialisée + l'entité à ajouter
            var (controller, ctx) = GetControllerWithInMemoryDb(GetInitialDbEntities());
            var newDto = new CurvePointDTO { CurvePointValue = 3.4d };
            // Assure la suppression du contexte après le test (dispose)
            using var _ = ctx;

            // Act
            var action = await controller.Create(newDto);

            // Assert = vérifie que la réponse est bien 201
            var created = Assert.IsType<CreatedAtActionResult>(action);

            // Assert = vérifie que le contenu est bien du bon type
            var dto = Assert.IsType<CurvePointDTO>(created.Value);

            // Assert = vérifie que le contenu est correct
            Assert.Equal(3.4d, dto.CurvePointValue);
        }

        [Fact]
        public async Task Create_returns400_when_model_invalid()
        {
            // Arrange (sans base de données, on mock le repo)
            var repo = new Mock<IGenericRepository<CurvePoint>>();
            var controller = new CurvePointController(repo.Object);
            controller.ModelState.AddModelError("CurvePointValue", "Required");

            // Act
            var action = await controller.Create(new CurvePointDTO { CurvePointValue = -1d });

            // Assert = vérifie que la réponse est bien 400
            var bad = Assert.IsType<BadRequestObjectResult>(action);

            // Assert = le repo n'est pas appelé
            repo.Verify(r => r.AddAsync(It.IsAny<CurvePoint>()), Times.Never);
        }

        [Fact]
        public async Task Update_returnsOK200()
        {
            // Arrange = crée le controller avec une base de données initialisée + l'entité à modifier
            var (controller, ctx) = GetControllerWithInMemoryDb(GetInitialDbEntities());
            var updateDto = new CurvePointDTO { CurvePointValue = 3.4d };
            // Assure la suppression du contexte après le test (dispose)
            using var _ = ctx;

            // Act
            var action = await controller.Update(2, updateDto);

            // Assert = vérifie que la réponse est bien 200
            var ok = Assert.IsType<OkObjectResult>(action);

            // Assert = vérifie que le contenu est bien du bon type
            var dto = Assert.IsType<CurvePointDTO>(ok.Value);

            // Assert = vérifie que le contenu est correct
            Assert.Equal(2, dto.Id);
            Assert.Equal(3.4d, dto.CurvePointValue);
        }

        [Fact]
        public async Task Update_returnsNotFound404()
        {
            // Arrange = crée le controller avec une base de données initialisée
            var (controller, ctx) = GetControllerWithInMemoryDb(GetInitialDbEntities());
            var updateDto = new CurvePointDTO { CurvePointValue = 3.4d };
            // Assure la suppression du contexte après le test (dispose)
            using var _ = ctx;

            // Act
            var action = await controller.Update(99, updateDto);

            // Assert = vérifie que la réponse est bien 404
            var notFound = Assert.IsType<NotFoundObjectResult>(action);

            // Assert = vérifie que le message est correct
            Assert.Equal("CurvePoint not found", notFound.Value);
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

            // Assert = vérifie que la réponse est bien 400
            var bad = Assert.IsType<BadRequestObjectResult>(action);

            // Assert = le repo n'est pas appelé
            repo.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Never);
            repo.Verify(r => r.UpdateAsync(It.IsAny<CurvePoint>()), Times.Never);
        }

        [Fact]
        public async Task Delete_returnsNoContent204()
        {
            // Arrange = crée le controller avec une base de données initialisée
            var (controller, ctx) = GetControllerWithInMemoryDb(GetInitialDbEntities());
            // Assure la suppression du contexte après le test (dispose)
            using var _ = ctx;

            // Act
            var action = await controller.Delete(2);

            // Assert = vérifie que la réponse est bien 204
            Assert.IsType<NoContentResult>(action);

            // Assert = vérifie que l'élément a bien été supprimé
            var entity = await ctx.CurvePoints.FindAsync(2);
            Assert.Null(entity);
        }

        [Fact]
        public async Task Delete_returnsNotFound404()
        {
            // Arrange = crée le controller avec une base de données initialisée
            var (controller, ctx) = GetControllerWithInMemoryDb(GetInitialDbEntities());
            // Assure la suppression du contexte après le test (dispose)
            using var _ = ctx;

            // Act
            var action = await controller.Delete(99);

            // Assert = vérifie que la réponse est bien 404
            var notFound = Assert.IsType<NotFoundObjectResult>(action);

            // Assert = vérifie que le message est correct
            Assert.Equal("CurvePoint not found", notFound.Value);
        }
    }
}
