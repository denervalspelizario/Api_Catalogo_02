using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {

        // pegando o contexto  
        private readonly AppDbContext _context;

        // 05 prorpiedade somente leitura de configuração
        // acessa DADOS appsettings.json usando metodo IConfiguration
        private readonly IConfiguration _configuration; 


        //  construtor que recebe o contexto 
        //  05 RECEBE PROPRIEDADE PARA LEITURA DE _CONFIGURATION
        public CategoriasController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration; // 05 construtor de configuração ver em appsettings.json
        }
        
        // 05 TESTE GET DO CONSTRUTOR DE CONFIGURAÇÃO
        [HttpGet("LerArquivoConfiguracao")] // DEFININDO A ROTA
        public string GetValores()
        {
            // PEGANDO OS DADOS ATRAVEZ DE _CONFIGURATION
            var valor1 = _configuration["chave1"];
            var valor2 = _configuration["chave2"];

            var secao1 = _configuration["secao1:chave2"];


            // RETORNANDO STRING COM OS DADOS CONCATENADOS
            return $"Chave1 = {valor1} \nChave2 = {valor2} \nSeçao1 => Chave2 = {secao1}";
        }



        // GET USANDO FROMSERVICES ANTES DO NET 7
        [HttpGet("UsandoFromServices/{nome}")]
        public ActionResult<string> GetSaudacaoFromServices(
            [FromServices] IMeuServico meuServico, string nome) 
        {
            return meuServico.Saudacao(nome);
        }

        // GET DEPOIS DO NET 7 SEM USAR FROM SERVICES QUE É O PADRÃO ATUAL
        [HttpGet("SemUsarFromServices/{nome}")]
        public ActionResult<string> GetSaudacaoSemFromServices(
             IMeuServico meuServico, string nome)
        {
            return meuServico.Saudacao(nome);
        }


        // GET QUE CHAMA LISTA DE TODOS AS CATEGORIAS E PRODUTOS
        [HttpGet("produtos")] 
        public ActionResult <IEnumerable<Categoria>> GetCategoriasProdutos()
        {

            // LISTANDO TODAS AS CATEGORIAS E PRODUTOS
            // ALÉM DE CATEGORIAS, USANDO O MÉTODO INCLUDE ADICIONA A LISTA OS PRODUTOS TB
            var categoriasEProdutos = _context.Categorias.Include(p => p.Produtos).ToList();


            // TRATANDO ERRO
            if (categoriasEProdutos is null)
            {
                return NotFound("Categorias não encontradas");
            }

            // RETORNA LISTA DE CATEGORIAS E PRODUTOS
            return categoriasEProdutos;
        }


        // GET QUE CHAMA LISTA DE TODAS AS CATEGORIAS
        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {

            // USANDO O _CONTEXT PEGA A LISTA DE TODAS AS CATEGORIAS
            var categorias = _context.Categorias.ToList();

            // 02 AsNoTracking METODO QUE GERA UMA CONSULTA MAIS OTIMIZADA
            // PORÉM SÓ USE SE CASO NÃO FOR ALTERAR OS ARQUIVOS
            var categoriasOtimizada = _context.Categorias.AsNoTracking().ToList();

            // TRATAMENTO DE ERRO
            if (categorias is null)
            {
                return NotFound("Categorias não encontradas");
            }

            // RETORNA LISTA DE CATEGORIAS
            return categorias;
        }

        // GET QUE CHAMA UMA CATEGORIA ESPECIFICA
        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {

            // USANDO _CONTEXTO PARA PEGAR PRIMEIRO PRODUTO BASEADO NA ID
            var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);


            // TRATAMENTO DE ERRO
            if (categoria is null)
            {
                return NotFound("Categoria não encontrado");
            }

            // METODO OK GERA STATUS 200 E IMPRIMI CATEGORIA CONSULTADA
            return Ok(categoria);
        }


        // POST QUE CRIA CATEGORIA
        [HttpPost]
        public ActionResult Post(Categoria categoria) // RECEBE NO BODY UM TIPO CATEGORIA
        {

            // TRATAMENTO DE ERRO
            if (categoria is null)
            {
                // usando metodo do ActionResult o BadRequest
                return BadRequest();
            }


            // USANDO O _CONTEXTO ADICIONANDO CATEGORIA
            _context.Categorias.Add(categoria);


            // SALVANDO DADOS NO BD
            _context.SaveChanges();


            // CreatedAtRouteResult que retorna o status 201 created e os dados que foram adicionados
            // "ObterCategoria" = nome da rota, id que foi incluido {id = categoria.CategoriaId}, objeto que foi adicionado = categoria
            return new CreatedAtRouteResult("ObterCategoria",
                new { id = categoria.CategoriaId }, categoria);

        }

        // PUT QUE ALTERA UMA CATEGORIA
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            // TRATAMENTO DE ERRO
            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }


            // usando a instancia do contexto(_context) estou informando que 
            // seu estado esta sendo modificado, até aqui só esta sendo feito 
            // na memoria
            _context.Entry(categoria).State = EntityState.Modified;

            // SALVANDO OS DADOS NO BD
            _context.SaveChanges();


            // METODO OK GERA STATUS 200 E MOSTRA PRODUTO QUE FOI ALTERADO
            return Ok(categoria);
        }


        // DELETE DE UM PRODUTO BASEADO NO ID PASSADO NA REQUEST
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            // USANDO _CONTEXTO PARA PEGAR PRIMEIRA CATEGORIA BASEADO NA ID
            var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

            // UMA ALTERNATIVA AO FIRSTORDEFAULT
            var categoria02 = _context.Categorias.Find(id);

            // TRATAMENTO DE ERRO
            if (categoria is null)
            {
                return NotFound("Categoria não encontrada");
            }

            // REMOÇÃO DA CATEGORIA 
            _context.Categorias.Remove(categoria);

            // SALVANDO NO DB
            _context.SaveChanges();

            // METODO OK GERA STATUS 200 E MOSTRA CATEGORIA QUE FOI EXCLUIDO
            return Ok(categoria);
        }
    }
}
