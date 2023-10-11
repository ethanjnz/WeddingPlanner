using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers;

[SessionCheck]
public class DecisionController : Controller
{
    private readonly ILogger<DecisionController> _logger;
    public MyContext _context;

    public DecisionController(ILogger<DecisionController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    // DELETE ONLY YOUR WEDDING
    [HttpPost("weddings/{weddingId}/delete")]
    public RedirectToActionResult DeleteWedding(int weddingId)
    {
        Wedding? toBeDeleted = _context.Weddings.SingleOrDefault(t => t.WeddingId == weddingId);
        if (toBeDeleted != null)
        {
            _context.Remove(toBeDeleted);
            _context.SaveChanges();
        }
        return RedirectToAction("Weddings", "Wedding");
    }

    // DECIDE ON A WEDDING
    [HttpPost("weddings/{id}/decision")]
    public RedirectToActionResult ToggleDecision(int id)
    {
        int UserId = (int)HttpContext.Session.GetInt32("UserId");
        Decision? existingDecision = _context.Decisions.FirstOrDefault(d => d.WeddingId == id && UserId == d.UserId);
        if (existingDecision == null)
        {
            Decision newDecison = new() { UserId = UserId, WeddingId = id };
            _context.Add(newDecison);
        }
        else
        {
            _context.Remove(existingDecision);
        }
        _context.SaveChanges();
        return RedirectToAction("Weddings", "Wedding");
    }









    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


