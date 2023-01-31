using AutoFixture;
using ProjetoTransicao.Application.Contextos.Categorias.Dtos;
using ProjetoTransicao.Tests.Base;

namespace ProjetoTransicao.Tests.Application.Fakes;

public class AtualizarCategoriaDtoFake : IFake<AtualizarCategoriaDto>
{
    private readonly Fixture _fixture;

    public AtualizarCategoriaDtoFake(Fixture fixture)
    {
        _fixture = fixture;
    }
    public AtualizarCategoriaDto GerarEntidadeInvalida()
    {
        var dto = _fixture.Build<AtualizarCategoriaDto>()
                          .Without(x => x.Descricao)
                          .Do(x =>
                          {
                              x.Descricao = string.Empty;
                          })
                          .Create();

        return dto;
    }

    public AtualizarCategoriaDto GerarEntidadeValida()
    {
        var dto = _fixture.Build<AtualizarCategoriaDto>()
                          .Without(x => x.Descricao)
                          .Without(x => x.Nome)
                          .Do(x =>
                          {
                              x.Nome = "Nova Categoria";
                              x.Descricao = "Nova Descrição";
                          })
                          .Create();

        return dto;
    }
}
