using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using proctos.Models;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Security.Claims;
using System.Net;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
namespace proctos.Controllers
{
    public class HomeController : Controller
    {
		private readonly ILogger<HomeController> _logger; 
		private readonly BrandsHopContext _context; 
		
        private static List<cloth> cloths = new List<cloth>
        {
            new cloth { Id = 1, Name = "Nike", Description = "Кроссовки Wmns Dunk High", Price = 19890, Image = "" },
            new cloth { Id = 2, Name = "Stone Island Junior", Description = "Детская толстовка Season 80 Crew Neck", Price = 22190, Image = "" },
            new cloth { Id = 3, Name = "Vance", Description = "Мужская толстовка Arched Pullover Hoodie", Price = 8790, Image = "" },
            new cloth { Id = 4, Name = "RIPNDIP", Description = "Панама Lord Nermal", Price = 5190, Image = "" }
        };
		public HomeController(ILogger<HomeController> logger, BrandsHopContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet]
        public IActionResult reg()
        {
            return View();
        }

        public IActionResult Login()
		{
			return View();
		}

        [HttpGet]
        // Главная страница
        public IActionResult Aut()
        {
            return View();
        }

        [HttpGet]
		// Главная страница
		public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Aut(string email, string password)
        {
            string hashedPassword = HashPassword(password);

            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.PasswordHash == hashedPassword);
            if (user != null)
            {
                var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.RoleId == 1 ? "Customer" : "OtherRole")
        };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                return RedirectToAction("Catalog", "Home");
            }

            ViewBag.ErrorMessage = "Неверные учетные данные";
            return View();
        }



        [HttpPost]
		public async Task<IActionResult> reg(string fullName, string email, string phone, string password)
		{
			if (_context.Users.Any(u => u.Email == email))
			{
				ViewBag.ErrorMessage = "Пользователь с таким email уже существует";
				return View();
			}

			string hashedPassword = HashPassword(password);

			var user = new User
			{
				FullName = fullName,
				Email = email,
				Phone = phone,
				PasswordHash = hashedPassword,
				DateJoined = DateTime.Now,
				RoleId = 1
			};

			_context.Users.Add(user);
			await _context.SaveChangesAsync();
			return RedirectToAction("Aut");
		}


		public IActionResult Privacy()
		{
			return View();
		}

		private string HashPassword(string password)
		{
			using (var sha256 = SHA256.Create())
			{
				var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
				return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
			}
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
		
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		// Страница "Каталог"
		public IActionResult catalog()
        {
            return View(cloths);  
        }

        // Страница "Заказ"
        public IActionResult order()
        {
            return View(cloths);
        }

        public IActionResult AddTofavorites(int id)
        {
            // Находим автомобиль по ID
            var cloth = cloths.FirstOrDefault(c => c.Id == id);
            if (cloth != null)
            {
                cloth.IsFavorite = true; // Устанавливаем флаг избранного
            }

            return RedirectToAction("Catalog"); // Возвращаемся в каталог
        }

        // Страница "Избранное"
        public IActionResult RemoveFromfavorites(int id)
        {
            var cloth = cloths.FirstOrDefault(c => c.Id == id);
            if (cloth != null)
            {
                cloth.IsFavorite = false; // Снимаем флаг избранного
            }

            return RedirectToAction("Favorites"); // Возвращаемся на страницу избранного
        }

        public IActionResult favorites()
        {
            List<cloth> favoriteCars = cloths.Where(c => c.IsFavorite).ToList(); 
            return View(favoriteCars); 
        }

        public IActionResult contacts()
        {
            return View();
        }

        // Страница "О нас"
        public IActionResult about()
        {
            return View();
        }
    }
}
