namespace ProjetoTransicao.Domain.Contextos.Categorias.Repositories;

public interface ICategoriaWriteRepository
{
    Task SalvarCategoriaAsync(Categoria categoria);
    Task AtualizarCategoriasAsync(Categoria categoria);
    Task ExcluirCategoriasAsync(Categoria categoria);
}
