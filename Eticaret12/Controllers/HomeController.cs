//using Eticaret.Helper;
using Eticaret12.Data;
using Eticaret12.Helper;
using Eticaret12.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Eticaret12.Controllers
{
    public class HomeController : Controller
	{
		private ShoppingCart shoppingCart;
		private readonly ApplicationDbContext _applicationDbContext;
        private readonly EticaretVeritabaniContext _eticaretVeriTabaniContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext applicationDbContext , EticaretVeritabaniContext eticaretContext)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
            _eticaretVeriTabaniContext = eticaretContext;
            shoppingCart = new ShoppingCart();
            shoppingCart._context = eticaretContext;
        }

        public IActionResult Index()
        {
            HttpContext.Session.SetString("siteAdi", "rafinebooksiyer");
            var cart = shoppingCart.GetCart(this.HttpContext);
            HttpContext.Session.SetString("adet", cart.GetCount().ToString ());
            ViewData["SepetAdet"] = HttpContext.Session.GetString("adet");
            HttpContext.Session.Clear();

            ///mail çalýþýyor mu deneyi
           // try
            //{
               // MailSender sender = new MailSender(_applicationDbContext);
               // var task = sender.SendEmailAsync("suled307@gmail.com", "hoþ geldniz", "naber");
           // }
            //	MailSender sender = new MailSender(_applicationDbContext);
            //	var task = sender.SendEmailAsync("gonderilecekMailAdresi@gmail.com", "asansörümüzde sýkýntý var", "1.kattan asansöre bindim, kaldým");


            //	//toplu mail atmak istersek bu durumda aþaðýdaki kodu çalýþtýrmamýz.
            //	//MailSender sender = new MailSender(_applicationDbContext);
            //	//foreach (var item in MailAdresleriToplulugu)
            //	//{					
            //	//	var task = sender.SendEmailAsync(item, "asansörümüzde sýkýntý var", "1.kattan asansöre bindim, kaldým");
            //	//}
            //}
            //catch (Exception ex)
              //  {
                //	string sonuc = ex.Message;
             //   }




                HelperClass helperClass = new HelperClass(_applicationDbContext, _eticaretVeriTabaniContext);
           
            var liste = helperClass.TurDatalariniEkle();
            
            if (_eticaretVeriTabaniContext.Turss.Count() == 0)
           {
                foreach (var item in liste)
                {
                    _eticaretVeriTabaniContext.Turss.Add(item);
                }
                _eticaretVeriTabaniContext.SaveChanges();
            }

            var turler = _eticaretVeriTabaniContext.Turss.ToList(); 

            AlbumHelper albumHelper = new AlbumHelper(_eticaretVeriTabaniContext);
            albumHelper.AddAlbums();
            return View(turler);
        }
        [HttpPost]
        public IActionResult Index(int id)
        {
            return View("Index", "Home");

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
         {
         return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
         }

        public IActionResult Hakkimizda()
        {

            var cart = shoppingCart.GetCart(this.HttpContext);
            HttpContext.Session.SetString("adet", cart.GetCount().ToString());
            ViewData["SepetAdet"] = HttpContext.Session.GetString("adet");


            return View();
        }

        public IActionResult KategoriyeGoreAlbumler(int id) //parametre olarak gelen deðer turid bilgisi.
        {
            string aaa = HttpContext.Session.GetString("siteAdi");
            //albumlere git, yukarýdaki id ile eþleþen albumleri bul, listele ve sayfada göster.
            List<Album> albumler = _eticaretVeriTabaniContext.Albums.Include(satir => satir.Tur).Where(satir => satir.TurId == id).ToList();
          
            var cart = shoppingCart.GetCart(this.HttpContext);
             HttpContext.Session.SetString("adet", cart.GetCount().ToString());
             ViewData["SepetAdet"] = HttpContext.Session.GetString("adet");

            return View(albumler); //albumler datasý ile view'ý açmalý
        }

        public IActionResult Details(int id) //parametre olarak gelen deðer albumid bilgisi.
        {
            //albumlere git, yukarýdaki id ile eþleþen albumleri bul, listele ve sayfada göster.
            Album secilenAlbum = _eticaretVeriTabaniContext.Albums.Where(satir => satir.AlbumId == id).FirstOrDefault();

            var cart = shoppingCart.GetCart(this.HttpContext);
            HttpContext.Session.SetString("adet", cart.GetCount().ToString());
            ViewData["SepetAdet"] = HttpContext.Session.GetString("adet");

            return View(secilenAlbum);
        }
       
        public IActionResult CreateOrder(int id)
        {
            //cart.CreateOrder()
            return View();
        }

        ////hangi ürün,kaç tane, OrderId bilgisi GUid ile oluþturulup eklensin.
        //public IActionResult CreateOrder(int AlbumId, int Quantity,string fiyat)
        //{
        ////	//OrderDetails tablosuna kayýt eklenecek.
        //	OrderDetail orderDetail = new OrderDetail();
        //	orderDetail.OrderId = 1;
        //	orderDetail.Quantity = Quantity;
        //	orderDetail.AlbumId = AlbumId;
        //	orderDetail.UnitPrice =Convert.ToDecimal(fiyat);

        //	_eticaretVeriTabaniContext.OrderDetails.Add(orderDetail);
        //	_eticaretVeriTabaniContext.SaveChanges();

        //	return Json("ok");

        //}

    }
}
