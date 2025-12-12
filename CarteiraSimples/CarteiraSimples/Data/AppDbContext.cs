using CarteiraSimples.Models;
using Microsoft.EntityFrameworkCore;

//classe que representa a ponte com o banco de dados. É uma classe genérica. No caso, preciso criar uma subclasse para passar para a classe base as entidades e configurações que pertencem ao projeto
namespace CarteiraSimples.Data
{
    public class AppDbContext : DbContext
    {
        //  construtor da minha classe. Mecanismo que faz a configuracao para o EF CORE saber como se conectar ao banco de dados. Toda vez que alguém quiser criar um 'AppDbContext' é obrigatorio me entregar as opções de conexão (options) para eu passar elas para a classe base (DbContext);
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Transacao> Transacoes => Set<Transacao>(); //"Existe uma tabela no banco chamada Transacoes que corresponde a classe Transacao"
    }

}
