FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /api

COPY . ./
RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /api
EXPOSE 80
COPY --from=build-env /api/out .
ENTRYPOINT ["dotnet", "MovimentacaoContaCorrenteApi.dll"]