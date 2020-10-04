using System;
using Xunit;
using MovimentacaoContaCorrenteApi;
using Moq;
using MongoDB.Bson.Serialization.Serializers;
using MovimentacaoContaCorrenteApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MovimentacaoContaTest
{
    public class MovimentacaoTest
    {
        private readonly Mock<IContaRepository> _iContaRepository;
        //private readonly Mock<IContaContext> _iContaContext;
        private MovimentacaoController _movimentacaoController;

        public MovimentacaoTest()
        {
            _iContaRepository = new Mock<IContaRepository>();
            //_iContaContext = new Mock<IContaContext>();
            _movimentacaoController = new MovimentacaoController(_iContaRepository.Object);
        }

        [Fact]
        public void consultarSaldo()
        {
            _iContaRepository.Setup(r => r.getConta(It.IsAny<int>())).Returns(new Conta());

            Conta objRetorno = _movimentacaoController.consultarSaldo(1);
            Assert.Equal(objRetorno.conta, objRetorno.conta);
        }

        [Fact]
        public void saqueSucesso()
        {
            Conta objConta = new Conta
            {
                conta = 12,
                saldo = 123
            };
            _iContaRepository.Setup(r => r.getConta(It.IsAny<int>())).Returns(objConta);

            OperacaoConta objOperacao = new OperacaoConta
            {
                conta = 12,
                valor = 120
            };

            ActionResult<Conta> objRetorno = _movimentacaoController.sacar(objConta.conta, objOperacao);

            Assert.IsType<OkObjectResult>(objRetorno.Result);
        }

        [Fact]
        public void saqueComSaldoMenor()
        {
            Conta objConta = new Conta
            {
                conta = 12,
                saldo = 123
            };
            _iContaRepository.Setup(r => r.getConta(It.IsAny<int>())).Returns(objConta);

            OperacaoConta objOperacao = new OperacaoConta
            {
                conta = 12,
                valor = 12300
            };

            ActionResult<Conta> objRetorno = _movimentacaoController.sacar(objConta.conta, objOperacao);
            
            Assert.IsType<UnprocessableEntityObjectResult>(objRetorno.Result);            
        }

        [Fact]
        public void saqueContaInexistente()
        {
            _iContaRepository.Setup(r => r.getConta(It.IsAny<int>())).Returns((Conta)null);

            OperacaoConta objOperacao = new OperacaoConta
            {
                conta = 12,
                valor = 12300
            };

            ActionResult<Conta> objRetorno = _movimentacaoController.sacar(objOperacao.conta, objOperacao);

            Assert.IsType<UnprocessableEntityObjectResult>(objRetorno.Result);
        }

        [Fact]
        public void depositoSucesso()
        {
            Conta objConta = new Conta
            {
                conta = 12,
                saldo = 123
            };
            _iContaRepository.Setup(r => r.getConta(It.IsAny<int>())).Returns(objConta);

            OperacaoConta objOperacao = new OperacaoConta
            {
                conta = 12,
                valor = 120
            };

            ActionResult<Conta> objRetorno = _movimentacaoController.depositar(objOperacao.conta, objOperacao);

            Assert.IsType<OkObjectResult>(objRetorno.Result);
        }
    }
}
