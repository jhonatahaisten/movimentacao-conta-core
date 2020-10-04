using System.Collections.Generic;

public interface IContaContext
{
    void updateSaldoConta(int nrConta, OperacaoConta objOperacao);

    Conta getConta(int nrConta);
}