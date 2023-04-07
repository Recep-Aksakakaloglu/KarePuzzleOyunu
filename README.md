# KarePuzzleOyunu
## Proje Konusu
Proje konusu 4x4 bir kare puzzle oyunudur. Mause ile parça kontrolü yapılmaktadır. Birinci tıklanan resim ile ikinci tıklanan resim yer değiştirmektedir. Her doğru değişimde +10 puan her yanlış değişimde -10 puan yazılmaktadır. Bu oyunun sonunda puan bilgileri bir adet txt dosyasının içinde tutulmaktadır. Görsel ekle butonu bulunmaktadır ve bu buton ile istediğiniz herhangi bir görseli oyunda kullanabilirsiniz. Karıştır buttonu ile oyun başlamaktadır.

## Kullanılan Teknolojiler
### A. C#
![alt text](https://upload.wikimedia.org/wikipedia/commons/thumb/0/0d/C_Sharp_wordmark.svg/2048px-C_Sharp_wordmark.svg.png)
* C# yani diğer bir adıyla C Sharp, Microsoft tarafından geliştirilen sunucu ve gömülü sistemleri çalıştırmak için tasarlanmış programlama dilidir. .NET Framework ortamında kullanılmak üzere geliştirilmiş nesne tabanlı programlama dillerinden birisidir.

### B. Windows Form
![alt text](https://www.infragistics.com/community/cfs-file/__key/communityserver-blogs-components-weblogfiles/00-00-00-04-34/1768.dev_2D00_tools_2D00_Windows_2D00_Forms_2D00_release_2D00_notes.jpg)
* Windows Forms, .NET Framework'te zengin istemci uygulamaları geliştirmek için tasarlanmış bir dizi yönetilen kitaplıktır. İstemci uygulamalarında daha kolay dağıtım ve daha iyi güvenlik ile verileri görüntülemek ve kullanıcı etkileşimlerini yönetmek için grafiksel bir API'dir.

* Windows Forms, yönetilen koddan yerel Windows grafik arabirim öğelerine ve grafiklerine erişmek için arabirim sağlayan kapsamlı bir istemci kitaplığı sunar. Windows istemcilerine benzer olay güdümlü mimari ile inşa edilmiştir ve bu nedenle uygulamaları yürütülmesi için kullanıcı girişini bekler.

### C. Bağlı Listeler
![alt text](https://zahidtekbas.com.tr/wp-content/uploads/2017/09/linked_list.jpg)

* Bağlı liste, dizilerden sonra en çok kullanılan veri yapısıdır. Bağlı listeler, dinamik veri yapılarıdır. Dizilerde olduğu gibi başta kaç tane elemanla çalışılacağı belirtilmek zorunda değildir. Dizilerde olduğu gibi eleman ekleme, silme işlemleri uygulanabilir ve araya eleman eklenebilir.

* Linked List’in avantajı, hafızayı dinamik olarak kullanmasıdır. Buna göre hafızadan silinen bir bilgi için hafıza alanı boşaltılacak veya yeni eklenen bir bilgi için sadece o bilgiyi tutmaya yetecek kadar hafıza alanı ayrılacaktır.

* Single Linked Liste göre Düğüm, bir sonraki düğüme bir pointer içerir. Bu sayede düğüm, kendinden sonraki düğümün adresini bilir.
* Double Linked Liste göre her düğüm, hem kendinden öncekine hem de kendinden sonrakine bağlanır, bu sayede liste üzerinde ileri ve geri ilerlemek mümkündür.

## Projenin Gerçekleştirilmesi
Projede iki adet form ekranı bulunmaktadır. Bunlar giriş ekranı ve oyun ekranıdır. Bu ekranlara gerekli konsepte uygun arkaplan resimleri ve Form komponentleriyle göze hoş gelen bir sayfa tasarımı oluşturuldu.

### A. Giriş Ekranı
* Giriş ekranı label, textbox, ve butonlar ile oluşturulmuştur. Sayfanın amacı textboxa kullanıcı adı girip buton ile sayfalar arası geçiş yapmaktır. Üzerine puzzle görseli eklediğimiz buton onclick eventi ile giriş ekranına geçiş yapılmaktadır. Burada önemli olan nokta if else koşulu ile text boxa boş alan kontrolü eklenmiştir ve eğer isim alanı boş bırakılırsa bu konuda uyarı veren bir messageBox görünmektedir.

### B. Oyun Ekranı
* Oyuncu ekranının labellar ve butonlardan oluştuğu görülmektedir. 
Giriş ekranının ve oyun ekranının üst panelini varsayılan windows paneli olmaktan çıkarıp kendi özgün panelimizle değiştirdik.

* İşleyiş olarak ilk kullanılan buton görsel ekleme butonudur. Bu buton adı üzerinde görsel eklemeye yaramaktadır. Seçilen görsel 16 adet ayrı buton üzerine konuşlandırılmaktadır. Öncelikli olarak open file dialog komutu ile dosya seçim ekranı açılmaktadır. Dosya seçimi sadece görsel uzantıları ile yapılabilmektedir (.jpg / .png). Sonrasında görseli her zaman 500x500 boyutlarıne çevirip for döngüsü yardımı ile görsel 16 parçaya bölünmüş şekilde butonlara konumlandırılmaktadır.
* Resimler üzerinde işlem yapmak için ayrıca graphics sınıfından faydalanılmıştır. Bu da buton üzerindeki görselleri istediğimiz şekilde boyutlandırmamıza yarıyor. For döngüsü aslında burada butonların arkaplan resimleri için dizi oluşturmaya yaramaktadır.
Bu işlem sırasında herhangi bir aksi durum yaşanırsa kullandığımı try catch yöntemi ile hatayı yakalayıp messagebox üzerine yazdırıyoruz.

* Karıştır butonu ise görsellerin konumunu değiştirmeye yaramaktadır. Aslında bu buton bir nevi oyunu da başlatan butondur. Bu buton aynı zamanda kendisi dışında 16 adet butonun kontrolünü sağlamaktadır. Bu sebepten dolayı bütün butonlar bir dizi içine atanmıştır ve bu dizide bağlı liste içerisinde kullanılmıştır. Buradaki bağlı liste foreach döngüsü yardımı ile  ButtonArray isimli bir buton eventi oluşturmaktadır ve bu event sonra anlatacağımız parçaların yer değiştirmesi olayını sağlamaktadır.  Enumerable.Range komutu kullanılmıştır bu komut LINQ ile kullanılabilecek bir tamsayı aralığı sağlar. Biz burada 0 ve 16 arası bir aralık tanımladık.  Sonrasında NewGuid ile yeni bir guid oluşturup .take yöntemi ile dizimizdeki eleman sayısını verdik.

* ButtonArray isimli eventten bahsetmiştik. Bu event parça değişimini sağlamaktadır. Bir adet sayaç bulunmaktadır ve event çalıştığında değeri 0 olan bu sayacın değeri herhangi bir butona tıklandığında artmaktadır. Sayac 1 olduğu zaman bu tıklanan değer bizim ilk değerimiz Sayac 2 olduğu zaman tıklanan değer değişecek değerimiz olacaktır ve sonrasında sayac değeri tekrardan sıfırlanacaktır. Sonrasında her işlemde hamle sayısı tek tek artmaktadır. 

* Sonrasında yeni bir metod çağırılır bu metod buton kontrol amacını taşımaktadır. Bu metodun çalışma algoritması 1. Node’un value değerinin ikinci Node’un arkaplan resmine eşit olup olmamasına dayanıyor ve bunun için ise if else koşul döngüleri kullanılmış ve for döngüsü ile de bu kontrol buton adedi kadar tekrarlanmıştır. 

* Txt işlemleri için using System.IO kütüphanesini uygulamaya dahil ettik. Txt işlemlerindeki sıralama; dosya oluşturma, skoru dosyaya yazma ve dosyadaki skoru ekrana yazdırmaktır. 

* Dosya oluşturmak için create.file komutu ile belirlenen dosya dolu üzerinden uygulanır. Sonrasında Stream Writer ile enyuksekskor.txt isimli dosyanın bulunduğu adrese puanın, oyuncu adının ve hamle sayısının yazılı olduğu labellardaki değer atanır.	Sonrasında bir adet String dizi oluşturulur ve bu dosyadaki veriler bir dizi elemanı olur. Sonrasında ise dizi ekrana yazdırılır. Dosya işlemlerinde iki adet püf nokta vardır. Bunlardan birincisi her yapılan dosya işleminin ardından .Close komutu ile işlemin sonlandırılması ikinci ise dosyaya değer yazdırırken dosya yolunun sağ tarafına true anahtar kelimesini eklemek bu anahtar kelime yazılan değerlerin satır satır yazılmasını sağlar. 
* Ekranın sağ altında bir adet datagridview aracı bulunmaktadır. Bunun sebebi Oyunu oynayan bütün oyuncuların Puan, Ad ve Hamle bilgilerini göstermektir.

* Ekranda bir adet kronometre bulunmaktadır. Bu kronometrenin çalışması için timer komponenti kullanılmıştır. Parçaları karıştır butonuna tıklandığında kronometre Timer.Start() ile başlar ve sayac 16 olduğu zaman Timer.Stop() durdurulur. Kronometre değerleri ise basit matematik işlemleriyle yapılmıştır salise, dakika, saniye, ve saat değerleri 0 olan değişkenler olarak tanımlanmış ve sonrasında salise 60 olduğu zaman saniye 1 olacak şekilde artacak sonrasında saniye 60 olduğunda dakika 1 olacak şekilde artacak ve dakika 60 olduğu zaman saat 1 olacak şekilde artacaktır. Bunlar if koşulları ile sayaç mantığında uygulanacaktır.

* Oyun sırasında yer değiştirilen butonların doğruluk kontrolünü sol üst satırdan başlarak aşağıya ve en aşağıya geldikten sonra üst soldan ikinci butondan da aynı şekilde devam edecek şekilde yaptık. Her yer değiştirme işleminden sonra oyun butonların doğruluk kontrolünü bu belirttiğimiz sıraya göre yapar. Eğer bir görsel olması gereken yerde değilse kontrol sonlanır ve yeniden parça değişimini bekler.

* Oyuncu bütün görselleri doğru yerlere yerleştirdiğinde karşısına kendisini tebrik eden ve puanını gösteren bir messagebox çıkacaktır. 
Aldığı bu puan oyuncu adı ve hamle bilgisiyle birlikte txt dosyasına kaydedilecektir.

* Oyuncu oyunu yeniden oynamak isterse veya sıra başka oyuncuya geçtiyse oyunun sol üstündeki restart butonu ile yeniden oyuna giriş ekranına oyuncu adını girmek üzere gönderilecektir.

## Uygulama Görselleri

### A. Giriş Ekranı
![alt text](https://github.com/Recep-Aksakakaloglu/KarePuzzleOyunu/blob/master/Giri%C5%9F%20Ekran%C4%B1.PNG?raw=true)

### B. Oyun Ekranı
![alt text](https://github.com/Recep-Aksakakaloglu/KarePuzzleOyunu/blob/master/Oyun%20Ekran%C4%B1.PNG?raw=true)

## Yararlandığımız Kaynaklar
* https://stackoverflow.com/questions/45857304/clarify-what-select-does
* https://stackoverflow.com/questions/13625891/cut-an-image-into-9-pieces-c-sharp
* https://stackoverflow.com/questions/1922040/how-to-resize-an-image-c-sharp
* https://stackoverflow.com/questions/3167971/how-can-i-transform-or-copy-an-array-to-a-linked-list
* https://www.youtube.com/watch?v=Wrnrl5ODxh4

## Hazırlayanlar
### Recep Aksakaloğlu
### Yılmaz Özkan
