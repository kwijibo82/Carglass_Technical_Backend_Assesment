using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Carglass.TechnicalAssessment.Backend.DL.Repositories;
using Carglass.TechnicalAssessment.Backend.DL;
using Carglass.TechnicalAssessment.Backend.Entities;

public class ClientRepositoryTests
{
    [Fact]
    public async Task GetAllAsync_ShouldReturnClients()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase_GetAllAsync")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            context.Clients.AddRange(new List<Client>
            {
                new Client { Id = 1, GivenName = "Mimi" },
                new Client { Id = 2, GivenName = "Javi" }
            });

            context.SaveChanges();
        }

        //Act
        using (var context = new ApplicationDbContext(options))
        {
            var repository = new ClientRepository(context);
            var result = await repository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, c => c.GivenName == "Javi");
        }
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnClient_WhenIdExists()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase_GetByIdAsync")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            context.Clients.Add(new Client { Id = 1, GivenName = "Mimi" });
            context.Clients.Add(new Client { Id = 2, GivenName = "Javi" });
            context.SaveChanges();
        }

        //Act
        using (var context = new ApplicationDbContext(options))
        {
            var repository = new ClientRepository(context);
            var result = await repository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Mimi", result.GivenName);
        }
    }

    [Fact]
    public async Task AddAsync_ShouldAddClient()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase_AddAsync")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            var repository = new ClientRepository(context);

            //Act
            var newClient = new Client { Id = 3, GivenName = "Carlos" };
            await repository.AddAsync(newClient);
            await context.SaveChangesAsync();

            // Assert
            var clientInDb = context.Clients.SingleOrDefault(c => c.Id == 3);
            Assert.NotNull(clientInDb);
            Assert.Equal("Carlos", clientInDb.GivenName);
        }
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateClient()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase_UpdateAsync")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            context.Clients.Add(new Client { Id = 1, GivenName = "Mimi" });
            context.SaveChanges();
        }

        //Act
        using (var context = new ApplicationDbContext(options))
        {
            var repository = new ClientRepository(context);
            var clientToUpdate = await context.Clients.SingleOrDefaultAsync(c => c.Id == 1);
            clientToUpdate.GivenName = "Mimi Updated";

            await repository.UpdateAsync(clientToUpdate);
            await context.SaveChangesAsync();
        }

        //Assert
        using (var context = new ApplicationDbContext(options))
        {
            var updatedClient = await context.Clients.SingleOrDefaultAsync(c => c.Id == 1);
            Assert.NotNull(updatedClient);
            Assert.Equal("Mimi Updated", updatedClient.GivenName);
        }
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveClient_WhenIdExists()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase_DeleteAsync")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            context.Clients.Add(new Client { Id = 1, GivenName = "Mimi" });
            context.SaveChanges();
        }

        //Act
        using (var context = new ApplicationDbContext(options))
        {
            var repository = new ClientRepository(context);
            await repository.DeleteAsync(1);
            await context.SaveChangesAsync();
        }

        //Assert
        using (var context = new ApplicationDbContext(options))
        {
            var clientInDb = await context.Clients.SingleOrDefaultAsync(c => c.Id == 1);
            Assert.Null(clientInDb);
        }
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnTrue_WhenClientExists()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase_ExistsAsync")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            context.Clients.Add(new Client { Id = 1, GivenName = "Mimi", DocNum = "12345", DocType = "Passport" });
            context.SaveChanges();
        }

        //Act
        using (var context = new ApplicationDbContext(options))
        {
            var repository = new ClientRepository(context);
            var exists = await repository.ExistsAsync("12345", "Passport");

            //Assert
            Assert.True(exists);
        }
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnFalse_WhenClientDoesNotExist()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase_ExistsAsync_NotFound")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            context.Clients.Add(new Client { Id = 1, GivenName = "Mimi", DocNum = "12345", DocType = "Passport" });
            context.SaveChanges();
        }

        //Act
        using (var context = new ApplicationDbContext(options))
        {
            var repository = new ClientRepository(context);
            var exists = await repository.ExistsAsync("54321", "ID");

            //Assert
            Assert.False(exists);
        }
    }

    [Fact]
    public async Task SaveChangesAsync_ShouldPersistChanges()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase_SaveChangesAsync")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            var repository = new ClientRepository(context);

            //Act
            var newClient = new Client { Id = 1, GivenName = "Mimi" };
            await repository.AddAsync(newClient);
            await repository.SaveChangesAsync();
        }

        //Assert
        using (var context = new ApplicationDbContext(options))
        {
            var clientInDb = await context.Clients.SingleOrDefaultAsync(c => c.Id == 1);
            Assert.NotNull(clientInDb);
            Assert.Equal("Mimi", clientInDb.GivenName);
        }
    }

    
    [Fact]
    public async Task DeleteChangesAsync_ShouldRemoveClientsWithSpecificDocType()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase_DeleteChangesAsync_DocType")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            context.Clients.AddRange(new List<Client>
        {
            new Client { Id = 1, GivenName = "Mimi", DocType = "Passport" },
            new Client { Id = 2, GivenName = "Javi", DocType = "ID" }
        });

            context.SaveChanges();
        }

        //Act
        using (var context = new ApplicationDbContext(options))
        {
            var repository = new ClientRepository(context);
            await repository.DeleteChangesAsync("Passport");
        }

        //Assert
        using (var context = new ApplicationDbContext(options))
        {
            var remainingClients = await context.Clients.ToListAsync();
            Assert.Single(remainingClients);
            Assert.Equal("ID", remainingClients.First().DocType);
        }
    }

    [Fact]
    public async Task GetPagedAsync_ShouldReturnPagedResults()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase_GetPagedAsync")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            context.Clients.AddRange(new List<Client>
        {
            new Client { Id = 1, GivenName = "Mimi" },
            new Client { Id = 2, GivenName = "Javi" },
            new Client { Id = 3, GivenName = "Carlos" }
        });

            context.SaveChanges();
        }

        //Act
        using (var context = new ApplicationDbContext(options))
        {
            var repository = new ClientRepository(context);
            var result = await repository.GetPagedAsync<Client>(1, 2);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Items.Count());
            Assert.Equal(3, result.TotalCount);
        }
    }

    [Fact]
    public async Task GetPagedAsync_ShouldReturnEmptyResults_WhenPageOutOfRange()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase_GetPagedAsync_Empty")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            context.Clients.AddRange(new List<Client>
        {
            new Client { Id = 1, GivenName = "Mimi" },
            new Client { Id = 2, GivenName = "Javi" }
        });

            context.SaveChanges();
        }

        //Act
        using (var context = new ApplicationDbContext(options))
        {
            var repository = new ClientRepository(context);
            var result = await repository.GetPagedAsync<Client>(2, 2);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.Items);
            Assert.Equal(2, result.TotalCount);
        }
    }



}
