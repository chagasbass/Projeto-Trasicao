using ProjetoTransicao.Domain.Contextos.Categorias.Entities;

namespace ProjetoTransicao.Application.Contextos.Categorias.Dtos;

public class ListarCategoriasDto
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public DateTime DataCadastro { get; set; }

    public ListarCategoriasDto() { }


    public static implicit operator ListarCategoriasDto(Categoria categoria)
    {
        if (categoria is not null)
        {
            return new ListarCategoriasDto
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                Descricao = categoria.Descricao,
                DataCadastro = categoria.DataCadastro
            };
        }

        return default;
    }

    public static IEnumerable<ListarCategoriasDto> RetornarDadosDeCategorias(IEnumerable<Categoria> categorias)
    {
        var listagemDeCategoriasDto = new List<ListarCategoriasDto>();

        if (categorias.Any())
        {
            foreach (var categoria in categorias)
            {
                ListarCategoriasDto listarCategoriaDto = categoria;
                listagemDeCategoriasDto.Add(listarCategoriaDto);
            }
        }

        return listagemDeCategoriasDto;
    }
}
