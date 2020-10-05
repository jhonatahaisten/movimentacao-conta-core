## Rodando aplicação docker local


## Rodando API em docker local
Abra a aplicação no VS Code ou Visual Studio, dependendo da IDE utilizada será necessário baixar as dependências usando:
```console
dotnet restore
```

Utilizando um docker linux instalado e rodando localmente na máquina, execute o comando abaixo no diretório raiz da aplicação:
```console
cd MovimentacaoContaCorrenteApi
docker-compose up -d --build
```

O comando acima criou e subiu 3 containers para aplicação: API, MongoDB e outro para uma carga de dados iniciais no MongoDB.

Já é possível testar a aplicação, a API está disponível na porta 8080 (https://localhost:8080) e o MongoDB na porta 27017(https://localhost:27017).
Para facilitar disponibilizei uma collection do Postman e swagger: http://localhost:8080/swagger/index.html, mas fiquem à vontade para testar a API usando as ferramentas que preferirem.
Além disso, a API também está disponibilizada na AWS: http://3.94.121.110:8080/swagger/index.html


## Cenários e retornos da API
###Cenário 1:
Sacar informando o número da conta e um valor válido
ENTÃO a API retornará HttpStatusCode 200 e um payload contendo saldo atualizado e número da conta

###Cenário 2:
Sacar informando o número da conta e um valor maior do que o meu saldo
ENTÃO a API retornará HttpStatusCode 422

###Cenário 3:
Depositar informando o número da conta e um valor válido
ENTÃO a API atualizará o saldo da conta no banco de dados, retornará HttpStatusCode 200 e um payload contendo saldo atualizado e número da conta

###Cenário 4:
Consultar saldo informando o número da conta
ENTÃO a API consultará o saldo da conta no banco de dados, retornardo HttpStatusCode 200 e um payload contendo saldo e número da conta


## Executando testes unitários

Para executar os testes unitários da aplicação, executar o comando abaixo no diretório do projeto de testes:
```
dotnet test /p:CollectCoverage=true
```