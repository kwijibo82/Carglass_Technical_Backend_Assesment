using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carglass.TechnicalAssessment.Backend.BL;
using Carglass.TechnicalAssessment.Backend.DL.Repositories;
using Carglass.TechnicalAssessment.Backend.Dtos;
using Carglass.TechnicalAssessment.Backend.Entities;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;

public class ClientServiceTests
{
    private readonly Mock<IClientRepository> _mockRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IValidator<ClientDto>> _mockValidator;
    private readonly ClientService _service;

    public ClientServiceTests()
    {
        _mockRepository = new Mock<IClientRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockValidator = new Mock<IValidator<ClientDto>>();
        _service = new ClientService(_mockRepository.Object, _mockMapper.Object, _mockValidator.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllClients()
    {
        //Arrange
        var clients = new List<Client> { new Client { Id = 1 }, new Client { Id = 2 } };
        var mappedClients = new List<ClientDto> { new ClientDto { Id = 1 }, new ClientDto { Id = 2 } };

        _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(clients);
        _mockMapper.Setup(m => m.Map<IEnumerable<ClientDto>>(clients)).Returns(mappedClients);

        //Act
        var result = await _service.GetAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnClient_WhenExists()
    {
        //Arrange
        var client = new Client { Id = 1 };
        var mappedClient = new ClientDto { Id = 1 };

        _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(client);
        _mockMapper.Setup(m => m.Map<ClientDto>(client)).Returns(mappedClient);

        //Act
        var result = await _service.GetByIdAsync(1);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowKeyNotFoundException_WhenClientNotFound()
    {
        //Arrange
        _mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Client?)null);

        //Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetByIdAsync(1));
    }

    [Fact]
    public async Task CreateAsync_ShouldAddClient_WhenValid()
    {
        //Arrange
        var dto = new ClientDto { Id = 1, DocNum = "123", DocType = "Passport" };
        var entity = new Client { Id = 1 };

        _mockValidator.Setup(v => v.Validate(dto)).Returns(new ValidationResult());
        _mockRepository.Setup(r => r.ExistsAsync(dto.DocNum, dto.DocType, null)).ReturnsAsync(false); // Se especifica `null` para el último parámetro
        _mockMapper.Setup(m => m.Map<Client>(dto)).Returns(entity);
        _mockMapper.Setup(m => m.Map<ClientDto>(entity)).Returns(dto);

        //Act
        var result = await _service.CreateAsync(dto);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(dto.Id, result.Id);
        _mockRepository.Verify(r => r.AddAsync(entity), Times.Once);
        _mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
    }


    [Fact]
    public async Task UpdateAsync_ShouldUpdateClient_WhenValid()
    {
        //Arrange
        var dto = new ClientDto { Id = 1, DocNum = "123", DocType = "Passport" };
        var entity = new Client { Id = 1 };

        _mockRepository.Setup(r => r.GetByIdAsync(dto.Id)).ReturnsAsync(entity);
        _mockValidator.Setup(v => v.Validate(dto)).Returns(new ValidationResult());
        _mockRepository.Setup(r => r.ExistsAsync(dto.DocNum, dto.DocType, dto.Id)).ReturnsAsync(false);

        //Act
        await _service.UpdateAsync(dto);

        //Assert
        _mockRepository.Verify(r => r.UpdateAsync(entity), Times.Once);
        _mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteClient_WhenExists()
    {
        //Arrange
        var client = new Client { Id = 1 };
        _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(client);

        //Act
        await _service.DeleteAsync(1);

        //Assert
        _mockRepository.Verify(r => r.DeleteAsync(client.Id), Times.Once);
        _mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task GetPagedClientsAsync_ShouldReturnPagedResult()
    {
        //Arrange
        var clients = new List<Client> { new Client { Id = 1 }, new Client { Id = 2 } };
        var pagedResult = new PagedResultDto<Client>
        {
            Items = clients,
            TotalCount = 2
        };
        var mappedClients = new List<ClientDto>
        {
            new ClientDto { Id = 1 },
            new ClientDto { Id = 2 }
        };

        _mockRepository.Setup(r => r.GetPagedAsync<Client>(1, 2)).ReturnsAsync(pagedResult);
        _mockMapper.Setup(m => m.Map<IEnumerable<ClientDto>>(clients)).Returns(mappedClients);

        //Act
        var result = await _service.GetPagedClientsAsync(1, 2);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.TotalCount);
        Assert.Equal(2, result.Items.Count());
    }
}
