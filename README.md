# RiseTechnology.Assesment.CoinPrices

Bitcoin fiyat grafigi uygulamasi. Bitcoin fiyatlarini cizgi grafik uzerinde gosterir.

## Baslangic

Asagidaki yonergeleri izleyerek uygulamayi bilgisayariniza indirip, kurabilir ve calistirabilirsiniz.
 
### Gereksinimler

- .Net 7 runtime & Asp.Net 7 runtime
- Docker Desktop & docker compose
- git cli
- Windows Powershell

### Kurulum ve Calistirma

Uygulamayi clonu olusturmak ve calistirmak icin asagidaki komutlari sirasi ile uygulayiniz

.\git clone https://github.com/mguler/RiseTechnology.Assesment.CoinPrices.git

.\RiseTechnology.Assesment.CoinPrices\Docker\release.bat

.\RiseTechnology.Assesment.CoinPrices\Docker\docker-compose up --build

### Testler

Unit testleri uygulamak icin asagidaki komutu calistiriniz

.\RiseTechnology.Assesment.CoinPrices\dotnet test

### Git stratejisi

Gelistirme yapilmak icin master branch'tan yeni bir branch klonlandi. Gelistirme branchlarinin isimlendirilmesi dev-[gelistirme konusu] sablonu ile verildi. Gelistirme tamamlandiginda calisir durumdaki kod commit edilerek master branch'e pull request acildi ve kod merge edildi. Tum commitler uzerinde calisilan gelistirme tamamlandiginda yada gunluk olarak calisir durumdaki kodlarin commit edilmesi seklinde yapildi

## Karsilasilan Sorunlar

1) Uygulama container'a deploy edildiginde eger kullanilan bilgisayar yeterince guclu degilse mssql server'in calismasi zaman alir ve baglanti taleplerini karsilayamaz. Bu durumun onune gecebilmek icin oncelikle docker-compose configurasyounda "depens on" ile bagimliliklar tanimlandi fakat sorun cozulmedi. Bu sorunu cozmek icin wait-for-it.sh scripti kullanildi. wait-for-it.sh kendisine parametre olarak verilen host ve port'a baglanti kurmaya calisir, baglanti saglandiginda ise yine kendisine parametre olarak verilen uygulamayi calistirir. Cozum icin scriptin mssql 1433 nolu portu baglanti kurduktan sonra ilgili uygulamalari calistirmasi saglanarak sorun cozuldu
2) wait-for-it.sh scripti calistirildiginda "bash\r: No such file or directory" hatasi alindi bu sorunu cozmek icin wait-for-it.sh dosyasi notepad++ ile acilarak line endings LF yapilarak dosya kaydedildi. .gitattributes dosyasina "*.sh text eol=lf" satiri eklenerek git'e push edildi
3) System.InvalidOperationException: The view 'Login' was not found. The following locations were searched:
	WebApplication   |       /Views/User/Login.cshtml
	WebApplication   |       /Views/Shared/Login.cshtml 

	Yukaridaki view hatasi alindi, Cozum icin solution klasoru icerisinde *_./dotnet watch* run_ komutu ile *_hot reload_* aktif edilerek calistirildiginda gecici cozum saglandi. Daha sonra Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation pakedi uygulamaya eklenerek sorun cozuldu   
4) docker-compose.yml dosyasinda tanimli olan environment variable'da $ karakteri kullanildigi icin "Invalid Interpolation Format Error in Docker Compose" hatasi alindi $$ seklinde cift $ karakteri kullanilarak escape edilebilecegi anlasildi ilgili environment variable degeri degistirildi
     
## Araclar  

- Visual Studio 2022 Community Edition
- Docker Desktop
- GitHub
- Windows Powershell
- Nuget Package Manager
- Chrome Browser & Development Tools

## Gelistiriciler

M. Güler

## Lisans

Uygulama MIT lisansi altinda yayinlanmistir
