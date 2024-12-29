using System.Diagnostics;
using Login_with_session.Models;
using Microsoft.AspNetCore.Mvc;

namespace Login_with_session.Controllers
{
    public class HomeController : Controller
    {
        private readonly AuthenticationDbContext context;

        public HomeController(AuthenticationDbContext context)
        {
            this.context = context;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Login()
        {

            if (HttpContext.Session.GetString("SessionKey") != null)
            {
                return RedirectToAction("Dashboard");

            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User MyUser)
        {
            var userdata =  context.Users.Where(x => x.Email == MyUser.Email && x.Password == MyUser.Password).FirstOrDefault();
            if (userdata != null)
            {
                HttpContext.Session.SetString("SessionKey", MyUser.Email); //here Sessionkey works as Session Variable for  data of MyUser.Email
                return RedirectToAction("Dashboard", "Home");
            }
            else
            {
                ViewBag.Message = "Login Failed";


            }
            return View();

        }

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("SessionKey") != null)
            {
                ViewBag.SessionMsg = HttpContext.Session.GetString("SessionKey").ToString();

            }
            else
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        public IActionResult Logout()

        {
            if (HttpContext.Session.GetString("SessionKey") != null)
            {
                HttpContext.Session.Remove("SessionKey");
                
            }
            return RedirectToAction("Login");
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User MyUser)
        {
            if(ModelState.IsValid)
            {
                await context.Users.AddAsync(MyUser);
                await context.SaveChangesAsync();
                TempData["Success"] = "Registered Successfully";
                return RedirectToAction("Login");
            }
            return View();
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
    }
}
