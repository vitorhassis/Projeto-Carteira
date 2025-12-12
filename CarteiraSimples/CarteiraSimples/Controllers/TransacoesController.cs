using CarteiraSimples.Data; // permite acessar o AppDbContext, que conecta com o banco
using CarteiraSimples.Models; // dá acesso à classe 'Transacao', nosso modelo de dados
using Microsoft.AspNetCore.Mvc; // importa as classes para criar controllers e endpoints
using Microsoft.EntityFrameworkCore; // permite usar funções do EF

//tem toda a explicacao no material
namespace CarteiraSimples.Controllers
{
    //caracterizamos a classe como um controller da API
    [ApiController] 
    [Route("api/[controller]")]

    //a classe criada herda de ControllerBase, que eh uma classe do ASP.NET que ja posui tudo que um controller precisa, como métodos, etc
    public class TransacoesController : ControllerBase 
    {
        //estamos guardando uma ponte de acesso com o banco (private - so pode ser usado dentro do controller - readonly - so pode receber valor no construtor)
        private readonly AppDbContext _context;

        //qnd eu crio um controller, eu preciso passar uma conexao com o banco ativo, para ele ter a ponte com o banco. Agora, essa classe pode fazer consultar ao banco usando _context.Transacoes
        public TransacoesController(AppDbContext context)
        {
            _context = context; 
        }

        // GET: api/transacoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transacao>>> GetTransacoes()
        {
            return await _context.Transacoes.ToListAsync();
        }

        // GET: api/transacoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transacao>> GetTransacao(int id)
        {
            var transacao = await _context.Transacoes.FindAsync(id);

            if (transacao == null)
            {
                return NotFound();
            }

            return transacao;
        }

        // POST: api/transacoes
        [HttpPost]
        public async Task<ActionResult<Transacao>> PostTransacao(Transacao transacao)
        {
            _context.Transacoes.Add(transacao);      // adiciona o objeto recebido à tabela
            await _context.SaveChangesAsync();       // salva as alterações no banco

            // retorna a resposta HTTP 201 (Created) com a rota e o objeto criado
            return CreatedAtAction(nameof(GetTransacao), new { id = transacao.Id }, transacao);
        }


        // PUT: api/transacoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransacao(int id, Transacao transacao)
        {
            if (id != transacao.Id)
            {
                return BadRequest(); // se o id da URL for diferente do objeto recebido
            }

            _context.Entry(transacao).State = EntityState.Modified; // atualizo o status do objeto novo como modified, indicando que quando eu salvar, ele vai dar um UPDATE na entidade (objeto do banco) que possui o id passado

            try
            {
                await _context.SaveChangesAsync(); // salva as mudanças no banco
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Transacoes.Any(e => e.Id == id))
                {
                    return NotFound(); // se o id não existe no banco
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // retorna 204 (sem conteúdo), indicando sucesso na atualização
        }

        // DELETE: api/transacoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransacao(int id)
        {
            var transacao = await _context.Transacoes.FindAsync(id);
            if (transacao == null)
            {
                return NotFound(); // 404 se o ID não existir
            }

            _context.Transacoes.Remove(transacao); // marca o objeto pra exclusão
            await _context.SaveChangesAsync();     // executa o DELETE no banco

            return NoContent(); // 204 - exclusão feita com sucesso
        }
    }
}
