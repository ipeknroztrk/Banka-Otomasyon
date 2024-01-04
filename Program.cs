using Npgsql;
using NpgsqlTypes;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;

namespace sqltest
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            { 
                NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder();
                builder.Host = "localhost";
                builder.Username = "postgres";
                builder.Password = "12345678";
                builder.Database = "dburun";

                NpgsqlConnection connection = new NpgsqlConnection(builder.ConnectionString);
                connection.Open();
                
                String sql;

                NpgsqlCommand command;

                int anamenusecim = 0, altmenusecim = 0, girissecim = 0;

            MENU:
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("BANKA OTOMASYONU MENÜSÜ");
                Console.WriteLine("----------------------");
                Console.WriteLine("1. Yeni müşteri kaydı oluşturma");
                Console.WriteLine("2. Varolan bir müşteri için hesap giriş");
                Console.WriteLine("3. Varolan bir müşteri için hesap bilgilerini görüntüleme");
                Console.WriteLine("4. Varolan bir müşterinin hesabına para yatırma");
                Console.WriteLine("5. Varolan bir müşterinin hesabından para çekme");
                Console.WriteLine("6. Hesap hareketlerini görüntüleme");
                Console.WriteLine("7. Varolan bir hesabı silme");
                Console.WriteLine("8. Şifre degiştirme.");
                Console.WriteLine("9. Çıkış yapma ve uygulamayı sonlandırma.");
                Console.Write("---> Seçiminizi yapınız [1-9]: ");
                Console.ResetColor();


                anamenusecim = Convert.ToInt32((Console.ReadLine()));
                //int türüne çevirdik
                if (anamenusecim == 1)
                {

                    Console.WriteLine("-----------------------------------------");

                    Console.WriteLine("Ad:");
                    string ad = Console.ReadLine();

                    Console.WriteLine("Soyad:");
                    string soyad = Console.ReadLine();

                    Console.WriteLine("TCK:");
                    long tck = Convert.ToInt64(Console.ReadLine());
                    //tck bigint türünde oldugu için long toınt64 kullandık

                    // id yi return edip bu id üzerinden kullanıcılar tablosuna erişmek için
                    string sqlMusteri = "INSERT INTO public.musteri(ad, soyad, tck) VALUES (@p1, @p2, @p3)  RETURNING id";
                    NpgsqlCommand commandMusteri = new NpgsqlCommand(sqlMusteri, connection);
                    commandMusteri.Parameters.AddWithValue("@p1", ad);
                    commandMusteri.Parameters.AddWithValue("@p2", soyad);
                    commandMusteri.Parameters.AddWithValue("@p3", tck);
                    long id = Convert.ToInt64(commandMusteri.ExecuteScalar());
                    //command.executescalar() ifadesi id den geri dönen degeri döndürcek

                    Console.WriteLine("Kullanıcı adı:");
                    string kullaniciadi = Console.ReadLine();

                    Console.WriteLine("Şifre:");
                    long sifre = Convert.ToInt64(Console.ReadLine());


                    string sqlKullanici = "INSERT INTO public.kullaniciler( kullaniciadi, sifre,musteri_id) VALUES (@p4, @p5,@p6)";
                    NpgsqlCommand commandKullanici = new NpgsqlCommand(sqlKullanici, connection);
                    commandKullanici.Parameters.AddWithValue("@p4", kullaniciadi);
                    commandKullanici.Parameters.AddWithValue("@p5", sifre);
                    commandKullanici.Parameters.AddWithValue("@p6", id);
                    // burada p6 ya id yi atıp iki tablo arasında geçişi yapmış olduk
                    commandKullanici.ExecuteNonQuery();

                    //her yeni kullanıcı oluşturdugumda rastgele bakiye atasın 1000-50000 arası olsun 
                    Random random = new Random();
                    decimal bakiye = random.Next(100, 500000);

                    string sqlHesap = "INSERT INTO public.hesapp(musteri_id, bakiye) VALUES (@p7, @p8)";
                    NpgsqlCommand commandHesap = new NpgsqlCommand(sqlHesap, connection);
                    commandHesap.Parameters.AddWithValue("@p7", id);
                    commandHesap.Parameters.AddWithValue("@p8", bakiye);
                    commandHesap.ExecuteNonQuery();
                    Console.Clear();
                    Console.WriteLine("Yeni müşteri ve kullanıcı oluşturuldu.");
                    Console.WriteLine("------------------------------------------------------------");


                ALTMENU:
                    Console.WriteLine("Seçiminizi yapınız [1-->Ana menüye dön 2-->Programı kapat]: ");
                    altmenusecim = Convert.ToInt32(Console.ReadLine());
                    if (altmenusecim == 1)
                    {
                        goto MENU;
                        
                    }
                    else if (altmenusecim == 2)
                    {
                        Environment.Exit(0);

                    }
                    else
                    {
                        Console.WriteLine("Hatalı seçim yaptınız tekrar deneyiniz!!");
                        goto ALTMENU;
                        
                    }



                }
                else if (anamenusecim == 2)
                {
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine("Kullanıcı adı:");
                    string kullaniciadi = Console.ReadLine();

                    Console.WriteLine("Şifre: ");
                    long sifre = Convert.ToInt32(Console.ReadLine());

                    Console.Clear();

                    sql = "SELECT COUNT(*) FROM kullaniciler WHERE kullaniciadi=@kullaniciadi AND sifre=@sifre ";
                    // burada count ile kullanıcı adı ve şifreye sahip kullanıcıların sayısını döndürücek
                    // eger 0 dönerse kayıt bulunamadı sıfırdan büyük deger dönerse kullanıcı adı ve şifre var dicek
                    command = new NpgsqlCommand(sql, connection);

                    command.Parameters.AddWithValue("kullaniciadi", kullaniciadi);
                    command.Parameters.AddWithValue("sifre", sifre);
                    int sonuc = Convert.ToInt32(command.ExecuteScalar());
                    if (sonuc > 0)
                    {
                        Console.WriteLine("Başarılı giriş yaptınız  " + kullaniciadi);
                    }
                    else
                    {
                        Console.WriteLine("Kullanıcı adı veya parola yanlış");

                    }

                    Console.WriteLine("------------------------------------------------------------");

                ALTMENU:
                    Console.WriteLine("Seçiminizi yapınız [1-->Ana menüye dön 2-->Programı kapat]: ");
                    altmenusecim = Convert.ToInt32(Console.ReadLine());
                    if (altmenusecim == 1)
                    {
                        goto MENU;
                    }
                    else if (altmenusecim == 2)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("Hatalı seçim yaptınız tekrar deneyiniz!!");
                        goto ALTMENU;
                    }

                }

                else if (anamenusecim == 3)

                {
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine("TC Kimlik Numarası:");
                    long tck = Convert.ToInt64(Console.ReadLine());

                    string sqlMusteri = "SELECT ad, soyad, tck FROM public.musteri WHERE tck=@p1";
                    NpgsqlCommand commandMusteri = new NpgsqlCommand(sqlMusteri, connection);
                    commandMusteri.Parameters.AddWithValue("@p1", tck);


                    NpgsqlDataReader reader = commandMusteri.ExecuteReader();
                    if (reader.Read())//okunacak bir veri varsa true döner ve blogun içine girer
                    {
                        string ad = reader["ad"].ToString();
                        string soyad = reader["soyad"].ToString();
                        long tc = Convert.ToInt64(reader["tck"]);
                        // reader nesnesinden sırasıyla ad soyad ve tck bilgileirni alır

                        Console.WriteLine("******************");
                        Console.WriteLine("Müşteri Bilgileri:");
                        Console.WriteLine($"Ad: {ad}");
                        Console.WriteLine($"Soyad: {soyad}");
                        Console.WriteLine($"TCK: {tc}");


                        reader.Close();

                        string sqlHesap = "SELECT bakiye FROM public.hesapp WHERE musteri_id=(SELECT id FROM public.musteri WHERE tck=@p2)";
                        //iç içe sorgu ile girilen tc den musteri_idye erişilir
                        NpgsqlCommand commandHesap = new NpgsqlCommand(sqlHesap, connection);
                        commandHesap.Parameters.AddWithValue("@p2", tck);

                        decimal bakiye = Convert.ToDecimal(commandHesap.ExecuteScalar());
                        //bakiye degerini döndürür

                        Console.WriteLine($"Bakiye: {bakiye} TL");
                        Console.WriteLine("******************");
                    }
                    else
                    {
                        Console.WriteLine("Kayıtlı müşteri bulunamadı.");
                    }

                    Console.WriteLine("------------------------------------------------------------");

                ALTMENU:
                    Console.WriteLine("Seçiminizi yapınız [1-->Ana menüye dön 2-->Programı kapat]: ");
                    altmenusecim = Convert.ToInt32(Console.ReadLine());
                    if (altmenusecim == 1)
                    {
                        goto MENU;
                    }
                    else if (altmenusecim == 2)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("Hatalı seçim yaptınız tekrar deneyiniz!!");
                        goto ALTMENU;
                    }

                }



                else if (anamenusecim == 4)
                {
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine("Hesap ID:");
                    long hesap_id = Convert.ToInt64(Console.ReadLine());

                    Console.WriteLine("Miktar:");
                    long miktar = Convert.ToInt64(Console.ReadLine());

                    string bakiyesorgusu = "SELECT bakiye FROM hesapp WHERE hesap_id=@hesap_id";
                    //hesap_id ye göre bakiye degeri döndürülür ve girilen miktar üzerinee eklenip yeni bakiye olusur
                    NpgsqlCommand bakiyecommand = new NpgsqlCommand(bakiyesorgusu, connection);
                    bakiyecommand.Parameters.AddWithValue("@hesap_id", hesap_id);
                    long bakiye = Convert.ToInt64(bakiyecommand.ExecuteScalar());
                    bakiye += miktar;

                    string bakiyegüncelle = "UPDATE hesapp SET bakiye=@bakiye WHERE hesap_id=@hesap_id";
                    // girilem hesap id ye göre yeni bakiye degeri güncellernir
                    NpgsqlCommand güncellecommand = new NpgsqlCommand(bakiyegüncelle, connection);
                    güncellecommand.Parameters.AddWithValue("@bakiye", bakiye);
                    güncellecommand.Parameters.AddWithValue("@hesap_id", hesap_id);
                    güncellecommand.ExecuteNonQuery();

                    string hareketekleme = "INSERT INTO hesap_hareketleri(hesap_id,islem_tarihi,islem_turu,miktar)VALUES(@hesap_id,@islem_tarihi,@islem_turu,@miktar)";
                    NpgsqlCommand hareketcommand = new NpgsqlCommand(hareketekleme, connection);
                    hareketcommand.Parameters.AddWithValue("@hesap_id", hesap_id);
                    hareketcommand.Parameters.AddWithValue("@islem_tarihi", DateTime.Now);
                    hareketcommand.Parameters.AddWithValue("@islem_turu", "Para Yatırma");
                    hareketcommand.Parameters.AddWithValue("@miktar", miktar);
                    hareketcommand.ExecuteNonQuery();

                    Console.WriteLine("Para yatırma işlemi gerçekleşti.");

                    Console.WriteLine($"Güncel bakiyeniz: {bakiye}");

                    Console.WriteLine("------------------------------------------------------------");
                ALTMENU:

                    Console.WriteLine("Seçiminizi yapınız [1-->Ana menüye dön 2-->Programı kapat]: ");
                    altmenusecim = Convert.ToInt32(Console.ReadLine());
                    if (altmenusecim == 1)
                    {
                        goto MENU;
                    }
                    else if (altmenusecim == 2)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("Hatalı seçim yaptınız tekrar deneyiniz!!");
                        goto ALTMENU;
                    }



                }
                else if (anamenusecim == 5)
                {
                    Console.WriteLine("-----------------------------------------");

                    Console.WriteLine("Hesap ID:");
                    long hesap_id = Convert.ToInt64(Console.ReadLine());

                    Console.WriteLine("Miktar:");
                    long miktar = Convert.ToInt64(Console.ReadLine());

                    string bakiyesorgusu = "SELECT bakiye FROM hesapp WHERE hesap_id=@hesap_id";
                    //para yatırmadaki işlemin aynısını yapıyoruz sadece bu sefer -= yapıcaz
                    NpgsqlCommand bakiyecommand = new NpgsqlCommand(bakiyesorgusu, connection);
                    bakiyecommand.Parameters.AddWithValue("@hesap_id", hesap_id);
                    long bakiye = Convert.ToInt64(bakiyecommand.ExecuteScalar());

                    if (miktar <= 0)
                    {
                        Console.WriteLine("Geçersiz miktar. Lütfen pozitif bir değer girin.");
                        return;
                    }

                    if (bakiye < miktar)
                    {
                        //çekmek istenilen tutar bakiyeden fazla ise mesajı yazdırır
                        Console.WriteLine("Hesapta yeterli bakiye bulunmamaktadır.");
                        return;
                    }

                    bakiye -= miktar;

                    string bakiyegüncelle = "UPDATE hesapp SET bakiye=@bakiye WHERE hesap_id=@hesap_id";
                    NpgsqlCommand güncellecommand = new NpgsqlCommand(bakiyegüncelle, connection);
                    güncellecommand.Parameters.AddWithValue("@bakiye", bakiye);
                    güncellecommand.Parameters.AddWithValue("@hesap_id", hesap_id);
                    güncellecommand.ExecuteNonQuery();

                    string hareketekleme = "INSERT INTO hesap_hareketleri(hesap_id,islem_tarihi,islem_turu,miktar)VALUES(@hesap_id,@islem_tarihi,@islem_turu,@miktar)";
                    NpgsqlCommand hareketcommand = new NpgsqlCommand(hareketekleme, connection);
                    hareketcommand.Parameters.AddWithValue("@hesap_id", hesap_id);
                    hareketcommand.Parameters.AddWithValue("@islem_tarihi", DateTime.Now);
                    hareketcommand.Parameters.AddWithValue("@islem_turu", "Para Çekme");
                    hareketcommand.Parameters.AddWithValue("@miktar", miktar);
                    hareketcommand.ExecuteNonQuery();

                    Console.WriteLine("Para çekme işlemi gerçekleşti.");

                    Console.WriteLine($"Güncel bakiyeniz: {bakiye}");
                    Console.WriteLine("------------------------------------------------------------");

                ALTMENU:
                    Console.WriteLine("Seçiminizi yapınız [1-->Ana menüye dön 2-->Programı kapat]: ");
                    altmenusecim = Convert.ToInt32(Console.ReadLine());
                    if (altmenusecim == 1)
                    {
                        goto MENU;
                    }
                    else if (altmenusecim == 2)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("Hatalı seçim yaptınız tekrar deneyiniz!!");
                        goto ALTMENU;
                    }


                }

                else if (anamenusecim == 6)
                {
                    Console.WriteLine("-----------------------------------------");

                    Console.WriteLine("Hesap ID:");
                    long hesap_id = Convert.ToInt64(Console.ReadLine());

                    string hareketlerSorgusu = "SELECT * FROM hesap_hareketleri WHERE hesap_id=@hesap_id";
                    //girilen hesapid ye göre hesao haraketleri tablosuna eriştik
                    NpgsqlCommand hareketlerCommand = new NpgsqlCommand(hareketlerSorgusu, connection);
                    hareketlerCommand.Parameters.AddWithValue("@hesap_id", hesap_id);

                    using (NpgsqlDataReader hareketlerReader = hareketlerCommand.ExecuteReader())
                    {
                        Console.WriteLine("Hesap Hareketleri:");
                        Console.WriteLine("-------------------");
                        Console.WriteLine("Hareket ID\t| İşlem Tarihi\t\t| İşlem Türü\t| Miktar\t\t");

                        while (hareketlerReader.Read())
                        {//okunacak veri kalmayana kadar okumaya devam eder
                            long hareket_id = hareketlerReader.GetInt64(0);
                            DateTime islem_tarihi = hareketlerReader.GetDateTime(2);
                            string islem_turu = hareketlerReader.GetString(3);
                            long miktar = hareketlerReader.GetInt64(4);

                            Console.WriteLine($"{hareket_id}\t\t| {islem_tarihi}\t| {islem_turu}\t| {miktar}");
                        }
                    }
                    Console.WriteLine("------------------------------------------------------------");
                ALTMENU:
                    Console.WriteLine("Seçiminizi yapınız [1-->Ana menüye dön 2-->Programı kapat]: ");
                    altmenusecim = Convert.ToInt32(Console.ReadLine());
                    if (altmenusecim == 1)
                    {
                        goto MENU;
                    }
                    else if (altmenusecim == 2)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("Hatalı seçim yaptınız tekrar deneyiniz!!");
                        goto ALTMENU;
                    }
                }

                else if (anamenusecim == 7)
                {
                    Console.WriteLine("-----------------------------------------");

                    Console.WriteLine("Silmek istediğiniz hesabın ID'sini giriniz:");
                    long id = Convert.ToInt64(Console.ReadLine());

                    Console.WriteLine("Hesap ve bağlı tablolardaki kayıtlar siliniyor, onaylıyor musunuz? [E/H]");
                    string cevap = Console.ReadLine();

                    if (cevap.ToUpper() == "E")
                    {
                        
                        string silmeHesapHareketleriSorgusu = "DELETE FROM public.hesap_hareketleri WHERE hesap_id = @id";
                        NpgsqlCommand silmeHesapHareketleriCommand = new NpgsqlCommand(silmeHesapHareketleriSorgusu, connection);
                        silmeHesapHareketleriCommand.Parameters.AddWithValue("@id", id);
                        silmeHesapHareketleriCommand.ExecuteNonQuery();

                        string silmeKullanicilerSorgusu = "DELETE FROM public.kullaniciler WHERE musteri_id = @id";
                        NpgsqlCommand silmeKullanicilerCommand = new NpgsqlCommand(silmeKullanicilerSorgusu, connection);
                        silmeKullanicilerCommand.Parameters.AddWithValue("@id", id);
                        silmeKullanicilerCommand.ExecuteNonQuery();

                        string silmeMusteriSorgusu = "DELETE FROM public.musteri WHERE id = @id";
                        NpgsqlCommand silmeMusteriCommand = new NpgsqlCommand(silmeMusteriSorgusu, connection);
                        silmeMusteriCommand.Parameters.AddWithValue("@id", id);
                        int silinenMusteriKayitSayisi = silmeMusteriCommand.ExecuteNonQuery();

                        if (silinenMusteriKayitSayisi > 0)
                        {
                            Console.WriteLine("Hesap ve bağlı tablolardaki kayıtlar başarıyla silindi.");
                        }
                        else
                        {
                            Console.WriteLine("Silinecek kayıt bulunamadı.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Silme işlemi iptal edildi.");
                    }

                    Console.WriteLine("------------------------------------------------------------");

                ALTMENU:
                    Console.WriteLine("Seçiminizi yapınız [1-->Ana menüye dön 2-->Programı kapat]: ");
                    altmenusecim = Convert.ToInt32(Console.ReadLine());
                    if (altmenusecim == 1)
                    {
                        goto MENU;
                    }
                    else if (altmenusecim == 2)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("Hatalı seçim yaptınız tekrar deneyiniz!!");
                        goto ALTMENU;
                    }

                }


                else if (anamenusecim == 8)
                {
                    Console.WriteLine("-----------------------------------------");

                    Console.WriteLine("Kullanıcı adınızı giriniz:");
                    string kullaniciAdi = Console.ReadLine();

                    Console.WriteLine("Mevcut şifrenizi giriniz:");
                    long mevcutSifre = Convert.ToInt64(Console.ReadLine());

                    string sifreSorgu = "SELECT COUNT(*) FROM kullaniciler WHERE kullaniciadi=@kullaniciadi AND sifre=@sifre";
                    //databasede kullanıcı adı ve sifre eşleşmesi sorgusu apılıyor
                    NpgsqlCommand sorguCommand = new NpgsqlCommand(sifreSorgu, connection);
                    sorguCommand.Parameters.AddWithValue("@kullaniciadi", kullaniciAdi);
                    sorguCommand.Parameters.AddWithValue("@sifre", mevcutSifre);

                    int count = Convert.ToInt32(sorguCommand.ExecuteScalar());
                    // eşleşen 0 dan farklı hesap varsa ife girer
                    if (count > 0)
                    {
                        Console.WriteLine("Yeni şifrenizi giriniz:");
                        long yeniSifre = Convert.ToInt64(Console.ReadLine());

                        string güncelleSifreSorgu = "UPDATE kullaniciler SET sifre=@yenisifre WHERE kullaniciadi=@kullaniciadi";
                        //sifreyi güncelleyip sifreye yeni sifreyi atadık
                        NpgsqlCommand güncelleCommand = new NpgsqlCommand(güncelleSifreSorgu, connection);
                        güncelleCommand.Parameters.AddWithValue("@yenisifre", yeniSifre);
                        güncelleCommand.Parameters.AddWithValue("@kullaniciadi", kullaniciAdi);

                        int rowsAffected = güncelleCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Şifreniz başarıyla güncellendi.");
                        }
                        else
                        {
                            Console.WriteLine("Şifre güncelleme işlemi başarısız.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Kullanıcı adı veya mevcut şifre hatalı. Şifre değiştirme işlemi iptal edildi.");
                    }
                    Console.WriteLine("------------------------------------------------------------");
                ALTMENU:
                    Console.WriteLine("Seçiminizi yapınız [1-->Ana menüye dön 2-->Programı kapat]: ");
                    altmenusecim = Convert.ToInt32(Console.ReadLine());
                    if (altmenusecim == 1)
                    {
                        goto MENU;
                    }
                    else if (altmenusecim == 2)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("Hatalı seçim yaptınız tekrar deneyiniz!!");
                        goto ALTMENU;
                    }

                }


                else if (anamenusecim == 9)
                {
                    Console.WriteLine("Uygulamadan çıkılıyor...");
                    Environment.Exit(0);
                }


                connection.Close();
            }

            catch (NpgsqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("\nDone. Press enter.");
            Console.ReadKey();


        }
    }
}
