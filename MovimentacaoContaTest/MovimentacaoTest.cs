using Xunit;
using Moq;
using MovimentacaoContaCorrenteApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MovimentacaoContaTest
{
    public class MovimentacaoTest
    {
        private readonly Mock<IContaRepository> _iContaRepository;
        private MovimentacaoController _movimentacaoController;
        Conta objConta;
        OperacaoConta objOperacao;

        public MovimentacaoTest()
        {
            _iContaRepository = new Mock<IContaRepository>();
            _movimentacaoController = new MovimentacaoController(_iContaRepository.Object);
        }

        #region Testes Controler
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
            mockPadrao();

            _iContaRepository.Setup(r => r.getConta(It.IsAny<int>())).Returns(objConta);

            ActionResult<Conta> objRetorno = _movimentacaoController.sacar(objConta.conta, objOperacao);

            Assert.IsType<OkObjectResult>(objRetorno.Result);
        }

        [Fact]
        public void saqueComSaldoMenor()
        {
            mockPadrao();
            objOperacao.valor += objConta.saldo;

            _iContaRepository.Setup(r => r.getConta(It.IsAny<int>())).Returns(objConta);

            ActionResult<Conta> objRetorno = _movimentacaoController.sacar(objConta.conta, objOperacao);
            
            Assert.IsType<UnprocessableEntityObjectResult>(objRetorno.Result);            
        }

        [Fact]
        public void saqueContaInexistente()
        {
            mockPadrao();

            _iContaRepository.Setup(r => r.getConta(It.IsAny<int>())).Returns((Conta)null);

            ActionResult<Conta> objRetorno = _movimentacaoController.sacar(objOperacao.conta, objOperacao);

            Assert.IsType<UnprocessableEntityObjectResult>(objRetorno.Result);
        }

        [Fact]
        public void depositoSucesso()
        {
            mockPadrao();

            _iContaRepository.Setup(r => r.getConta(It.IsAny<int>())).Returns(objConta);

            ActionResult<Conta> objRetorno = _movimentacaoController.depositar(objOperacao.conta, objOperacao);

            Assert.IsType<OkObjectResult>(objRetorno.Result);
        }
        #endregion

        #region Testes Context
        [Fact]
        public void getConta()
        {
            mockPadrao();

            Mock<IContaContext> iContext = new Mock<IContaContext>();            
            iContext.Setup(r => r.getConta(It.IsAny<int>())).Returns(objConta);

            Conta objContaRetornado = new ContaRepository(iContext.Object).getConta(It.IsAny<int>());
            
            Assert.Equal(objConta.conta, objContaRetornado.conta);
        }

        [Fact]
        public void updateSaldoConta()
        {
            Mock<IContaContext> iContext = new Mock<IContaContext>();
            iContext.Setup(r => r.updateSaldoConta(It.IsAny<int>(), It.IsAny<OperacaoConta>()));

            ContaRepository repository = new ContaRepository(iContext.Object);
            repository.updateSaldoConta(It.IsAny<int>(), It.IsAny<OperacaoConta>());
        }
        #endregion

        #region Conn
        [Fact]
        public void connectionComUser()
        {
            MongoConnString conn = new MongoConnString() { User = "xx", Password = "xx", Port = 3030, Database = "yy", Host = "ii" };
            string strConnection = conn.ConnectionString;

            Assert.Contains(conn.User, strConnection);
        }

        [Fact]
        public void connectionSemPass()
        {
            MongoConnString conn = new MongoConnString() { User = "xx", Port = 3030, Database = "yy", Host = "ii" };
            string strConnection = conn.ConnectionString;

            Assert.DoesNotContain(conn.User, strConnection);
        }

        /*[Fact]
        public void context()
        {
            MongoConnString conn = new MongoConnString() { User = "xx" };

            Assert.Contains(conn.User, conn.ConnectionString);
            ContaContext contaContext = new ContaContext(new MongoConnString() { User = "xx" });


            mockPadrao();

            Mock<IContaContext> iContext = new Mock<IContaContext>();
            iContext.Setup(r => r.getConta(It.IsAny<int>())).Returns(objConta);

            Conta objContaRetornado = new ContaRepository(iContext.Object).getConta(It.IsAny<int>());

            Assert.Equal(objConta.conta, objContaRetornado.conta);
        }*/
        #endregion

        private void mockPadrao()
        {
            objConta = new Conta
            {
                conta = 12,
                saldo = 123
            };

            objOperacao = new OperacaoConta
            {
                conta = 12,
                valor = 120
            };
        }
    }
}
