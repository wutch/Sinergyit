# Sinergyit
Sinergyit Soruları

## Unit Test

  *1) .NET platformunda Unit Test yapma süreci*

    İlk aşamada hazırlık yapılır değişkenler tanımlanır ve mock objectler yerleştirilir.
    Daha sonra test etmek istediğimiz bir metodu çalıştırırız bu metodud ürettiği sonuç bir değişkene atanır.
    Son olarak beklediğimiz değerlerle bi önceki aşamada atadığımız değişkeni karşılaştırırız.

  *2) Xunit ve Moq temel kavramlar*
  
  Mocked Object Üretme:
  
    Kodda veritabanı gibi dış bağımlılıklara ihtiyaç oluyorsa bunlarla uğraşmadan kodu test edebilmek için
    sahte bir nesne üretir ve bu nesnenin bizim istediğimiz gibi davranmasını sağlarız.
    
  Assert İşlemleri:
  
    Assert.Equal(x,y) -> iki değerin aynı olup olmadığına bakar
    Assert.NotEqual(x,y) -> iki değer birbirinden farklı mı diye bakar     
    Assert.True(x) -> Sonuç true mu diye bakar
    Assert.False(x) -> Sonuç false mi diye bakar
    Assert.Null(x) -> x nesnesi null mu diye bakar
    
## Saga Patterns
 
 *Saga pattern mikroservis mimarisinde hangi sorunları çözmeye çalışır?*

    Mikroservislerde her servisin veritabanı vardır. Bir iş birden fazla servisi kapsıyorsa tüm veritabanlarının aynı anda güncellenmesi 
    hata durumundaysa hepsinin iptal edilmesi gerekir bu sürecin son adımında bir hata çıkarsa sondan önceki adımlarda yapılan değişikliklerin 
    nasıl geri alınacağı problemiyle saga ilgilenir ve sistemin yarım kalmış verilerle dolmasını engeller. 

*Saga patterndeki choreography ve orchestration yaklaşımları arasındaki temel fark nedir?*

    choreography:  * Merkeziyetsizdir. Her servis kendi işinden sorumludur
                   * Event driven dir . Servisler ortaya ödeme alındı gibi bir olay fırlatır.
                   * Servisler akıştaki diğer servislerin fırlattığı olayları bilmek zorundadır.
                   * Sistemin o an hangi aşamada olduğunu görmek zordur.
                        
    orchestration: * Merkezidir. Tüm süreci tek bir Orkestratör yönetir.
                   * command driven dir. Orkestratör servise ödemeyi al diye emir verir.
                   * Servisler birbirini hiç tanımaz sadece orkestratörden gelen emre bakar.
                   * Tüm durum orkestratör üzerinde tutulduğu için akış anlık olarak izlenebilir.

*Orchestration Saga pattern avantajları ve dezavantajları nelerdir?*

  Avantajları
  
    * Hangi adımın ne zaman çalışacağı veya hata anında hangi telafi işlemlerinin yapılacağı tek bir yerde tanımlıdır. Süreci güncellemek çok kolaydır.
    * Bağımsızdır, katılımcı servisler birbirlerinden habersizdir stok servisi ödeme servisini veya akışın neresinde olduğunu bilmez sadece ona verilen stok düş gibi komutları uygular.
    * Koreografide servisler birbirinin olaylarını dinlediği için kolayca içinden çıkılmaz döngülere girebilirler. Orkestrasyonda ise tüm trafik tek yönlü akar.
    * Bir siparişin tam olarak hangi aşamada takıldığını görmek için sadece orkestratörün loglarına bakmak yeterlidir.

  Dezavantajları 
  
    * Tüm süreç orkestratör üzerinden aktığı için orkestratör servisi çökerse yeni süreç başlatılamaz.
    * Geliştiriciler iş mantığını ilgili servislere dağıtmak yerine herşeyi orkestratörün içine yazmaya başlarsa orkestratör bir anda bakımı zor bir servise dönüşür.
    * Durum yönetimi yapmak zordur. Genellikle bu akışları yönetmek için ekstra araçlar ve kütüphaneler kurulması ve öğrenilmesi gerekir.

