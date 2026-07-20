# Sinergyit
Sinergyit Soruları

Unit Test

  1) .NET platformunda Unit Test yapma süreci

    İlk aşamada hazırlık yapılır değişkenler tanımlanır ve mock objectler yerleştirilir.
    Daha sonra test etmek istediğimiz bir metodu çalıştırırız bu metodud ürettiği sonuç bir değişkene atanır.
    Son olarak beklediğimiz değerlerle bi önceki aşamada atadığımız değişkeni karşılaştırırız.

  2) Xunit ve Moq temel kavramlar
  
  Mocked Object Üretme:
  
    Kodda veritabanı gibi dış bağımlılıklara ihtiyaç oluyorsa bunlarla uğraşmadan kodu test edebilmek için
    sahte bir nesne üretir ve bu nesnenin bizim istediğimiz gibi davranmasını sağlarız.
    
  Assert İşlemleri:
  
    Assert.Equal(x,y) -> iki değerin aynı olup olmadığına bakar
    Assert.NotEqual(x,y) -> iki değer birbirinden farklı mı diye bakar     
    Assert.True(x) -> Sonuç true mu diye bakar
    Assert.False(x) -> Sonuç false mi diye bakar
    Assert.Null(x) -> x nesnesi null mu diye bakar
