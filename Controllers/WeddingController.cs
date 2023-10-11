using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers;

[SessionCheck]
public class WeddingController : Controller
{
    private readonly ILogger<WeddingController> _logger;
    public MyContext _context;

    public WeddingController(ILogger<WeddingController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("wedding")]
    public IActionResult Weddings()
    {
        List<Wedding> AllWeddings = _context.Weddings.Include(t => t.Creator).Include(t => t.UserDecision).ToList();
        return View("Weddings", AllWeddings);
    }

    //GET ALL WEDDINGS
    [HttpGet("weddings/new")]
    public IActionResult WeddingForm()
    {

        return View("WeddingForm");
    }


    // PROCESS WEDDING FORM
    [HttpPost("weddings/create")]
    public IActionResult CreateWedding(Wedding newWedding)
    {
        if (!ModelState.IsValid)
        {
            Console.WriteLine("Not valid");
            return View("WeddingForm");
        }
        _context.Add(newWedding);
        _context.SaveChanges();

        return RedirectToAction("OneWedding", new { weddingid = newWedding.WeddingId });
    }

    // GET ONE WEDDING
    [HttpGet("weddings/{weddingId}")]
    public IActionResult OneWedding(int weddingId)
    {
        Wedding? OneWedding = _context.Weddings.Include(t => t.UserDecision).ThenInclude(t => t.DecisionMaker).FirstOrDefault(t => t.WeddingId == weddingId);
        if (OneWedding == null)
        {
            return RedirectToAction("Weddings");
        }
        return View("Details", OneWedding);
    }






    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {

        int? userId = context.HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            context.Result = new RedirectToActionResult("Index", "User", null);
        }
    }
}
