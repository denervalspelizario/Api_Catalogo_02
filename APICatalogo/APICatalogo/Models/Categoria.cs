using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;         // chamando bblioteca data anotations
using System.ComponentModel.DataAnnotations.Schema;  // chamando bblioteca data anotations

namespace APICatalogo.Models;

// 03 (DATA ANOTATIONS) mapeando essa entidade para a tabela Categorias la do banco de dados
[Table("Categorias")]
public class Categoria // classe anemica pq só foi definido propriedades
{
    // 02 este construtor seria uma boa pratica 
    // no construtor da classe já se inicializa a propriedade Produtos
    // que é uma coleção de objeto Produto
    public Categoria()
    {
        Produtos = new Collection<Produto>();
    }



    // propriedades 01
    [Key] // 03 (DATA ANOTATIONS)indicando com o Key que CategoriaId é uma chave primaria
    public int CategoriaId { get; set; } // indicando a chave primaria

    [Required] // 03 (DATA ANOTATIONS)indicando que Nome é obrigatorio
    [StringLength(80)]  // indicando que sera tipo string e o tamanho maximo será 80
    public string? Nome { get; set; }

    [Required] // 03 (DATA ANOTATIONS)indicando que ImagemUrl é obrigatorio
    [StringLength(300)]  // indicando que sera tipo string e o tamanho maximo será 300
    public string? ImagemUrl { get; set; } 

    // 02 estou definindo que Categoria vai ter uma coleção de objeto Produto
    // que será chamada Produtos, cada categoria pode ter uma coleção de produtos
    // isso por si só ja seria suficiente mas agora vou la na tabela Produto deixar ainda
    // mais explicito
    public ICollection<Produto>? Produtos { get; set; }
}
