using Microsoft.EntityFrameworkCore;
using PrimeiraCrudMVC.Data.Map;
using PrimeiraCrudMVC.Models;

namespace PrimeiraCrudMVC.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options): base(options) 
        {
        }   

        public DbSet<ContatoModel> Contatos { get; set; }

        public DbSet<UsuarioModel> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContatoMap());

            base.OnModelCreating(modelBuilder);
        }

    }
}
