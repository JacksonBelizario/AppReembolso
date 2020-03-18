using App.Web.Maps;
using Microsoft.EntityFrameworkCore;

namespace App.Web.Models
{
    public class SistemaDbContext : DbContext
    {
        public SistemaDbContext(DbContextOptions<SistemaDbContext> options) : base(options)
        {
            Database.Migrate();
        }
        public DbSet<Solicitacao> Solicitacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new SolicitacaoMap(modelBuilder.Entity<Solicitacao>());
        }
    }
}
