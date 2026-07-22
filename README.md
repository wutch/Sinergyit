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
