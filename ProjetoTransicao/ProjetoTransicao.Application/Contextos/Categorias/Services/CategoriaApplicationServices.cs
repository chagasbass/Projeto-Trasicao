using Flunt.Notifications;
using ProjetoTransicao.Application.Contextos.Categorias.Contracts;
using ProjetoTransicao.Application.Contextos.Categorias.Dtos;
using ProjetoTransicao.Domain.Contextos.Categorias.Entities;
using ProjetoTransicao.Domain.Contextos.Categorias.Repositories;
using ProjetoTransicao.Extensions.Logs.Services;
using ProjetoTransicao.Shared.Entities;
using ProjetoTransicao.Shared.Enums;
using ProjetoTransicao.Shared.Notifications;

namespace ProjetoTransicao.Application.Contextos.Categorias.Services;

public class CategoriaApplicationServices : ICategoriaApplicationServices
{
    private readonly ILogServices _logServices;
    private readonly INotificationServices _notificationServices;
    private readonly ICategoriaWriteRepository _categoriaWriteRepository;
    private readonly ICategoriaReadRepository _categoriaReadRepository;

    public CategoriaApplicationServices(ILogServices logServices,
                                        INotificationServices notificationServices,
                                        ICategoriaWriteRepository categoriaWriteRepository,
                                        ICategoriaReadRepository categoriaReadRepository)
    {
        _logServices = logServices;
        _notificationServices = notificationServices;
        _categoriaWriteRepository = categoriaWriteRepository;
        _categoriaReadRepository = categoriaReadRepository;
    }

    public async Task<ICommandResult> AtualizarCategoriasAsync(AtualizarCategoriaDto atualizarCategoriaDto)
    {
        var categoriaExistente = await _categoriaReadRepository.ListarCategoriasAsync(atualizarCategoriaDto.Id);

        if (categoriaExistente is null)
        {
            _notificationServices.AddNotification(new Notification("categoria", "A categoria não existe"), StatusCodeOperation.BadRequest);
            return new CommandResult(_notificationServices.GetNotifications().ToList(), false, "Erros na operação");
        }

        categoriaExistente.AlterarNome(atualizarCategoriaDto.Nome)
                          .AlterarDescricao(atualizarCategoriaDto.Descricao);

        categoriaExistente.ValidarEntidade();
        categoriaExistente.PrepararDados();

        if (!categoriaExistente.IsValid)
        {
            _notificationServices.AddNotifications(categoriaExistente.Notifications, StatusCodeOperation.BusinessError);
            return new CommandResult(categoriaExistente.Notifications.ToList(), false, "Erros na operação.");

        }

        await _categoriaWriteRepository.AtualizarCategoriasAsync(categoriaExistente);

        _notificationServices.AddStatusCode(StatusCodeOperation.NoContent);

        return new CommandResult(categoriaExistente.ToString(), true, "Categoria atualizada com sucesso");
    }

    public async Task<ICommandResult> ExcluirCategoriasAsync(Guid id)
    {
        var categoriaExiste = await _categoriaReadRepository.ListarCategoriasAsync(id);

        if (categoriaExiste is null)
        {
            _notificationServices.AddNotification(new Notification("categoria", "A categoria não existe."), StatusCodeOperation.NotFound);
            return new CommandResult(_notificationServices.GetNotifications().ToList(), false, "Erros na operação.");
        }

        await _categoriaWriteRepository.ExcluirCategoriasAsync(categoriaExiste);

        _notificationServices.AddStatusCode(StatusCodeOperation.OK);

        return new CommandResult(categoriaExiste.ToString(), true, "Operação efetuada com sucesso");
    }

    public async Task<ICommandResult> ListarCategoriasAsync(Guid id)
    {
        if (id.Equals(Guid.Empty))
        {
            _notificationServices.AddNotification(new Notification("id", "O indentificador de categoria é inválido."), StatusCodeOperation.BusinessError);

            return new CommandResult(_notificationServices.GetNotifications().ToList(), false, "Houve problemas na chamada.");
        }

        var categoriaEncontrada = await _categoriaReadRepository.ListarCategoriasAsync(id);

        if (categoriaEncontrada is null)
        {
            _notificationServices.AddNotification(new Notification("id", "A Categoria não foi encontrada."), StatusCodeOperation.NotFound);

            return new CommandResult(categoriaEncontrada, true, "A pesquisa não retornou resultados.");
        }

        ListarCategoriasDto categoriaDto = categoriaEncontrada;

        return new CommandResult(categoriaDto, true, "Sucesso na chamada.");
    }

    public async Task<ICommandResult> ListarCategoriasAsync()
    {
        var categorias = await _categoriaReadRepository.ListarCategoriasAsync();

        if (!categorias.Any())
        {
            _notificationServices.AddNotification(new Notification("categorias", "A pesquisa não retornou resultados."), StatusCodeOperation.NotFound);

            var categoriasDtoVazias = ListarCategoriasDto.RetornarDadosDeCategorias(categorias);

            return new CommandResult(categoriasDtoVazias, true, "A pesquisa não retornou resultados.");
        }

        var categoriasDto = ListarCategoriasDto.RetornarDadosDeCategorias(categorias);

        return new CommandResult(categoriasDto, true, "Sucesso na chamada.");
    }

    public async Task<ICommandResult> SalvarCategoriasAsync(SalvarCategoriaDto salvarCategoriaDto)
    {
        Categoria novaCategoria = salvarCategoriaDto;

        /*Recebe contrato
         *se contrato invalido , retorna notificao e status code 422
         *faz listagem de categoria
         *se categoria existe, retorna notificacao
         *salva categoria 
         */

        if (!novaCategoria.IsValid)
        {
            _notificationServices.AddNotifications(novaCategoria.Notifications, StatusCodeOperation.BusinessError);
            return new CommandResult(novaCategoria.Notifications.ToList(), false, "Erros na operação.");
        }

        var categoriaExiste = await _categoriaReadRepository.ListarCategoriasAsync(novaCategoria.Nome);

        if (categoriaExiste)
        {
            _notificationServices.AddNotification(new Notification("categoria", "A categoria já está cadastrada."), StatusCodeOperation.BadRequest);
            return new CommandResult(_notificationServices.GetNotifications().ToList(), false, "Erros na operação.");
        }

        await _categoriaWriteRepository.SalvarCategoriaAsync(novaCategoria);

        _notificationServices.AddStatusCode(StatusCodeOperation.Created);

        return new CommandResult(novaCategoria.ToString(), true, "Categoria cadastrada com sucesso");
    }
}
