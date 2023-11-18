using Microsoft.EntityFrameworkCore;
using PortalColaborador.Models;

namespace PortalColaborador.Data;
public class AppDataContext : DbContext
{
    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
    {

    }

    //Classes que vão se tornar tabelas no banco de dados
    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<Jornada> jornadas { get; set; }
    public DbSet<Usuario> usuarios { get; set; }
}