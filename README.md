# RiseTechnology.Assesment.CoinPrices

Uygulamayi calistirmak icin solution Docker klasoru altindaki ./release.bat sciptini calistirdiktan sonra "docker-compose up --build" komutunu calistiriniz

"The view 'Index' was not found. Searched locations: /Views/Home/Index.cshtml, /Views/Shared/Index.cshtml, /Pages/Shared/Index.cshtml" ve benzeri exceptionlar meydana geliyorsa
Powershell console icerisinde #####dotnet watch run ile uygulama ayaga kaldirildiktan sonra tekrar debug yapiniz 

### Git stratejisi
Gelistirme yapilmak icin master branch'tan yeni bir branch klonlandi gelistirme branchlarinin isimlendirilmesi dev-[gelistirme konusu] sablonu ile verildi gelistirme tamamlandiginda calisir durumdaki kod commit edilerek master branch'e pull request acilarak kod merge edildi. Gelistirm tamamlandiginda yada gunluk olarak calisir durumdaki kodlarin commit edilmesine ozen gosterildi
 
