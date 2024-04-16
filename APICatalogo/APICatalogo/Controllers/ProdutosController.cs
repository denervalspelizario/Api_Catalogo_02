using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {

        // INJEÇÃO DE DEPENDENCIA CONTEXT
        private readonly AppDbContext _context;

        // CONSTRUTOR QUE PEGA O CONTEXT
        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        // GET CHAMA PRIMEIRO PRODUTO DA LISTA
        // /api/produtos/primeiro
        [HttpGet("primeiro")]
        //[HttpGet("{valor:alpha:length(5)}")]  // RESTRIÇÃO SO ACEITA VALOR ALFANUMERICOS DE A - Z
                                                //DE TAMANHO 5
        public async Task<ActionResult <Produto>> GetPrimeiro() // TRANSFORMANDO EM ASSINCRONO  async + Task
        {
            // USANDO CONTEXT PEGANDO PRIMEIRO ITEM DA LISTA PRODUTOS
            var produto =  await _context.Produtos.FirstOrDefaultAsync(); // mudei FirstOrDefault por FirstOrDefaultAsync
                                                                          // além claro do await  
            //TRATANDO ERRO
            if(produto is null)
            {
                return  NotFound("Produto não encontrado");
            }

            // OK METODO QUE RETORNA STATUS 200 E PRODUTO
            return   Ok(produto);

        }


        //  GET QUE CHAMA LISTA DE TODOS OS PRODUTOS
        // /api/produtos
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            // USANDO O _CONTEXT PEGA A LISTA DE TODOS OS PRODUTOS
            var produtos = _context.Produtos.ToList();


            // TRATAMENTO DE ERRO
            if (produtos is null)
            {
                return NotFound("Produtos não encontrados");
            }

            // RETORNANDO LISTA DE PRODUTOS
            return produtos;
        }



        // GET QUE CHAMA UM PRODUTO ESPECIFICO
        // /api/produtos/id
        // PARAMETRO TIPO INT E QUE SEJA NO MINIMO 1 (RESTRIÇÃO DE ROTA)
        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public async Task<ActionResult<Produto>> Get(int id, [BindRequired] string nome) // precisa informar na url nome
        {                                                                                // /api/produtos/1?nome=Suco
            var nomeProduto = nome; 

            // USANDO _CONTEXTO PARA PEGAR PRIMEIRO PRODUTO BASEADO NA ID
            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.ProdutoId == id);


            // TRATAMENTO DE ERRO
            if (produto is null)
            {
                return NotFound("Produto não encontrado");
            }

            // RETORNO DO PRODUTO
            return produto;
        }



        // metodo tipo POST
        // /api/produtos
        [HttpPost]
        public ActionResult Post(Produto produto) // vai receber na request um tipo Produto
        {

            // validação
            if (produto is null)
            {
                // usando metodo do ActionResult o BadRequest
                return BadRequest();
            }


            // ADD O PRODUTO
            _context.Produtos.Add(produto);


            // SALVANDO ALTERAÇÃO NO BD
            _context.SaveChanges();



            //  CreatedAtRouteResult que retorna o status 201 created e os dados que foram adicionados
            // "ObterProduto" = nome da rota, id que foi incluido {id = produto.ProdutoId}, objeto que foi adicionado = produto
            return new CreatedAtRouteResult("ObterProduto",
                new { id = produto.ProdutoId }, produto);

        }


        // PUT (ALTERACAO) DE UM PRODUTO BASEADO NA ID PASSSA DA REQUEST
        // /api/produtos/id
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            // validação do id
            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }


            // usando a instancia do contexto(_context) estou informando que 
            // seu estado esta sendo modificado, até aqui só esta sendo feito na memoria
            _context.Entry(produto).State = EntityState.Modified;

            // agora precisa persistir no banco de dados para de fato
            _context.SaveChanges();


            // METODO OK GERA STATUS 200 E MOSTRA PRODUTO QUE FOI ALTERADO
            return Ok(produto);
        }

        // DELETE DE UM PRODUTO BASEADO NO ID PASSADO NA REQUEST
        // /api/produtos/id
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {

            // USANDO _CONTEXTO PARA PEGAR PRIMEIRO PRODUTO BASEADO NA ID
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            // ALTERNATIVA PARA BUSCAR PRODUTO
            var produto02 = _context.Produtos.Find(id);

            // TRATAMENTO DE ERRO
            if(produto is null)
            {
                return NotFound("Produto não localizado");
            }

            // REMOVENDO PRODUTO
            _context.Produtos.Remove(produto);

            // SALVANDO ALTERACAO NO BD
            _context.SaveChanges();

            // METODO OK GERA STATUS 200 E MOSTRA PRODUTO QUE FOI EXCLUIDO
            return Ok(produto);
        }
    }
}
