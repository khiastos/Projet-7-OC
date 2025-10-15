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
    public class TradeControllerTests
    {
        // Helper pour créer un DbContext en mémoire avec des données initiales
        private static LocalDbContext GetDbContext(params Trade[] seed)
        {
            var options = new DbContextOptionsBuilder<LocalDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var ctx = new LocalDbContext(options);
            if (seed?.Length > 0)
            {
                ctx.Trades.AddRange(seed);
                ctx.SaveChanges();
            }
            return ctx;
        }

        // Helper pour obtenir des entités initiales
        private Trade[] GetInitialDbEntities()
        {
            return new Trade[]
             {
                new Trade {Id = 1, Account="Acc-1"},
                new Trade {Id = 2, Account="Acc-2"},
                new Trade {Id = 3, Account="Acc-3"},
            };
        }

        // Helper pour créer un controller avec une base de données en mémoire, avec un tuple de retour
        private (TradeController controller, LocalDbContext ctx) GetControllerWithInMemoryDb(params Trade[] seed)
        {
            var ctx = GetDbContext(seed);
            var repo = new GenericRepository<Trade>(ctx);
            var controller = new TradeController(repo);
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
            var items = Assert.IsAssignableFrom<IEnumerable<TradeDTO>>(ok.Value);
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
            var dto = Assert.IsType<TradeDTO>(ok.Value);
            // Assert = vérifie que le contenu est correct
            Assert.Equal(2, dto.Id);
            Assert.Equal("Acc-2", dto.Account);
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
            var newDto = new TradeDTO { Account = "Acc-New" };
            // Assure la suppression du contexte après le test (dispose)
            using var _ = ctx;

            // Act
            var action = await controller.Create(newDto);

            // Assert = vérifie que la réponse est bien 201
            var created = Assert.IsType<CreatedAtActionResult>(action);
            // Assert = vérifie que le contenu est bien du bon type
            var dto = Assert.IsType<TradeDTO>(created.Value);
            // Assert = vérifie que le contenu est correct
            Assert.Equal("Acc-New", dto.Account);
        }

        [Fact]
        public async Task Create_returns400_when_model_invalid()
        {
            // Arrange (sans base de données, on mock le repo)
            var repo = new Mock<IGenericRepository<Trade>>();
            var controller = new TradeController(repo.Object);
            controller.ModelState.AddModelError("Account", "Required");

            // Act
            var action = await controller.Create(new TradeDTO { Account = null! });

            // Assert = vérifie que la réponse est bien 400
            var bad = Assert.IsType<BadRequestObjectResult>(action);
            // Assert = le repo n'est pas appelé
            repo.Verify(r => r.AddAsync(It.IsAny<Trade>()), Times.Never);
        }

        [Fact]
        public async Task Update_returnsOK200()
        {
            // Arrange = crée le controller avec une base de données initialisée + l'entité à modifier
            var (controller, ctx) = GetControllerWithInMemoryDb(GetInitialDbEntities());
            var updateDto = new TradeDTO { Account = "Acc-Updated" };
            // Assure la suppression du contexte après le test (dispose)
            using var _ = ctx;

            // Act
            var action = await controller.Update(2, updateDto);

            // Assert = vérifie que la réponse est bien 200
            var ok = Assert.IsType<OkObjectResult>(action);
            // Assert = vérifie que le contenu est bien du bon type
            var dto = Assert.IsType<TradeDTO>(ok.Value);
            // Assert = vérifie que le contenu est correct
            Assert.Equal(2, dto.Id);
            Assert.Equal("Acc-Updated", dto.Account);
        }

        [Fact]
        public async Task Update_returnsNotFound404()
        {
            // Arrange = crée le controller avec une base de données initialisée
            var (controller, ctx) = GetControllerWithInMemoryDb(GetInitialDbEntities());
            var updateDto = new TradeDTO { Account = "Acc-Updated" };
            // Assure la suppression du contexte après le test (dispose)
            using var _ = ctx;

            // Act
            var action = await controller.Update(99, updateDto);

            // Assert = vérifie que la réponse est bien 404
            var notFound = Assert.IsType<NotFoundObjectResult>(action);
            // Assert = vérifie que le message est correct
            Assert.Equal("Trade not found", notFound.Value);
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

            // Assert = vérifie que la réponse est bien 400
            var bad = Assert.IsType<BadRequestObjectResult>(action);
            // Assert = le repo n'est pas appelé
            repo.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Never);
            repo.Verify(r => r.UpdateAsync(It.IsAny<Trade>()), Times.Never);
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
            var entity = await ctx.Trades.FindAsync(2);
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
            Assert.Equal("Trade not found", notFound.Value);
        }
    }
}
