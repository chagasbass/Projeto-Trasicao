using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using ProjetoTransicao.Domain.Contextos.Categorias.Entities;
using System.Reflection;

namespace ProjetoTransicao.Infra.Data.DataContexts;

public class WriteDataContext : DbContext
{
    public DbSet<Categoria> Categorias { get; set; }

    public WriteDataContext(DbContextOptions<WriteDataContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
            .LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.Ignore<Notification>();

        base.OnModelCreating(modelBuilder);
    }
}
