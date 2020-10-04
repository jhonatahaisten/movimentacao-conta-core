using Microsoft.AspNetCore.Mvc;

public class ContaRepository : IContaRepository
{
    private readonly IContaContext _context;

    public ContaRepository(IContaContext context)
    {
        _context = context;
    }

    public Conta getConta(int conta)
    {
        return _context.getConta(conta);
    }

    public void updateSaldoConta(int nrConta, OperacaoConta objOperacao)
    {
        //altera o saldo da conta incrementando ou descrementando o valor informado
        _context.updateSaldoConta(nrConta, objOperacao);
    }
}