namespace APICatalogo.Services
{
    public class MeuServico : IMeuServico
    {
        // METODO HERDADO DA INTERFACE IMeuServico
        public string Saudacao(string nome) 
        {
            return $"Bem vindo, {nome} \n\n {DateTime.UtcNow}";
        }
    }
}
