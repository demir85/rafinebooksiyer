//using Eticaret12.Data;

using Eticaret12.Data;
using Eticaret12.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Eticaret12.Helper
{
    public class HelperClass
    { 
        private readonly ApplicationDbContext _applicationDbcontext;
       private readonly EticaretVeritabaniContext _ctx;
        public HelperClass (ApplicationDbContext applicationDbcontext, EticaretVeritabaniContext eticaretVeritabaniContext)
        {  _applicationDbcontext = applicationDbcontext; 
        _ctx = eticaretVeritabaniContext;
        }
        public int sonIdAl()
        {
            string id = _applicationDbcontext.Roles.Select(satir=> satir.Id).Max();
            int idm =Convert.ToInt32(id);
            return ++idm;
        }

        public string RandomSifreOlustur()
        {
            string sifre = string.Empty;
            Random random = new Random();
            for (int i = 0; i <=10; i++)
            {
                int uretilenDeger = random.Next(65, 91);
                sifre += (char)uretilenDeger;
            }
           return sifre;
        }

        public string RandomSiparisNoOlustur()
        {
            string sifre = "";
            Random random = new Random();
            for (int i = 0; i < 6; i++) // 6 haneli şifre
            {
                sifre += random.Next(0, 10); //0-9 aralıgında bir değer üretecek
            }
            return sifre;
        }

        public bool SiparisNoVarmi(string siparisNo)
        {
           var dbdenGelen = _ctx.OrderNumberHistories.Where(c => c.OrderNumberCode == siparisNo).FirstOrDefault();
           if (dbdenGelen == null)
                return false;
           else
               return true;
        }
        public List<Tur> TurDatalariniEkle()
        {
            List < Tur > türs = new List<Tur>();
            türs.Add(new Tur() { TurAdi = "Roman", FotografYolu="/images/romann.jpeg" });
            türs.Add(new Tur() { TurAdi = "Şiir", FotografYolu = "/images/şiir1.jpeg" });
            türs.Add(new Tur() { TurAdi = "Kurgu-Bilim", FotografYolu = "/images/kurgubilim.jpeg" });
            türs.Add(new Tur() { TurAdi = "Öykü", FotografYolu = "/images/öykü.jpeg" });
            türs.Add(new Tur() { TurAdi = "Çizgi-Roman", FotografYolu = "/images/cizgiroman.jpeg" });
            türs.Add(new Tur() { TurAdi = "Kişisel Gelişim", FotografYolu = "/images/kisiselgelisim.jpeg" });
            türs.Add(new Tur() { TurAdi = "Polisiye", FotografYolu = "/images/polisiye.jpeg" });


            return türs;
        }

        public string BosluklariSil(string deger)
        {
            string result = string.Empty;
            //4444 3333 2222 1111:  ..... 4444333322221111 boşluksuz hale ceviren metot.
            string[] degerler = deger.Split(' ');
            foreach (var item in degerler)
            {
                result += item;
            }
            return result;
        }

        public string GetAlbumIds(List<Cart> degerler)
        {
            string result = string.Empty;
            foreach (var item in degerler)
            {
                result += item.AlbumId;
                result += ";";
            }
            return result;
        }


        public int ArananKarakterAdedi(string deger, char arananKarakter)
        {
            int adet = 0;
            foreach (var item in deger)
            {
                if (item == arananKarakter)
                {
                    adet++;
                }
            }
            return adet;
        }
    }
}




       
    

