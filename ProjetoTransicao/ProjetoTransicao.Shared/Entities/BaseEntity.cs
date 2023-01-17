namespace ProjetoTransicao.Extensions.Entities;

public abstract class BaseEntity : Notifiable<Notification>
{
    public Guid Id { get; private set; }
    public DateTime DataCadastro { get; private set; }

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
        DataCadastro = DateTime.Now;
    }

    public override string ToString() => $"{GetType().Name} [Id= {Id}]";

    public abstract void ValidarEntidade();
}