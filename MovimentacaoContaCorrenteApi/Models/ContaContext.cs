using MongoDB.Driver;

public class ContaContext: IContaContext
{
    private readonly IMongoDatabase _iMongo;
    private readonly IMongoCollection<Conta> _iConta;

    public ContaContext(MongoConnString config)
    {
        var client = new MongoClient(config.ConnectionString);
        _iMongo = client.GetDatabase(config.Database);
        _iConta = _iMongo.GetCollection<Conta>("conta");
    }

    public Conta getConta(int nrConta)
    {
        ProjectionDefinition<Conta> projection = Builders<Conta>.Projection.Include("conta").Include("saldo");

        return _iConta.Find<Conta>(conta => conta.conta == nrConta).Project<Conta>(projection).FirstOrDefault();
    }

    public void updateSaldoConta(int nrConta, OperacaoConta objOperacao)
    {
        var objFilter = Builders<Conta>.Filter.Eq("conta", nrConta);
        var objUpdateDefinition = Builders<Conta>.Update.Inc(conta => conta.saldo, objOperacao.valor);

        _iConta.FindOneAndUpdate(objFilter, objUpdateDefinition);
    }
}