using CarteiraSimples.Data; // permite acessar o AppDbContext, que conecta com o banco
using CarteiraSimples.Models; // dá acesso à classe 'Transacao', nosso modelo de dados
using Microsoft.AspNetCore.Mvc; // importa as classes para criar controllers e endpoints
using Microsoft.EntityFrameworkCore; // permite usar funções do EF

namespace CarteiraSimples.Controllers
{
    //caracterizamos a classe como um controller da API
    [ApiController]
    [Route("api/[controller]")]


    public class DashBoardController : ControllerBase //a classe criada herda de ControllerBase, que eh uma classe do ASP.NET que ja posui tudo que um controller precisa, como métodos, etc
    {
        
        private readonly AppDbContext _context; //estamos guardando uma ponte de acesso com o banco (private - so pode ser usado dentro do controller - readonly - so pode receber valor no construtor)

        
        public DashBoardController (AppDbContext context) //qnd eu crio um controller, eu preciso passar uma conexao com o banco ativo, para ele ter a ponte com o banco. Agora, essa classe pode fazer consultar ao banco usando _context.Transacoes
        {
            _context = context;
        }

         //quando o front acessar /api/dashboard/resumo, esse metodo sera chamado. Colocamos isso em cima do metodos
        [HttpGet("resumo")]

        //publica, async (assincrona) pq espera uma requisicao do banco, Task porque retorna uma tarefa assincrona, do tipo 'ActionResult' pq o resultado vai ser uma resposta HTTP
        public async Task<IActionResult> GetResumo() 
        {
            //vou no banco, pego todos os registros da tabela 'Transacoes', espera a resposta sem travar a AP(await e ToListAsync) e guarda o resultado em uma lista 'transacoes'
            var transacoes = await _context.Transacoes.ToListAsync();

            var totalReceitas = transacoes
                //expressao lambda. uma forma curta de escrever "para cada transacao, pegue o campo tipo/valor dela
                .Where(t => t.Tipo.ToLower() == "receita")
                .Sum(t => t.Valor);


            var totalDespesas = transacoes
                .Where(t => t.Tipo.ToLower() == "despesa")
                .Sum(t => t.Valor);

            var saldo = (totalReceitas - totalDespesas);

            //'Ok' eh um metodo da classe 'ControllerBase', que a nossa classe tem acesso pelo fato de herdar a classe ': ControllerBase'. Ele cria uma resposta HTTP com status 200 (sucesso) e um corpo JSON com oq vc passar dentro.
            return Ok(new
            {
                saldo,
                totalReceitas,
                totalDespesas
            });
        }

        
        [HttpGet("categorias")]
        public async Task<IActionResult> GetCategorias()
        {
            //transacoes armazena todas as transacoes do banco em formato de lista
            var transacoes = await _context.Transacoes.ToListAsync();
            var categorias = transacoes

                //basicamente to filtrando as transacoes que sao "despesas", agrupando-as pelas categorias/GRUPOS (como usamos o GroupBy, cada brupo gerado tem uma prop chamada Key, que representa o valor pelo qual agrupamos, no caso, 'Categoria'), e criando um objeto que retorna a categoria e o total (soma dos valores). O '.ToList()' eh para ele executar a consulta de verdade no banco.

                .Where(t => t.Tipo.ToLower() == "despesa")
                .GroupBy(t => t.Categoria)
                .Select(g => new
                {
                    categoria = g.Key,
                    total = g.Sum(t => t.Valor),
                })
                .ToList();

            return Ok(categorias);

        }

        [HttpGet("mensal")]
        public async Task<IActionResult> GetResumoMensal()
        {
            var transacoes = await _context.Transacoes.ToListAsync();
            var mensal = transacoes

                //vamos agrupar pelo ano e mes, criar um JSON com ano, mes, receita e despesa, ordernar pelo ano, e dentro de cada ano, ele ordena pelo mes (.ThenBy)

                .GroupBy(t => new { t.Data.Year, t.Data.Month })
                .Select(g => new
                {
                    ano = g.Key.Year,
                    mes = g.Key.Month,
                    receitas = g.Where(t => t.Tipo.ToLower() == "receita").Sum(t => t.Valor),
                    despesas = g.Where(t => t.Tipo.ToLower() == "despesa").Sum(t => t.Valor)
                })
                .OrderBy(x => x.ano)
                .ThenBy(x => x.mes)
                .ToList();
            return Ok(mensal);
        }
    
    }


}
