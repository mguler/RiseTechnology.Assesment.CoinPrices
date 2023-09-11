rmdir /s /q "DataApi/Release"
rmdir /s /q "HostApp/Release" 
rmdir /s /q "WebApplication/Release" 

mkdir "DataApi/Release"
mkdir "HostApp/Release"
mkdir "WebApplication/Release"

dotnet restore ..\RiseTechnology.Assesment.CoinPrices.sln
dotnet publish ..\RiseTechnology.Assesment.CoinPrices.sln

xcopy "..\RiseTechnology.Assesment.CoinPrices.DataApi\bin\Debug\net7.0\publish\*.*" "DataApi/Release" /s /e /h
xcopy "..\RiseTechnology.Assesment.CoinPrices.Integrations.HostApp\bin\Debug\net7.0\publish\*.*" "HostApp/Release" /s /e /h
xcopy "..\RiseTechnology.Assesment.CoinPrices.WebApplication\bin\Debug\net7.0\publish\*.*" "WebApplication/Release" /s /e /h

copy "wait-for-it.sh" "DataApi/Release"
copy "wait-for-it.sh" "HostApp/Release"
copy "wait-for-it.sh" "WebApplication/Release"