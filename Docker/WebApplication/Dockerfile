FROM mcr.microsoft.com/dotnet/sdk:7.0
WORKDIR /app
COPY ./Release .

EXPOSE 5000/tcp
ENV ASPNETCORE_URLS HTTP://*:5000
ENV ASPNETCORE_ENVIROMENT docker

ENTRYPOINT ["dotnet", "RiseTechnology.Assesment.CoinPrices.WebApplication.dll"]
