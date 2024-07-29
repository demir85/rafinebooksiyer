//using Eticaret12.Data;
using Eticaret12.Data;
using Eticaret12.Helper;
using Eticaret12.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using SQLitePCL;

namespace Eticaret12.Controllers
{
    public class AlbumController : Controller
    {
        private readonly EticaretVeritabaniContext _context;
        private readonly ApplicationDbContext _applicationDbContext;

        public AlbumController(EticaretVeritabaniContext context, ApplicationDbContext appContext)
        {
            _context = context;
           _applicationDbContext = appContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles ="Admin")]
        //public IActionResult AdminSayfasi()
        //{
        //    return View();
        //}
        public IActionResult AlbumEkle()
        {
            ViewBag.turler = new SelectList(_context.Turss.OrderBy(satir=>satir.TurAdi), "TurId", "TurAdi");
            return View();
        }

        public IActionResult FotoYukle()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminSayfasi()
        { return View(); }

        [HttpPost]
        public IActionResult AlbumEkle(Album album , IFormFile file)
        {
            album.KitapArtUrl = "/uploads/" + file +/* ";" + User.Identity.Name + ";" */ Path.GetExtension(file.FileName);
            _context.Add(album);
            _context.SaveChanges();
            return RedirectToAction("Albumler"); 
        }
        public IActionResult Albumler()
        {
           List<Album> albumlerHepsi= _context.Albums.Include("Tur").Where(satir => satir.StokdaVarmı == true).ToList();
            return View(albumlerHepsi);
        }
       
        public IActionResult Edit(int Id)
        {
            var albumdetay = _context.Albums.First(satir => satir.AlbumId == Id);
            return View(albumdetay);

        }
        [HttpPost]
        public IActionResult Edit(int Id, Album album, IFormFile file)
        {
          var albumdetay=_context.Albums.First(satir => satir.AlbumId == Id);
            albumdetay.AlbumId = album.AlbumId;
            albumdetay.Baslik = album.Baslik;
            albumdetay.KitabınAdı = album.KitabınAdı;
            albumdetay.Fiyat = album.Fiyat;
            //albumdetay.KitapArtUrl = "/uploads/" + file.FileName + ";" + User.Identity.Name + ";" + Path.GetExtension(file.FileName);
            albumdetay.StokdaVarmı = album.StokdaVarmı;
          
            _context.Update(albumdetay);
            _context.SaveChanges();
            return RedirectToAction("Albumler");
        }
        public IActionResult Details(int Id)
        {
            var albumdetay = _context.Albums.First(satir => satir.AlbumId == Id);
            return View(albumdetay);

        }
        public IActionResult Delete(int Id)
        {
            var albumdetay = _context.Albums.First(satir => satir.AlbumId == Id);
            return View(albumdetay);

        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int AlbumId)
        {
            //Album silinecekAlbum= _context.Albums.Where(satir=>satir.KitapId==KitapId).FirstOrDefault();
            // _context.Remove(silinecekAlbum);
            // _context.SaveChanges();
            // return RedirectToAction("Albumler");

            Album silinecekAlbum = _context.Albums.Where(satir => satir.AlbumId == AlbumId).FirstOrDefault();
            silinecekAlbum.StokdaVarmı=false;
            _context.Update(silinecekAlbum);
            _context.SaveChanges();
            return RedirectToAction("Albumler");
        }

        public IActionResult TopluSilme()
        {
            List<Album> pasifler = _context.Albums.Where(satir=>satir.StokdaVarmı==false).ToList();
           foreach(var item in pasifler)
            {
                item.StokdaVarmı= true;
                _context.Update(item);
                _context.SaveChanges();
            }
            return RedirectToAction("Albumler");
        }
    }
}
