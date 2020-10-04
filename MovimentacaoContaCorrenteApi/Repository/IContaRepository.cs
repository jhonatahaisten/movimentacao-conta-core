using Microsoft.AspNetCore.Mvc;

public interface IContaRepository {

    Conta getConta(int conta);

    void updateSaldoConta(int nrConta, OperacaoConta objConta);
}