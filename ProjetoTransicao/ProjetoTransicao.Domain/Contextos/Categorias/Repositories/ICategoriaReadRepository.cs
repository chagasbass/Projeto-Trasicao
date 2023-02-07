namespace ProjetoTransicao.Domain.Contextos.Categorias.Repositories;

public interface ICategoriaReadRepository
{
    Task<Categoria> ListarCategoriasAsync(Guid id);
    Task<bool> ListarCategoriasAsync(string? nome);
    Task<IEnumerable<Categoria>> ListarCategoriasAsync();
}
