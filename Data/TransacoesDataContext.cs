using System.Linq.Expressions;
using ContaCorrente.ApiExtrato.Models;
using MongoDB.Driver;

namespace ContaCorrente.ApiExtrato.Data
{
    public class TransacoesDataContext
    {

        private readonly IMongoCollection<Transacao> _transacao;

        public TransacoesDataContext(ITransacoesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _transacao = database.GetCollection<Transacao>(settings.CollectionName);

        }

        public async Task<List<Transacao>> GetTransacoesDia(int dia) =>
           await _transacao.Find<Transacao>(x => x.DataCriacao >= DateTime.Now.AddDays(dia * (-1))).ToListAsync();

        public async Task<List<Transacao>> GetTransacoesAgenciaConta(string agencia, string conta) =>
            await _transacao.Find<Transacao>(x =>
                        x.Agencia == agencia &&
                        x.Conta == conta).ToListAsync();
        public async Task<List<Transacao>> GetTransacoesAgenciaContaDia(string agencia, string conta, int dia) =>
            await _transacao.Find<Transacao>(x =>
                x.Agencia == agencia &&
                x.Conta == conta &&
                x.DataCriacao >= DateTime.Now.AddDays(dia * (-1))).ToListAsync();

    }
}
