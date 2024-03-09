using Lab22.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab22.Controllers;
public class Task6Controller : Controller
{
    public IActionResult Index()
    {
        return View(new Task6Model());
    }

    [HttpPost]
    public ActionResult Index(Task6Model model)
    {
        model.B = 0.1 * Math.Pow(model.X, 5) + Math.Pow(Math.E, -(0.1 * model.X))
            - Math.Sqrt(Math.Abs(model.X) * Math.Abs(model.X + model.Y));
        return View(model);
    }
}
