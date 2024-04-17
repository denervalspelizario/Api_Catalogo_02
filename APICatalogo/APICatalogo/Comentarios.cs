/* 

  ESTUDO WEB API SECAO 4 FUNDAMENTOS DA WEBAPI

  ** USANDO UM MIDDLEWARE TRATAMENTO GLOBAL DE EXCEPTION
  - VAMOS CONFIGURAR UM MIDDLEWARE DE TRATAMENTO DE EXCEÇÕES - UseExceptionHandler
   ´para capturar exceções não tratadas
    
  - crie uma classe em Models ErrorDetails nela tera uma classe usada para representar os erros
  - crie uma pasta chamada Extensions nela tera um arquivo ApiExceptionMiddleware e nela tera 
    um método ConfigureExceptionHandler para IApplicationBuilder
  - Habilitar o uso do método de extensão no ConfigureExceptionHandler no Program.cs
  - faça o teste agora todo erro tera o formato indicado no ApiExceptionMiddleware


  ** CRIANDO FILTROS PERSONALIZADOS 
     tem 2 maneiras assincrona e sincrona
     assincrona = cria uma classe que implementa interface IAsyncActionFilter
     sincrona = cria uma classe que implementa interface IActionFilter
     
     crie uma pasta Filters 
     crie na pasta uma classe chamada ApiLoggingFilter E ADICIONE O CODIGO DO FILTRO
     depois va em Program.cs E ADICIONE  builder.Services.AddScoped<ApiLoggingFilter>();

     DEPOIS VA EM QUALQUER ACTION  NESTE CASO FOMOS NO 
     categoriaController e pegamos o get que retorna todas as categorias




*/