using ProjetoTransicao.Application.Contextos.Categorias.Dtos;
using ProjetoTransicao.Shared.Entities;

namespace ProjetoTransicao.Application.Contextos.Categorias.Contracts
{
    public interface ICategoriaApplicationServices
    {
        Task<ICommandResult> SalvarCategoriasAsync(SalvarCategoriaDto salvarCategoriaDto);
        Task<ICommandResult> AtualizarCategoriasAsync(AtualizarCategoriaDto atualizarCategoriaDto);
        Task<ICommandResult> ExcluirCategoriasAsync(Guid id);
        Task<ICommandResult> ListarCategoriasAsync(Guid id);
        Task<ICommandResult> ListarCategoriasAsync();
    }
}