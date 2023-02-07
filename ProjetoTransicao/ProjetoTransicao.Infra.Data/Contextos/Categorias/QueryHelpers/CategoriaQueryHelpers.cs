
namespace ProjetoTransicao.Infra.Data.Contextos.Categorias.QueryHelpers;

public static class CategoriaQueryHelpers
{
    public static string ListarCategorias()
    {
        var query = new StringBuilder();
        query.AppendLine(" SELECT ID AS Id, NOME AS Nome, DESCRICAO AS Descricao, DATA_CADASTRO AS DataCadastro FROM CATEGORIAS");

        return query.ToString();
    }

    public static string ListarCategoriasPorId()
    {
        var query = new StringBuilder();
        query.AppendLine(" SELECT ID AS Id, NOME AS Nome, DESCRICAO AS Descricao, DATA_CADASTRO AS DataCadastro FROM CATEGORIAS");
        query.AppendLine(" WHERE ID = @id");

        return query.ToString();
    }

    public static string ListarCategoriasPorNome()
    {
        var query = new StringBuilder();
        query.AppendLine(" SELECT ID AS Id, NOME AS Nome, DESCRICAO AS Descricao, DATA_CADASTRO AS DataCadastro FROM CATEGORIAS");
        query.AppendLine(" WHERE UPPER(RTRIM(LTRIM(NOME)))  = @nome");

        return query.ToString();
    }
}
