namespace ProjetoTransicao.Application.Contextos.Categorias.Dtos;

public class AtualizarCategoriaDto
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }

    public AtualizarCategoriaDto() { }
}