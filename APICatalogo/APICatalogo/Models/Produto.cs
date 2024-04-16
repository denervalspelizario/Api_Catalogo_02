using APICatalogo.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models;



// 03 (DATA ANOTATIONS) mapeando essa entidade para a tabela Produtos la do banco de dados
[Table("Produtos")]
// classe anemica pq só foi definido propriedades
public class Produto : IValidatableObject // 04 VALIDACAO ATRAVEZ DA INTERFACE IVALIDATIO 
{                                         // OLHAR NO FINAL DA PAGINA
    // propriedades
    // 03 (DATA ANOTATIONS)indicando com o Key que ProdutoId é uma chave primaria
    [Key] 
    public int ProdutoId { get; set; } 

    // 03 (DATA ANOTATIONS) Nome é obrigatorio, tipo string e o tamanho maximo 80 e minimo 5
    [Required(ErrorMessage ="O nome é obrigatorio")] 
    [StringLength(30, ErrorMessage ="O nome deve ter entre 5 a 80 caracteres", MinimumLength = 5)]
    //[PrimeiraLetraMaiuscula] // VALIDAÇÃO PERSONALIZADA
    public  string? Nome { get; set; }

    // 03 (DATA ANOTATIONS) Descricao é obrigatorio, tipo string e o tamanho maximo será 300 e minimo 10
    [Required] 
    [StringLength(300, MinimumLength = 10)]  
    public  string? Descricao { get; set; }

    // 03 (DATA ANOTATIONS) OBRIGATORIO, decimal com precisão de 10 digitos e 2 casas decimais
    [Required] 
    [Column(TypeName ="decimal(10,2)")]
    public  decimal Preco { get; set; }

    // 03 (DATA ANOTATIONS) ImageUrl é obrigatorio ,tipo string e o tamanho maximo será 300
    [Required] 
    [StringLength(300)]  
    public  string? ImageUrl { get; set; }
    public  float Estoque { get; set; }
    public  DateTime DataCadastro { get; set; }

    
    // tabela relacional que vai indicar o id da categoria do produto
    // e esta tabela esta linkada(relacionada com a tabela Categoria)
    public int CategoriaId { get; set; }


    // criando uma propriedade de navegação aonde estou definindo
    // que Produto está mapeado em Categoria ou seja cada produto
    // terá uma Categoria(lembrando que Categoria tem o id nome e url da imagem)

    [JsonIgnore] // 04 essa prorpiedade será ignorada na serializacao e desserializacao
    public Categoria? Categoria { get; set; }


    // VALIDAÇÃO PELA INTERFACE VALIDATIONTABLEOBJECT
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        // IMPLEMENTACAO DE VALIDACAO
        
        if(!string.IsNullOrEmpty(this.Nome))
        {
            var primeiraLetra = this.Nome[0].ToString();

            if(primeiraLetra != primeiraLetra.ToUpper())
            {
                yield return new
                    ValidationResult("A primeira letra do produto deve ser maisucula",
                    new[]
                    { nameof(this.Nome)}
                    );
            }

            if(this.Estoque <= 0)
            {
                yield return new
                    ValidationResult("O estoque deve ser maior que 0",
                    new[]
                    { nameof(this.Estoque)}
                    );
            }
        }
    }
}
