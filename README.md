
# Banka Otomasyon Projesi

Bu proje, basit bir banka otomasyon sistemini simüle eden bir C# uygulamasını içermektedir.

## Proje Hakkında

Bu proje, müşteri hesaplarını yönetme, para transferi yapma, bakiye sorgulama gibi temel bankacılık işlemlerini gerçekleştirmenizi sağlar. Ayrıca, PostgreSQL veritabanı kullanılarak müşteri ve hesap bilgilerini saklar.

## Başlangıç

Projeyi çalıştırmadan önce, aşağıdaki adımları izleyerek gerekli kurulumları gerçekleştirmeniz gerekmektedir.

### Gereksinimler

- Visual Studio veya bir C# derleyicisi
- PostgreSQL veritabanı

### Kurulum

1. GitHub reposunun ana sayfasında sağ üst köşede bulunan "Code" butonuna tıklayarak projeyi bilgisayarınıza indirin veya klonlayın.

    ```bash
    git clone https://github.com/kullaniciadi/banka-otomasyon-projesi.git
    ```

2. `BankaOtomasyonProjesi.sln` dosyasını Visual Studio veya tercih ettiğiniz C# geliştirme ortamında açın.

3. PostgreSQL veritabanınızı oluşturun ve bağlantı ayarlarınızı `appsettings.json` dosyasında güncelleyin.

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Host=localhost;Port=5432;Database=dburun;Username=postgres;Password=sifre"
      }
    }
    ```

4. Proje dosyalarınızı derleyin ve çalıştırın.

## Kullanım

Proje başlatıldığında, basit bir kullanıcı arayüzü ile karşılaşacaksınız. Müşteri hesaplarını ekleyebilir, para transferi yapabilir, bakiye sorgulayabilirsiniz.

## Katkıda Bulunma

Eğer projeye katkıda bulunmak istiyorsanız, lütfen bir çekme isteği oluşturun. Yeni özellikler ekleyebilir veya hataları düzeltebilirsiniz.


