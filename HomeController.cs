// Controllers/HomeController.cs
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    public static List<User> Users = new List<User>
    {
        new User { Username = "user1", Password = "password1" },
        new User { Username = "user2", Password = "password2" }
    };

    public IActionResult Index()
    {
        ViewBag.UserDetails = TempData["Username"] as string;
        return View();
    }

    [HttpPost]
    public IActionResult Index(string password, string username, string action)
    {
        if (action == "Delete")
        {
            password = password.Substring(0, Math.Max(0, password.Length - 1));
        }

        bool isPasswordValid = CheckPassword(password, username);

        if (isPasswordValid)
        {
            TempData["Username"] = username;
            TempData["Password"] = password;
            ViewBag.UserDetails = $"Имя пользователя: {username}";
        }

        return View();
    }

    private bool CheckPassword(string enteredPassword, string enteredUsername)
    {
        var user = Users.FirstOrDefault(u => u.Username == enteredUsername);

        if (user != null)
        {
            return enteredPassword == user.Password;
        }

        return false;
    }
}
