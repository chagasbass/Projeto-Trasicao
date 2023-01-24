using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoTransicao.Extensions.Entities;

namespace ProjetoTransicao.Infra.Data.Bases
{
    public abstract class BaseMappings<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public abstract void Configure(EntityTypeBuilder<T> builder);

        public void ConfigurarEntidadeBase(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("ID")
                .IsRequired();

            builder.Property(c => c.DataCadastro)
                .HasColumnName("DATA_CADASTRO")
                .IsRequired();
        }
    }
}
