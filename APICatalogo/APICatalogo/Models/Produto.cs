using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models;



// 03 (DATA ANOTATIONS) mapeando essa entidade para a tabela Produtos la do banco de dados
[Table("Produtos")]
public class Produto // classe anemica pq só foi definido propriedades
{
    // propriedades
    [Key] // 03 (DATA ANOTATIONS)indicando com o Key que ProdutoId é uma chave primaria
    public int ProdutoId { get; set; } // chave primaria

    [Required] // 03 (DATA ANOTATIONS)indicando que Nome é obrigatorio
    [StringLength(80)]  // indicando que sera tipo string e o tamanho maximo será 80
    public  string? Nome { get; set; }

    [Required] // 03 (DATA ANOTATIONS)indicando que Descricao é obrigatorio
    [StringLength(300)]  // indicando que sera tipo string e o tamanho maximo será 300
    public  string? Descricao { get; set; }

    [Required] // 03 (DATA ANOTATIONS)indicando que Preco é obrigatorio
    [Column(TypeName ="decimal(10,2)")]// indicando que sera tipo decimal com precisão de 10 digitos e 2 casas decimais
    public  decimal Preco { get; set; }

    [Required] // 03 (DATA ANOTATIONS)indicando que ImageUrl é obrigatorio
    [StringLength(300)]  // indicando que sera tipo string e o tamanho maximo será 300
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
}
