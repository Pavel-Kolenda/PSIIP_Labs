using Microsoft.AspNetCore.Mvc;

namespace Lab21.Controllers;
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
