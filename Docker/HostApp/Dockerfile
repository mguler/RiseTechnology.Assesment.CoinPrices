FROM mcr.microsoft.com/dotnet/sdk:7.0
WORKDIR /app
COPY ./Release .

ENV ASPNETCORE_ENVIROMENT docker

COPY ./Release/wait-for-it.sh /wait-for-it.sh
RUN chmod +x wait-for-it.sh

ENTRYPOINT ["/wait-for-it.sh", "db2019:1433", "-t", "120", "--","dotnet", "RiseTechnology.Assesment.CoinPrices.Integrations.HostApp.dll"]