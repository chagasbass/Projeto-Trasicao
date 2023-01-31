using ProjetoTransicao.Domain.Contextos.Categorias.Entities;

namespace ProjetoTransicao.Tests.Domain.Contextos.Categorias.Entities;

public class CategoriaTests
{
    [Fact]
    [Trait("Categoria", "Validação de entidade")]
    public void Deve_Retornar_Notificação_Quando_Categoria_For_Inválida()
    {
        //Arrange ,Act

        var categoria = new Categoria("", "descrição");

        //Assert
        Assert.True(categoria.Notifications.Any());
    }

    [Fact]
    [Trait("Categoria", "Validação de entidade")]
    public void Nâo_Deve_Retornar_Notificação_Quando_Categoria_For_Válida()
    {
        //Arrange,Act
        var categoria = new Categoria("nova Categoria", "Descricao da Categoria");

        //Assert
        Assert.False(categoria.Notifications.Any());
    }
}
