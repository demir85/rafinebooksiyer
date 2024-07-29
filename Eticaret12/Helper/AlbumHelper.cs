using Eticaret12.Data;
using Eticaret12.Models;

namespace Eticaret12.Helper
{
    public class AlbumHelper
    {
        private readonly EticaretVeritabaniContext _context;

        public AlbumHelper(EticaretVeritabaniContext context)
        {
            _context = context;
        }
       
        public void AddAlbums()
        {
            if (_context.Albums.ToList().Count == 0)
            {
                var liste = new List<Album>
                {
new Album { Baslik = "The Best Of Men At Work", Tur = _context.Turss.Single(g => g.TurAdi == "Rock"), Fiyat = 8.99M,KitapArtUrl = "/images/default.jpeg" },
new Album { Baslik = "Worlds", Tur = _context.Turss.Single(g => g.TurAdi == "Jazz"), Fiyat = 8.99M, KitapArtUrl = "/images/default.jpeg" },
new Album { Baslik = "For Those About To Rock We Salute You", Tur = _context.Turss.Single(g => g.TurAdi == "Rock"), Fiyat = 8.99M, KitapArtUrl = "/images/default.jpeg" },
new Album { Baslik = "Let There Be Rock", Tur = _context.Turss.Single(g => g.TurAdi == "Rock"), Fiyat = 8.99M, KitapArtUrl = "/images/default.jpeg" },
new Album { Baslik = "Balls to the Wall", Tur = _context.Turss.Single(g => g.TurAdi == "Rock"), Fiyat = 8.99M,  KitapArtUrl = "/images/default.jpeg" },
new Album { Baslik = "Restless and Wild", Tur = _context.Turss.Single(g => g.TurAdi == "Rock"), Fiyat = 8.99M,  KitapArtUrl = "/images/default.jpeg" },
new Album { Baslik = "Big Ones", Tur = _context.Turss.Single(g => g.TurAdi == "Rock"), Fiyat = 8.99M, KitapArtUrl = "/images/default.jpeg" },
new Album { Baslik = "Quiet Songs", Tur = _context.Turss.Single(g => g.TurAdi == "Jazz"), Fiyat = 8.99M,  KitapArtUrl = "/images/default.jpeg" },
new Album { Baslik = "Jagged Little Pill", Tur = _context.Turss.Single(g => g.TurAdi == "Rock"), Fiyat = 8.99M,  KitapArtUrl = "/images/default.jpeg" },
new Album { Baslik = "Facelift", Tur = _context.Turss.Single(g => g.TurAdi == "Rock"), Fiyat = 8.99M, KitapArtUrl = "/images/default.jpeg" },
new Album { Baslik = "Frank", Tur = _context.Turss.Single(g => g.TurAdi == "Pop"), Fiyat = 8.99M, KitapArtUrl = "/images/default.jpeg" },
new Album { Baslik = "Warner 25 Anos", Tur = _context.Turss.Single(g => g.TurAdi == "Jazz"), Fiyat = 8.99M, KitapArtUrl = "/images/default.jpeg" },
new Album { Baslik = "Audioslave", Tur = _context.Turss.Single(g => g.TurAdi == "Rock"), Fiyat = 8.99M, KitapArtUrl = "/images/default.jpeg" },
new Album { Baslik = "The Best Of Billy Cobham", Tur = _context.Turss.Single(g => g.TurAdi == "Jazz"), Fiyat = 8.99M, KitapArtUrl = "/images/default.jpeg" },
new Album { Baslik = "Chronicle, Vol. 1", Tur = _context.Turss.Single(g => g.TurAdi == "Rock"), Fiyat = 8.99M, KitapArtUrl = "/images/default.jpeg" },
new Album { Baslik = "Chronicle, Vol. 2", Tur = _context.Turss.Single(g => g.TurAdi == "Rock"), Fiyat = 8.99M, KitapArtUrl = "/images/default.jpeg" },
new Album { Baslik = "Into The Light", Tur = _context.Turss.Single(g => g.TurAdi == "Rock"), Fiyat = 8.99M, KitapArtUrl = "/images/default.jpeg" },
new Album { Baslik = "Come Taste The Band", Tur = _context.Turss.Single(g => g.TurAdi == "Rock"), Fiyat = 8.99M, KitapArtUrl = "/images/default.jpeg" },
new Album { Baslik = "Deep Purple In Rock", Tur = _context.Turss.Single(g => g.TurAdi == "Rock"), Fiyat = 8.99M, KitapArtUrl = "/images/default.jpeg" },

                };


                foreach (var item in liste)
                {
                    item.KitabınAdı = item.Baslik;
                    _context.Albums.Add(item);
                }
                _context.SaveChanges();
            }
        }
    }
}
    

