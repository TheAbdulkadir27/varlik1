using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels;

namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly VarlikZimmetEnginContext _db;
        private readonly ILogger<AccountController> _logger; // Loglama işlemleri için kullanılır.

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, VarlikZimmetEnginContext db, ILogger<AccountController> logger)
        {
            _userManager = userManager; // Kullanıcı işlemleri için kullanılır. Kullanıcı oluşturma, silme, güncelleme, rol ekleme, çıkarma gibi işlemler yapılabilir.
            _signInManager = signInManager;
            _db = db;
            _logger = logger;
        }

        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                _logger.LogInformation("Login denemesi başladı: {KullaniciAdi}", model.Login);
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Model durumu geçersiz");
                    return View(model);
                }

                // Kullanıcıyı bul
                if (model.Login == null)
                {
                    ModelState.AddModelError(string.Empty, "Email veya kullanıcı adı gereklidir.");
                    return View(model);
                }
                IdentityUser? user = await _userManager.FindByEmailAsync(model.Login);
                if (user == null)
                {
                    user = await _userManager.FindByNameAsync(model.Login);
                }

                if (user == null)
                {
                    _logger.LogWarning("Kullanıcı bulunamadı: {KullaniciAdi}", model.Login);
                    ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya şifre.");
                    return View(model);
                }

                // Direkt SignInManager ile giriş deneyelim
                var result = await _signInManager.PasswordSignInAsync(user.UserName!, model.Sifre!, model.BeniHatirla, false);


                _logger.LogInformation("Giriş denemesi sonucu: {Succeeded}, {IsLockedOut}, {IsNotAllowed}, {RequiresTwoFactor}",
                    result.Succeeded, result.IsLockedOut, result.IsNotAllowed, result.RequiresTwoFactor);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Kullanıcı başarıyla giriş yaptı: {KullaniciAdi}", model.Login);

                    // Session'a kullanıcı bilgilerini ekle
                    HttpContext.Session.SetString("UserName", user.Email ?? string.Empty);

                    // Başarılı mesajı ekle
                    TempData["Success"] = "Başarıyla giriş yapıldı!";

                    // Ana sayfaya yönlendir
                    return RedirectToAction("Index", "Home");
                }

                // Başarısız giriş
                _logger.LogWarning("Giriş başarısız: {KullaniciAdi}", model.Login);
                ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya şifre.");
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login işlemi sırasında hata oluştu. Kullanıcı: {KullaniciAdi}, Hata: {Message}",
                    model.Login, ex.Message);
                ModelState.AddModelError(string.Empty, "Bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
                return View(model);
            }
        }

        // GET: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.KullaniciAdi, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Sifre);

                if (result.Succeeded)
                {
                    // İlk kullanıcıyı Admin yap, diğerlerini User
                    if (_userManager.Users.Count() == 1)
                    {
                        await _userManager.AddToRoleAsync(user, UserRoles.Admin);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, UserRoles.User);
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("Kullanıcı çıkış yaptı.");
            return RedirectToAction("Login", "Account");
        }

        // GET: /Account/AccessDenied
        [HttpGet]
        [AllowAnonymous] //AllowAnonymous: Yetkilendirme yapmadan erişilebilir demek.AccessDenied, Giriş yapılamaz kısmı burada bu yuzden yetkilendirme yapmadan erişilebilir olmalı.
        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult ErrorPage()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                // Kullanıcının cookie'sini güncelle
                await _signInManager.RefreshSignInAsync(user);
                TempData["SuccessMessage"] = "Şifre başarıyla değiştirildi.";
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }
    }
}
//Todo: IMusteriRepository.cs
//     ├── IRolRepository.cs

