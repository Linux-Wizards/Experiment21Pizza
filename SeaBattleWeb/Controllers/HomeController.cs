using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SeaBattleWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace SeaBattleWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    //[Authorize]
    public async Task<IActionResult> PizzaTest()
        {
            //return View();
            List<Pizza> pizzaList = new List<Pizza>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5282/Pizza"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    pizzaList = JsonConvert.DeserializeObject<List<Pizza>>(apiResponse);
                }
            }
            return View(pizzaList);
        }

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
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
    
    [Authorize]
    public IActionResult Claims()
    {
        return View();
    }
}
