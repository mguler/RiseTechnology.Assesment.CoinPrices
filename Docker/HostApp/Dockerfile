FROM mcr.microsoft.com/dotnet/sdk:7.0
WORKDIR /app
COPY ./Release .

ENV ASPNETCORE_ENVIROMENT docker

ENTRYPOINT ["dotnet", "RiseTechnology.Assesment.CoinPrices.Integrations.HostApp.dll"]