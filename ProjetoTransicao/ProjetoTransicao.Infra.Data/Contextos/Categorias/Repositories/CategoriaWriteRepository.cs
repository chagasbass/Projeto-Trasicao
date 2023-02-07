namespace ProjetoTransicao.Infra.Data.Contextos.Categorias.Repositories;

public class CategoriaWriteRepository : ICategoriaWriteRepository
{
    private readonly WriteDataContext _context;

    public CategoriaWriteRepository(WriteDataContext context)
    {
        _context = context;
    }

    public async Task AtualizarCategoriasAsync(Categoria categoria)
    {
        _context.Update(categoria);
        await _context.SaveChangesAsync();
    }

    public async Task ExcluirCategoriasAsync(Categoria categoria)
    {
        _context.Remove(categoria);
        await _context.SaveChangesAsync();
    }

    public async Task SalvarCategoriaAsync(Categoria categoria)
    {
        await _context.AddAsync(categoria);
        await _context.SaveChangesAsync();
    }
}