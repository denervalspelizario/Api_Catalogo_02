/* Estruturação da API

** exclusão dos arquivos controllers controllers

** criação pasta Models e adição de classes Categoria e Produto

** neste caso vamos usar a abordagem CODE-FIRST
   Podemos realizar o mapeamento das classes e das entidades do dominio e gerar o banco de dados e as tabelas
   Primeiro criamos as entidades e a partir delas geramos o banco e as tabelas(migrations)
   O EF CORE adota algumas convenções para realizar o mapeamento das entidades

** então temos que instalar as referências ao EF CORE e ao provedor POMELO para o MYSQL
   Pomelo.EntityFrameworkCore.MySql
   Microsoft.EntityFrameworkCore.Design
   Microsoft.AspNetCore.OpenAI
   comandos são 
   Package Manager Console - install-package <nome_pacote>
   NET CLI - dotnet add package <nome_pacote>

  além disso precisa instalar 
  Ferramenta EF Core Tools (Microsoft.EntityFrameworkCore.Tools)
  (ferramenta para gerar as migrations)
  dotnet tool install --global dotnet-ef (instalar)
  dotnet tool update --global dotnet-ef (atualizar)
  dotnet ef  (verificar se esta instalada)
  
   Essas instalações podem ser feitas pelo Nuget-solução apenas com alguns clicks

** Criar arquivo de contexto do EF CORE 
   pasta Context > AppDbContext
   esse arquivo de contexto que vai linkar as classes criadas(Categoria e Produto)
   com as tabelas do banco de dados que serão criadas(Categorias e Produtos)
   observe que as tabelas estão no plural e as propriedades no singular

** após criar o contexto ir no appsetting.json e definir qual a string de conexao 
   com banco de dados que será usada para se comunicar com o db
   la no appsetting vou criar "ConnectionString" la que sera definido a string de conexao com o banco
   o SITE WWWW.CONNECTIONSTRINGS.COM PERMITE A CONSULTA A DIVERSAS STRINGS DE CONEXAO

** ir na classe Program.cs e crio a mySqlConnection para concluir a string de conexao com o bd
   e no services adicionando o contexto e o tipo de bd 
   ATENTAR A SINTAXE

** agora antes de criar as migrations vamos definir a relação entre Categoria e Produto
   ou seja vamos criar uma coluna na tabela produtos que estará relacionada a Categoria
   a famosa tabela FOREIGN KEY
   De inicio vamos em Categoria.cs criar uma tabela relacional
   E Depois vou em Produto criar a tabela relacional CategoriaId

** Ápós podemos de fato fazer as migrations ja que esta tudo configurado
   vamos usar o EF Core Migrations e o EF Core Tools
   segue os comandos que vamos usar
   
   dotnet ef
   
   - Criar o script de migração
     dotnet ef migrations add 'nome'
  
   - Remove o script de migração criado
     dotnet ef migrations remove 'nome'

   - Gera o banco de dados e as tabelas com base no script
     dotnet ef database update

    OBS lembrando que tem a opção de fazer essas migrations usando o Console do visual studio
        (Package Manager Console) e instalar o Pacote Nuget Microsoft.EntityFrameworkCore.Tools

     - Criar o script de migração
       add-migration 'nome'
  
     - Remove o script de migração criado
       remove-migration 'nome'

     - Gera o banco de dados e as tabelas com base no script
       update-database    

** neste caso vamos criar as migrations por comandos mesmo
   vá em Ferramentas>Linha de Comando>Prompt de Comando do Desenvolvedor
   de um comando dir e depois entre neste caso vc esta na pasta da solução porem
   vc precisa estar na pasta do projeto que neste caso será cd APICatalogo,
   agora estando na pasta de certa segue o comando: dotnet ef migrations add MigracaoInicial
   Depois de feito o comando será criado uma pasta chamada Migration com 2 arquivos

   Deu certo agora vamos criar o arquivo de script voltando ao console
   dotnet ef database update   
   este comando vai gerar as tabelas as colunas com as definições estabelecidas
   se vc for no mysql workbench e dar uma atualizada vc vai ver que ele criou o db
   do jeito que foi estabelecido

** depois de usar as migrations e fazer de fato o banco e as tabelas e linkar com a aplicação
   chegou o momento de ajustar e configurar as tabelas seguindo o padrão desejado
   pq elas vem em um padrão não muito otimizado
   para fazer isto vamos usar os famosos Data Annotations
   vamos inciar em Categoria e depois em Produto

** Após fazer a adição dos Data Anotations e otimizar as tabelas
   va para o Ferramentas>Linha de Comando>Prompt de Comando do Desenvolvedor
   e de o comando para atualizar as migrations neste caso será
   dotnet ef migrations add AjusteTabelas
   este AjusteTabelas será o nome da nova migrations criada

   depois de criada essa nova migrations(AjusteTabela)
   agora temos que atualizar no banco de fato voltando no console digite
   
   dotnet ef database update
   
   assim com esse comando será atualizado de fato la no banco de dados 

** Agora vamos popular as tabelas para poder testar vamos usar o conceito de "Popular tabela de dados"
   no resumo tem varias abordagens para fazer isso no nosso caso vamos usar a abordagem
   - Criar uma migração vazia usando o Migrations e usar os métodos Up() e Down()
     definindo nestes métodos as instruções INSERT INTO para incluir dados nas tabelas
    
   ou seja o roteiro será
   
   1 - Criar uma nova migração usando Migrations
       dotnet ef migrations add PopularesCategorias
   2 - Definir os comandos SQL no método Up() para incluir dados
       insert into categorias(Nome,ImageUrl) Values('Bebidas','bebidas.jpg')
   3 - Definir os comandos SQL no método Down() para reverter a migração
       delete from categorias
   4 - Aplicar a migração
       dotnet ef database update


   seguindo a mesma ideia na tabela Produto

   1 - Criar uma nova migração usando Migrations
       dotnet ef migrations add PopularProdutos
   2 - Definir os comandos SQL no método Up() para incluir dados
       insert into produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId) 
       Values('Suco de Laranja','Suco de Laranja 500ml, 7.45, 'sucolaranja.jpg',10,now(),1)
   3 - Definir os comandos SQL no método Down() para reverter a migração
       delete from produtos
   4 - Aplicar a migração
       dotnet ef database update


** sabendo de tudo isso agora vamos dar os comandos
   Ferramentas>Linha de Comando>Prompt de Comando do Desenvolvedor entra na pasta de projeto e 
   digite o comando : dotnet ef migrations add PopularCategorias
   assim sera criao do arquivo em migrations PopularCategorias
   la faça a configuraçãod e inserção de dados e depois digite no console
   dotnet ef database update 
   para atualizar o banco de dados segundo a migration criada
    
   repetir o mesmo procedimento com adicionar dados na tabela Produto

** Agora depois é hora de ir para os Controllers da Api
   existem varias maneira de criar controlers no asp.netcore
   aqui vamos fazer assim
   cliclar com botao direto na pasta Controllers > Adicionar > Controller > Controller Vazio
   lembrando do lado esquerdo estar em API  clicar no ADD vai abrir uma aba para vc colocar
   o nome e pronto Controller criado

   Esse processo sera o mesmo tanto no caso de ProdutosController e CategoriasController
   Os comentarios explicando o passo a passo estao no ProdutosController  no CategoriasController
   deixei o codigo mais limpo 
   

** O primeiro Get de Categoria que retorna todas as categorias e produtos esta com um problema de
   de serialização para resolve-lo va em Progam.cs e mude o builder.Services.AddControllers()
 
  
** Após fazer o ajuste para evitar o erro de serializacao e desserializacao 
   vamos otimizar nosso codigo   

   Dica Nunca retorne todos os registros em uma consulta
   exemplo: 
   _context.Produtos.Take(10).Tolist(); - neste caso ele pega toda lista mas  até 10 itens

   Dica Nunca retorne objetos relacionados sem aplicar um filtro
   exemplo:
   _context.Categorias.Include(p => p.Produtos).Where(c=>c.CategoriaId <= 5).ToList();
   pegando todos as categorias e todos os produtos onde o Id de categoria for menor ou igual a 5

** Agora que a gente ja fez algumas otimizacoes vamos  fazer tratamento de erros com 
   o bloco try cacth


*/