FROM mcr.microsoft.com/dotnet/sdk:7.0
WORKDIR /app

COPY ./Release .
EXPOSE 5000/tcp

ENV ASPNETCORE_URLS HTTP://*:5000
ENV ASPNETCORE_ENVIROMENT docker

COPY ./Release/wait-for-it.sh /wait-for-it.sh
RUN chmod +x wait-for-it.sh

ENTRYPOINT ["/wait-for-it.sh", "db2019:1433", "-t", "120", "--","dotnet", "RiseTechnology.Assesment.CoinPrices.WebApplication.dll"]
#ENTRYPOINT ["dotnet", "RiseTechnology.Assesment.CoinPrices.WebApplication.dll"]