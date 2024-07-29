using Eticaret12.Data;
using Eticaret12.Helper;
using Eticaret12.Models;
using Eticaret12.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Eticaret12.Controllers
{

    public class UserController : Controller
    {   
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly EticaretVeritabaniContext eticaretVeritabaniContext;
        public UserController( ApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager, EticaretVeritabaniContext ctx)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            eticaretVeritabaniContext = ctx;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RolEkle()
        {

            return View();
        }
        [HttpPost]
        public IActionResult RolEkle(IdentityRole rolex)
        {
            HelperClass hc = new HelperClass(_applicationDbContext, eticaretVeritabaniContext);
            int idDeğeri = hc.sonIdAl();
            IdentityRole role = new IdentityRole();
            role.Id = idDeğeri.ToString();
            role.Name = rolex.Name;
            _applicationDbContext.Add(role);
            _applicationDbContext.SaveChanges();
           return RedirectToAction("Roller");
        }
        public IActionResult KullaniciRolAta()
        {
           List<IdentityUser> kullanicilar= _applicationDbContext.Users.ToList();
            List<IdentityRole> roller = _applicationDbContext.Roles.ToList();
            ViewBag.kullanicilar= new SelectList(kullanicilar, "Id", "UserName");
            ViewBag.roller = new SelectList(roller, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult KullaniciRolAta(UserRol userRol)
        {
            IdentityUserRoley ıdentityUserRole=new IdentityUserRoley();
             ıdentityUserRole.Id= Guid.NewGuid().ToString();
            ıdentityUserRole.UserId = userRol.UserId;
            ıdentityUserRole.RoleId= userRol.RolId.ToString();
           
            _applicationDbContext.UserRoles.Add(ıdentityUserRole);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Kullanicilar");
        }
       
            public IActionResult Roller()
        {
           List<IdentityRole>  roller= _applicationDbContext.Roles.ToList();
            return View(roller);
        }
        public IActionResult Kullanicilar()
        {
            // select u.UserName, r.Name from AspNetUserRoles ur join AspNetUsers u on ur.UserId = u.Id join AspNetRoles r on r.Id = ur.RoleId
            var model = from t in _applicationDbContext.Users
                        join x in _applicationDbContext.UserRoles
                        on t.Id equals x.UserId into userRoles
                        from ur in userRoles.DefaultIfEmpty()
                        join z in _applicationDbContext.Roles
                        on ur.RoleId equals z.Id into roles
                        from r in roles.DefaultIfEmpty()
                        select new UserRole
                        {
                           // Id = Guid.NewGuid().ToString(),
                           RolName = r!= null? r.Name: null,
                            UserName= t.UserName 
                        };
            return View(model); 
        }
        public IActionResult KullaniciEkle() 
        { 
            return View(); 
        }
        [HttpPost]
        public async Task <IActionResult> KullaniciEkle(IdentityUser user)
        {
           // IdentityUser newUser = new IdentityUser();
           // user.Id= Guid.NewGuid().ToString();
           // newUser.Email= user.Email;
           // newUser.UserName= user.Email;
          //  newUser.NormalizedUserName = user.Email.ToUpper().Replace("i", "ı").Replace("Ç", "C");
          //  newUser.NormalizedEmail= user.Email.ToUpper().Replace("i", "ı").Replace("Ç", "C");
          //  newUser.EmailConfirmed=false;
         //   newUser.PhoneNumberConfirmed=false;
         //   newUser.TwoFactorEnabled=false;
          //  newUser.LockoutEnabled=true;
          //  newUser.AccessFailedCount=0;
           // _applicationDbContext.Add(newUser);
         //   _applicationDbContext.SaveChanges();

            user.UserName = user.Email;
            await _userManager.CreateAsync(user, user.PasswordHash);

            return RedirectToAction("Kullanicilar");
        }
        public IActionResult KullaniciOnay()
        {
           var onaylanacaklar = _applicationDbContext.Users./*Where(satir=>satir.EmailConfirmed==false)*/ ToList();
            return View(onaylanacaklar);
        }

        public IActionResult Onay(string Username)
        {
            try 
            {
                IdentityUser dbdenGelen = _applicationDbContext.Users.Where(satir => satir.UserName == Username).FirstOrDefault();
                dbdenGelen.EmailConfirmed = true;
                _applicationDbContext.Update(dbdenGelen);
                _applicationDbContext.SaveChanges();
                return Json("ok");

            }
            catch 
            {
                return Json("hata");
            }
            
            
        }
        public async Task <IActionResult> RandomSifreOlusturma(string Username)
        {


          IdentityUser dbdenGelen = _applicationDbContext.Users.Where(satir => satir.UserName == Username).FirstOrDefault();
            if(dbdenGelen.PasswordHash!= null)
            {
                return Json("password var ");
            }
            else
            {
                HelperClass hc= new HelperClass(_applicationDbContext, eticaretVeritabaniContext);
                string sifre = hc.RandomSifreOlustur();

				//MailSender sender= new MailSender(_applicationDbContext);
				// sender.SendEmailAsync (sifre, sifre, sifre);
				var result = await UpdateUser (dbdenGelen, sifre);

				////1.mail gönderme+
				MailSender sender = new MailSender(_applicationDbContext);

				sender.SendEmailAsync("32@gmail.com", "Password Recovery", "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <meta charset=\"utf-8\" />\r\n   " +
                    " <title></title>\r\n</head>\r\n<body>\r\n  <p>" + "Şifreniz Güncellendi. Yeni Şifreniz: " + sifre + 
                    "</p> <img src=\"https://www.shutterstock.com/image-vector/password-theft-cracking-cybercrime-hacker-260nw-2210091473.jpg\" style=\"width:200px;height:200px;\" />\r\n  " +
                    "  <h1>Şifre Onaylama Ekranı</h1>\r\n    <p>Şifrenizi onaylamak için <a href=\"https://www.youtube.com/\" target=\"_blank\">tıklayınız</a></p>\r\n\r\n</body>\r\n</html>");

				////2.userPasswordHistories tablosuna bu şifreyi kaydedelim.
				UserPasswordHistory passwordHistory= new UserPasswordHistory();
				passwordHistory.CreatedDate = DateTime.Now;
				passwordHistory.ExpireDate = passwordHistory.CreatedDate.AddMinutes(1); //1 DK İÇİNDE KENDİSİNE İLETİLEN ŞİFREYİ GİRMELİDİR.
				passwordHistory.IsActive = false;
				passwordHistory.Mail = dbdenGelen.Email;
				passwordHistory.Password = sifre;
               _applicationDbContext.UserPasswordHistories.Add(passwordHistory);
				_applicationDbContext.SaveChanges();
				
				return Json("ok");
            }
        }
		public IActionResult SifreOnay()
		{
			//HttpContext.Session.SetString("username", username);
			return View();
		}

		//[HttpPost]
		//public IActionResult SifreOnay(/*string usernamex, string password*/)
		//{
			//string username = HttpContext.Session.GetString("username");
			////-SİFRENİN İPTAL OLMA ZAMANI OLMALI,++
			////-Kullanıcının mail ile iletilen şifreyi girmesi lazım.  eğer doogru girerse, hala zaman geçmemişse bu durumda o kullanıcının üyeliği aktif duruma getirilir.
			//var userBilgileri = _applicationDbContext.UserPasswordHistories.Where(satir => satir.Mail == username).OrderByDescending(satir => satir.CreatedDate).FirstOrDefault();

			//if (password == userBilgileri.Password && userBilgileri.ExpireDate >= DateTime.Now)
			//{
			//	userBilgileri.IsActive = true;
			//}
			//_applicationDbContext.SaveChanges();

			//return View();
		//}
		public IActionResult Delete(int id) 
        {
            var silinecekYetkiDetay =_applicationDbContext.UserRolesY.First(satir => satir.Id == id.ToString());
            return View(silinecekYetkiDetay);

        }
        [HttpPost]
        public IActionResult Delete(int id,IdentityUserRoley ıdentityUserRoley)
        {
            var silinecekYetkiDetay = _applicationDbContext.UserRolesY.First(satir => satir.Id == id.ToString());
            return View(silinecekYetkiDetay);

        }

        public IActionResult TopluGuncelleme()
		{
			//alttaki kod tablodaki login olan kişide dahil olmak üzere, herkesi 3 rolüne güncelledi. (login olan kişi hariç diğer kullanıcıların rolünü değiştir demek lazım.

			string loginOlanKisiId = _applicationDbContext.Users.Where(satir => satir.UserName == User.Identity.Name).FirstOrDefault().Id;

			_applicationDbContext.Database.ExecuteSqlRaw("update AspNetUserRoles Set RoleId='3' where UserId!='" + loginOlanKisiId + "'");

			return Json("ok");
		}

		public async Task<IActionResult> UpdateUser(IdentityUser user, string sifre)
		{

			//1.usermanager üzerindeng git o userI bul.
			var userx = await _userManager.FindByIdAsync(user.Id);

			if (userx == null)
			{
				return NotFound("kullanıcı bulunamadı");
			}

			//yeni ayarlayıp 


			var yeniPasswordHashli = _userManager.PasswordHasher.HashPassword(user, sifre);
			userx.PasswordHash = yeniPasswordHashli;

			var result = await _userManager.UpdateAsync(userx);

			if (!result.Succeeded)//Succeeded değilse ! : olumsuzluk anlamını taşır
			{
				return BadRequest("şifre güncellenemedi");
			}

			return View();

           
		}

        public IActionResult SifreOnay (string username)
        {
            HttpContext.Session.SetString("username", username);
            return View();
	}

        [HttpPost]
        public IActionResult SifreOnay(string usernamex, string password)
        {
            string username = HttpContext.Session.GetString("username");
            //-SİFRENİN İPTAL OLMA ZAMANI OLMALI,++
            //-Kullanıcının mail ile iletilen şifreyi girmesi lazım.  eğer doogru girerse, hala zaman geçmemişse bu durumda o kullanıcının üyeliği aktif duruma getirilir.
            var userBilgileri = _applicationDbContext.UserPasswordHistories.Where(satir => satir.Mail == username).OrderByDescending(satir => satir.CreatedDate).FirstOrDefault();

            if (password == userBilgileri.Password && userBilgileri.ExpireDate >= DateTime.Now)
            {
                userBilgileri.IsActive = true;
            }
            _applicationDbContext.SaveChanges();

            return View();
        }

        public IActionResult OdemeYap(string code)
        {
            var dbdenGelen = eticaretVeritabaniContext.UserPaymentCodeHistories.Where(satir => satir.UserName == User.Identity.Name).OrderByDescending(satir => satir.SendDate).FirstOrDefault();

            if (dbdenGelen.Code == code && DateTime.Now <= dbdenGelen.ExpireDate)
            {
                dbdenGelen.IsPaymentReceived = true; //zamanında ve dogru girilmişse dbde aktif yaptık
                eticaretVeritabaniContext.Update(dbdenGelen);
                eticaretVeritabaniContext.SaveChanges();
                return Json("ok");
            }
            else
            {
                return Json("hata");
            }
        }
        public IActionResult PaymentSuccess()
        {
            //random yapısını kullanarak, sayısal değerlerden oluşan bir kod üretelim.
            //bu üretilen sipariş numaralarını tabloya yazalım. 
            //yeni uretilmiş olan kod daha önce üretilmiş ve tabloda varsa(tablo oluşacak)  farklı bir değer olması için tekrar üretelim.
            //pttkargo tarafına bu değer iletilir. (kargo bilgilerini kaydettiğimiz tabloya da eklemek gerekir.)
            //bu değer kullanıcıya mail-sms olarak gönderilir.

            HelperClass helperClass = new HelperClass(_applicationDbContext, eticaretVeritabaniContext);
            string siparisNo = helperClass.RandomSiparisNoOlustur();
            if (helperClass.SiparisNoVarmi(siparisNo) == false)
            {
               //siparisno yok. 

            //    //daha önce böye bir sipariş no üretilmemiş
                OrderNumberHistory orderNumberHistory = new OrderNumberHistory();
               orderNumberHistory.OrderNumberCode = siparisNo;

                eticaretVeritabaniContext.OrderNumberHistories.Add(orderNumberHistory);
               eticaretVeritabaniContext.SaveChanges();
            }
            else
            {
                //varsa yeni kod üret.
               siparisNo = helperClass.RandomSiparisNoOlustur();
            }

            ViewData["SiparisNo"] = siparisNo;






            return View();
        }
    }
}
   





