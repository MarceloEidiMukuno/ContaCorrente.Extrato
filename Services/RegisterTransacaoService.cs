using ContaCorrente.ApiExtrato.Data;
using ContaCorrente.ApiExtrato.Models;
using MongoDB.Driver;

namespace ContaCorrente.ApiExtrato.Services
{
    public class RegisterTransacaoService : IRegisterTransacaoService
    {
        private readonly IConfiguration _configuration;

        private readonly IMongoCollection<Transacao> _transacao;

        public RegisterTransacaoService(IConfiguration configuration)
        {
            _configuration = configuration;

            MongoClient client = new MongoClient(
                _configuration.GetSection("TransacoesDatabaseSettings:ConnectionString").Value);

            IMongoDatabase database = client.GetDatabase(_configuration.GetSection("TransacoesDatabaseSettings:DatabaseName").Value);

            _transacao = database.GetCollection<Transacao>(_configuration.GetSection("TransacoesDatabaseSettings:CollectionName").Value);
        }

        public async Task RegisterAsync(Transacao transacao)
        {

            await _transacao.InsertOneAsync(transacao);

        }
    }
}