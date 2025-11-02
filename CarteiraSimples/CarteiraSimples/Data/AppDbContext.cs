using CarteiraSimples.Models;
using Microsoft.EntityFrameworkCore;

namespace CarteiraSimples.Data
{
    public class AppDbContext : DbContext
    {
        //  construtor da minha classe. Mecanismo que faz a configuracao para o EF CORE saber como se conectar ao banco de dados;
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Transacao> Transacoes => Set<Transacao>();
    }

}
