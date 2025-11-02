using CarteiraSimples.Data; // permite acessar o AppDbContext, que conecta com o banco
using CarteiraSimples.Models; // dá acesso à classe 'Transacao', nosso modelo de dados
using Microsoft.AspNetCore.Mvc; // importa as classes para criar controllers e endpoints
using Microsoft.EntityFrameworkCore; // permite usar funções do EF

//tem toda a explicacao no material
namespace CarteiraSimples.Controllers
{
    [ApiController] 
    [Route("api/[controller]")] 
    public class TransacoesController : ControllerBase
    {
        private readonly AppDbContext _context; // variável privada para guardar o objeto AppDbContext

        // o ASP.NET cria automaticamente uma instância de AppDbContext e injeta aqui quando o controller é usado
        public TransacoesController(AppDbContext context)
        {
            _context = context; // guarda essa instância para ser usada dentro das funções
        }

        // GET: api/transacoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transacao>>> GetTransacoes()
        {
            return await _context.Transacoes.ToListAsync();
        }

    }
}
