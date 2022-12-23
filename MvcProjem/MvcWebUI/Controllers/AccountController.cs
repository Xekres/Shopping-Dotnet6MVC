using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcWebUI.Entities;
using MvcWebUI.Models;

namespace MvcWebUI.Controllers
{
    public class AccountController : Controller
    {
        //Kullanıcı verilerini yönetmek için oluşturdum
        //Kullanıcı hesaplarını yönetmek için:
        private UserManager<CustomIdentityUser> _userManager;
        //Rolleri yönetmek için:
        private RoleManager<CustomIdentityRole> _roleManager;
        //Sisteme giriş-çıkış yönetimi için:
        private SignInManager<CustomIdentityUser> _signInManager;

        public AccountController(UserManager<CustomIdentityUser> userManager, RoleManager<CustomIdentityRole> roleManager, SignInManager<CustomIdentityUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            //Yukarıda ki View de bilgilerini doldurdugunda kayıt olmaya çalısacak:
            //Eğer model Valid ise CustomIdentityUser ı kullanarak bilgileri set edeceğim.
            //Tabi sistmde Admin diye bir rol olmaması lazım.
            if (ModelState.IsValid)
            {
                CustomIdentityUser user = new CustomIdentityUser
                {
                    UserName = registerViewModel.UserName,
                    Email = registerViewModel.Email,
                };
                //Kullanıcı oluşturma sonucunda IdentityResult döner.
                //Eğer yapılan işlem başarılıysa(Default olarak Admin diye eklicem ben)
                IdentityResult result = _userManager.CreateAsync(user, registerViewModel.Password).Result;
                if (result.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync("Admin").Result)
                    {
                        CustomIdentityRole role = new CustomIdentityRole
                        {
                            Name = "Admin"
                        };
                        //Eğer result başarılı olmazsa modelState e rolü döndüremedim diye hata döndürelim
                        IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
                        if (!roleResult.Succeeded)
                        {
                            ModelState.AddModelError("", "We Can't Add Role");
                            return View(registerViewModel);
                        }
                    }
                    //Eğer Role manager mevcutsa yenisini olusturmaya çalıstıracak.
                    //Kullanıcıya role ekleyelim
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                    //Eklendikten sonra login e yollayalım ve giriş yapsın
                    return RedirectToAction("Login", "Account");
                }
            }
            return View(registerViewModel);
        } 

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                //Login gerçekleşmezse hesabı kitleyelim mi kitlemeyelim mi ? FALSE=
                var result = _signInManager.PasswordSignInAsync(loginViewModel.UserName, 
                    loginViewModel.Password,
                    loginViewModel.RememberMe,
                    false).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Admin");
                }
                ModelState.AddModelError("", "Invalid Login");
            }
            return View(loginViewModel);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            _signInManager.SignOutAsync().Wait();
            return RedirectToAction("Login");
        }
    }
}
