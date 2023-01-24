using ProjetoTransicao.Domain.Contextos.Categorias.Entities;

namespace ProjetoTransicao.Application.Contextos.Categorias.Dtos;

public class SalvarCategoriaDto
{
    public string? Nome { get; set; }
    public string? Descricao { get; set; }

    public SalvarCategoriaDto() { }

    public static implicit operator Categoria(SalvarCategoriaDto dto)
    {
        var categoria = new Categoria(dto.Nome, dto.Descricao);
        categoria.ValidarEntidade();

        return categoria;
    }
}