*4) State Machine Diagram Açıklamaları*

    Saga patternda her bir mikroservis kendi yerel işlemini yapar. Bir hatayla karşılaşıldığında sistemin geri alma işlemi
    kronolojik sıranın tersine çalışır. Önce en son yapılan işlem ardından bir önceki işlem gerçekleşir

    StokKontrolu aşaması - Stok varsa sistem bir sonraki aşama olan StokRezerveEdildi ye geçer 
    stok yoksa sistem doğrudan SiparisIptalEdildi durumuna geçer.

    OdemeKontrolu aşaması - ödeme alındıysa sistem bir sonraki aşama olan OdemeTamamlandi durumuna geçer.
    Bakiye yetersizse geri alma işlemi başlatılır ve sistem StokGeriAlindi durumuna geçer.

    KargoKontrolu aşaması - Kargo başarılıysa sistem nihai başarı durumu olan SiparisTamamlandi durumuna geçer.
    Kargo başarısızsa telafi işlemi başlatılır ve sistem OdemeIadeEdildi durumuna geçer.

## Logging

*Logging nedir, hangi problemleri çözer?*

    Logging bir yazılımın çalışması sırasında meydana gelen olayların hataların ve durum değişikliklerinin
    timestamplerle birlikte kaydedilmesidir. Uygulamanın arka planda ne yaptığını anlamamıza yarar.
    
    Loglar bir hata oluştuğunda sistemin o anki durumunu göstererek hatanın kaynağını bulmamızı sağlar
    Uygulamanın beklenene göre uygun çalışıp çalışmadığını gösterir.Sisteme kimin ne zaman ve nereden eriştiğini
    takip etmemizi, başarısız giriş denemelerini, yetkisiz erişimlerleri tespit etmemizi sağlar.
    Dağıtık sistemlerde bir işlem başarısız olduğunda hatanın yazdığımız kodda mı yoksa dışarıdan çağırdığımız bir serviste mi olduğunu anlamamızı sağlar.

*Log, metric ve trace arasındaki fark nedir?*

    Log sistemdeki bir olayın metin tabanlı kaydıdır. Hata ayıklarken en çok detayı loglardan elde ederiz
    temel olarak kullanma amacımız hata analizi ve detaylı inceleme yapmak
    
    Metric sistemin genel sağlığını gösteren sayısal ölçümlerdir sistem yöneticilerini uyarmak 
    alarm kurmak ve dashboard hazırlamak için kullanılır. Metrikler tekil olaylarla ilgilenmez genel trende bakar
    temel olarak kullanma amacımız alarm kullanmak ve canlı sistem izlemek.
    
    Trace özellikle mikroservis mimarilerinde kullanıcı isteğinin sistem içindeki yolculuğunu gösterir.
    İstek hangi servislerden geçti veritabanında ne kadar bekledi API a ne kadar sürede bağlandı gibi bilgileri
    span adı verilen işlem ağaçlarıyla sunar, temel kullanım amacımız performansı düşüren nedenleri bulmak.

*Log seviyeleri (Trace–Critical) ne anlama gelir; hangi durumda hangisi seçilir?*

    Log seviyeleri uygulamanızın ürettiği kayıtları önem derecesine göre sınıflandırmamızı sağlar. 
    Bu sınıflandırma sayesinde gereksiz detaylar yerine kritik sorunlara odaklanabilir ve otomatik alarmlar kurabiliriz.
    
    Trace en ince detayları içeren kodun satır satır nasıl çalıştığını gösteren seviyedir.
    Yalnızca karmaşık bir bug çözülmeye çalışılırken geçici olarak açılır. Algoritma içindeki döngü adımlarını
    if else bloklarına girilip girilmediğini veya çok büyük veri setlerinin işlenme anındaki anlık durumlarını
    görmek istediğimizde Trace yi seçeriz.

    Critical uygulamanın çalışmasını tamamen durduran anında müdahale gerektiren senaryolardır.
    Bu log tetiklendiğinde uygulama muhtemelen çökmüştür. Ana veritabanına hiçbir şekilde ulaşılamadığında sunucuda disk tamamen
    dolduğunda veya uygulama bellek yetersizliğinden kapandığında seçilir.

    

    
