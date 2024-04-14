using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Context;

// classe AppDbContext herda  a classe DbContext que é do EntityFrameworkCore
// ela é responsavel por realizar a comunicação entre as minhas entidades
// e o banco de dados relacional
public class AppDbContext : DbContext
{
    // criando um construtor (ctor tab e tab)
    // que tem como parametro chamado options que tera as opções de configurações
    // que serão usadas para configurar o banco de dados
    // base(options estou chamando o construtor da classe base(DbContext))
    // e passando as configurações que eu vou definir
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
            
    }

    // propriedades
    // usando a classe DbSet
    // mapeando a propriedade Categoria para a TABELA Categorias 
    // mapeando a propriedade Produto para a TABELA Produtos
    // ambas as propriedades estão definidas como nullables
    public DbSet<Categoria>? Categorias { get; set; }
    public DbSet<Produto>? Produtos { get; set; }
}
