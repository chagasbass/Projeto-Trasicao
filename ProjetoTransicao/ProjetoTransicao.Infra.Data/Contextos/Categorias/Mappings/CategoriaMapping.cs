namespace ProjetoTransicao.Infra.Data.Contextos.Categorias.Mappings;

public class CategoriaMapping : BaseMappings<Categoria>
{
    public override void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("CATEGORIAS");

        builder.Property(c => c.Nome)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(c => c.Descricao)
               .HasMaxLength(50)
               .IsRequired();

        builder.Ignore(x => x.Notifications);

        base.ConfigurarEntidadeBase(builder);
    }
}
