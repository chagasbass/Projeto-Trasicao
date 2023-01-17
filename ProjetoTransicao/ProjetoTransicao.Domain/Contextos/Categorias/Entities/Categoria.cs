using Flunt.Notifications;
using Flunt.Validations;
using ProjetoTransicao.Domain.Contextos.Categorias.Recursos;
using ProjetoTransicao.Extensions.Entities;

namespace ProjetoTransicao.Domain.Contextos.Categorias.Entities;

public class Categoria : BaseEntity, IEntity
{
    public string? Nome { get; private set; }
    public string? Descricao { get; private set; }

    protected Categoria() { }

    public Categoria(string? nome, string? descricao)
    {
        Nome = nome;
        Descricao = descricao;

        ValidarEntidade();
        PrepararDados();
    }
    public Categoria AlterarNome(string nome)
    {
        Nome = nome;

        return this;
    }

    public Categoria AlterarDescricao(string descricao)
    {
        Descricao = descricao;

        return this;
    }

    public void PrepararDados()
    {
        Nome = Nome?.Trim().ToUpper();
        Descricao = Descricao?.Trim().ToUpper();
    }

    public override void ValidarEntidade()
    {
        AddNotifications(new Contract<Notification>()
            .Requires()
            .IsNotNullOrEmpty(Nome, nameof(Nome), MensagensDeCategoria.NomeNaoPreenchido)
            .IsGreaterOrEqualsThan(Nome.Length, 2, nameof(Nome), MensagensDeCategoria.NomeInvalido)
            .IsLowerThan(Nome.Length, 50, nameof(Nome), MensagensDeCategoria.NomeMaiorCaracter)
            .IsNotNullOrEmpty(Descricao, nameof(Descricao), MensagensDeCategoria.DescricaoNaoPreenchida)
            .IsLowerThan(Descricao.Length, 50, nameof(Descricao), MensagensDeCategoria.DescricaoMaiorCaracter)
            .IsGreaterOrEqualsThan(Descricao.Length, 2, nameof(Descricao), MensagensDeCategoria.DescricaoInvalida));
    }

    public override string ToString() => $"Id: {Id} Nome: {Nome}  Descrição: {Descricao}";
}
