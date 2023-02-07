namespace ProjetoTransicao.Infra.Data.Contextos.Categorias.Repositories;

public class CategoriaReadRepository : ICategoriaReadRepository
{
    private readonly ReadDataContext _context;

    public CategoriaReadRepository(ReadDataContext context)
    {
        _context = context;
    }

    public async Task<Categoria> ListarCategoriasAsync(Guid id)
    {
        using var conexao = _context.AbrirConexao();

        var parametro = new { id };

        var categoriaEncontrada = await conexao.QueryFirstOrDefaultAsync<Categoria>(CategoriaQueryHelpers.ListarCategoriasPorId(), parametro);

        return categoriaEncontrada;

    }

    public async Task<bool> ListarCategoriasAsync(string? nome)
    {
        using var conexao = _context.AbrirConexao();

        var parametro = new { nome };

        var categoriaEncontrada = await conexao.QueryFirstOrDefaultAsync<Categoria>(CategoriaQueryHelpers.ListarCategoriasPorNome(), parametro);

        return categoriaEncontrada is not null;
    }

    public async Task<IEnumerable<Categoria>> ListarCategoriasAsync()
    {
        using var conexao = _context.AbrirConexao();

        var categoriasEncontradas = await conexao.QueryAsync<Categoria>(CategoriaQueryHelpers.ListarCategorias());

        return categoriasEncontradas;
    }
}
