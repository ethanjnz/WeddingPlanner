using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    public MyContext _context;

    public UserController(ILogger<UserController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    // REGISTER
    [HttpPost("users/create")]
    public IActionResult CreateUser(User newUser)
    {
        if (!ModelState.IsValid)
        {

            return View("Index");
        }
        PasswordHasher<User> hasher = new();
        newUser.Password = hasher.HashPassword(newUser, newUser.Password);
        _context.Add(newUser);
        _context.SaveChanges();
        HttpContext.Session.SetInt32("UserId", newUser.UserId);
        return RedirectToAction("Weddings", "Wedding");
    }

    // LOGIN
    [HttpPost("users/login")]
    public IActionResult LoginUser(LogUser LogAttempt)
    {
        if (!ModelState.IsValid)
        {
            return View("Index");
        }
        User? dbUser = _context.Users.FirstOrDefault(t => t.Email == LogAttempt.LogEmail);
        if (dbUser == null)
        {
            ModelState.AddModelError("LogPassword", "Invalid Credentials (e)");
            return View("Index");
        }
        PasswordHasher<LogUser> hasher = new();
        PasswordVerificationResult pwCompareResult = hasher.VerifyHashedPassword(LogAttempt, dbUser.Password, LogAttempt.LogPassword);
        if (pwCompareResult == 0)
        {
            ModelState.AddModelError("LogPassword", "Invalid Credentials (p)");
            return View("Index");
        }
        HttpContext.Session.SetInt32("UserId", dbUser.UserId);
        HttpContext.Session.SetString("Username", dbUser.FirstName);

        return RedirectToAction("Weddings", "Wedding");
    }


    [HttpPost("clear")]
    public RedirectToActionResult LogOut()
    {

        HttpContext.Session.Remove("UserId");
        HttpContext.Session.Remove("Username");
        return RedirectToAction("Index");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
