using CarteiraSimples.Models;
using Microsoft.EntityFrameworkCore;

namespace CarteiraSimples.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Transacao> Transacoes => Set<Transacao>();
    }

}
