using AutoFixture;
using Moq;
using ProjetoTransicao.Application.Contextos.Categorias.Services;
using ProjetoTransicao.Domain.Contextos.Categorias.Entities;
using ProjetoTransicao.Domain.Contextos.Categorias.Repositories;
using ProjetoTransicao.Extensions.Logs.Services;
using ProjetoTransicao.Shared.Entities;
using ProjetoTransicao.Shared.Enums;
using ProjetoTransicao.Shared.Notifications;
using ProjetoTransicao.Tests.Application.Fakes;

namespace ProjetoTransicao.Tests.Application.Services;

public class CategoriaApplicationServicesTests
{
    private readonly Mock<ILogServices> _logServices;
    private readonly Mock<INotificationServices> _notificationServices;
    private readonly Mock<ICategoriaWriteRepository> _categoriaWriteRepository;
    private readonly Mock<ICategoriaReadRepository> _categoriaReadRepository;

    private readonly Fixture _fixture;
    private readonly SalvarCategoriaDtoFake _salvarCategoriaDtoFake;
    private readonly AtualizarCategoriaDtoFake _atualizarCategoriaDtoFake;

    public CategoriaApplicationServicesTests()
    {
        _logServices = new Mock<ILogServices>();
        _notificationServices = new Mock<INotificationServices>();
        _categoriaReadRepository = new Mock<ICategoriaReadRepository>();
        _categoriaWriteRepository = new Mock<ICategoriaWriteRepository>();

        _fixture = new Fixture();
        _salvarCategoriaDtoFake = new SalvarCategoriaDtoFake(_fixture);
        _atualizarCategoriaDtoFake = new AtualizarCategoriaDtoFake(_fixture);
    }

    [Fact]
    [Trait("CategoriaApplicationServices", "Salvar nova categoria - validação de contrato")]
    public async Task Deve_Retornar_Notificação_E_StatusCode_422_Quando_Contrato_Para_Salvar_Nova_Categoria_For_Inválido()
    {
        //Arrange
        var salvarCategoriaDto = _salvarCategoriaDtoFake.GerarEntidadeInvalida();
        var statusCode = StatusCodeOperation.BusinessError;

        _notificationServices.Setup(x => x.HasNotifications()).Returns(true);
        _notificationServices.Setup(x => x.StatusCode).Returns(statusCode);

        //Act

        var service = new CategoriaApplicationServices(_logServices.Object,
                                                       _notificationServices.Object,
                                                       _categoriaWriteRepository.Object,
                                                       _categoriaReadRepository.Object);

        var commandResult = (CommandResult)await service.SalvarCategoriasAsync(salvarCategoriaDto);

        //Assert
        Assert.True(_notificationServices.Object.HasNotifications());
        Assert.Equal(statusCode, _notificationServices.Object.StatusCode);
        Assert.False(commandResult.Success);
    }

    [Fact]
    [Trait("CategoriaApplicationServices", "Salvar nova categoria - existência de dados")]
    public async Task Deve_Retornar_Notificação_E_StatusCode_400_Quando_Nova_Categoria_Já_Existir()
    {
        //Arrange
        var salvarCategoriaDto = _salvarCategoriaDtoFake.GerarEntidadeValida();
        Categoria categoria = salvarCategoriaDto;
        var statusCode = StatusCodeOperation.BadRequest;

        _notificationServices.Setup(x => x.HasNotifications()).Returns(true);
        _notificationServices.Setup(x => x.StatusCode).Returns(statusCode);
        _categoriaReadRepository.Setup(x => x.ListarCategoriasAsync(categoria.Nome)).ReturnsAsync(true);

        //Act
        var service = new CategoriaApplicationServices(_logServices.Object,
                                                       _notificationServices.Object,
                                                       _categoriaWriteRepository.Object,
                                                       _categoriaReadRepository.Object);

        var commandResult = (CommandResult)await service.SalvarCategoriasAsync(salvarCategoriaDto);

        //Assert
        Assert.True(_notificationServices.Object.HasNotifications());
        Assert.Equal(statusCode, _notificationServices.Object.StatusCode);
        Assert.False(commandResult.Success);
    }

    [Fact]
    [Trait("CategoriaApplicationServices", "Salvar nova categoria - inserção de nova categoria")]
    public async Task Deve_Salvar_Nova_Categoria()
    {
        //Arrange
        var salvarCategoriaDto = _salvarCategoriaDtoFake.GerarEntidadeValida();
        Categoria categoria = salvarCategoriaDto;
        var statusCode = StatusCodeOperation.Created;

        _notificationServices.Setup(x => x.HasNotifications()).Returns(false);
        _notificationServices.Setup(x => x.StatusCode).Returns(statusCode);
        _categoriaReadRepository.Setup(x => x.ListarCategoriasAsync(categoria.Nome)).ReturnsAsync(false);
        _categoriaWriteRepository.Setup(x => x.SalvarCategoriaAsync(categoria));

        //Act
        var service = new CategoriaApplicationServices(_logServices.Object,
                                                       _notificationServices.Object,
                                                       _categoriaWriteRepository.Object,
                                                       _categoriaReadRepository.Object);

        var commandResult = (CommandResult)await service.SalvarCategoriasAsync(salvarCategoriaDto);

        //Assert
        Assert.False(_notificationServices.Object.HasNotifications());
        Assert.Equal(statusCode, _notificationServices.Object.StatusCode);
        Assert.NotNull(commandResult.Data);
        Assert.True(commandResult.Success);
    }
}
