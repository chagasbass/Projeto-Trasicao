using AutoFixture;
using ProjetoTransicao.Application.Contextos.Categorias.Dtos;
using ProjetoTransicao.Tests.Base;

namespace ProjetoTransicao.Tests.Application.Fakes;

public class SalvarCategoriaDtoFake : IFake<SalvarCategoriaDto>
{
    private readonly Fixture _fixture;

    public SalvarCategoriaDtoFake(Fixture fixture)
    {
        _fixture = fixture;
    }

    public SalvarCategoriaDto GerarEntidadeInvalida()
    {
        var dto = _fixture.Build<SalvarCategoriaDto>()
                          .Without(x => x.Descricao)
                          .Do(x =>
                          {
                              x.Descricao = string.Empty;
                          }).Create();
        return dto;
    }

    public SalvarCategoriaDto GerarEntidadeValida()
    {
        var dto = _fixture.Build<SalvarCategoriaDto>()
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
