using Microsoft.AspNetCore.Mvc;

namespace MovimentacaoContaCorrenteApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovimentacaoController : ControllerBase
    {
        private readonly IContaRepository _repo;

        public MovimentacaoController(IContaRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("saldo/{conta}")]
        public Conta consultarSaldo(int conta)
        {
            return _repo.getConta(conta);
        }

        [HttpPut("depositar/{conta}")]
        public ActionResult<Conta> depositar(int conta, [FromBody] OperacaoConta objOperacao)
        {
            //realiza a operação de saque, incrementando o saldo da conta
            _repo.updateSaldoConta(conta, objOperacao);

            //consulta o saldo da conta atualizado
            return new OkObjectResult(_repo.getConta(conta));
        }

        [HttpPut("sacar/{conta}")]
        public ActionResult<Conta> sacar(int conta, [FromBody] OperacaoConta objOperacao)
        {
            //consulta o saldo da conta atualizado
            Conta objConta = _repo.getConta(conta);

            if (objConta != null && objConta.saldo >= objOperacao.valor)
            {
                objOperacao.valor *= -1;
                //realiza a operação de saque, decrementando o saldo da conta
                _repo.updateSaldoConta(conta, objOperacao);

                //consulta o saldo da conta atualizado
                Conta objContaAtualizado = _repo.getConta(conta);
                return new OkObjectResult(objContaAtualizado);
            }
            else
            {
                return new UnprocessableEntityObjectResult(new Conta());
            }
        }
    }
}
