using Microsoft.AspNetCore.Mvc;

namespace CardActionsService;

public class CardActionsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}