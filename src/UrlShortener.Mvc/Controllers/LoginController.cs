using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Mvc.Controllers {
    public class LoginController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
