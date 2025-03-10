using EmprestimoLivros.Models;
using EmprestimoLivros.Models.Entities;
using EmprestimoLivros.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace EmprestimoLivros.Data.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}
    
    public DbSet<EmprestimosModel>  Emprestimos { get; set; }
    public DbSet<UsuarioModel>  Usuarios { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmprestimosModel>()
            .Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()");
        
        modelBuilder.Entity<UsuarioModel>()
            .Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()");
    }

}

