using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SeaBattleWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace SeaBattleWeb.Controllers;

public class LobbyController : Controller
{
    private readonly ILogger<LobbyController> _logger;


    public async Task<IActionResult> LobbyList()
        {
            List<Lobby> lobbyList = new List<Lobby>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5282/Lobby"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    lobbyList = JsonConvert.DeserializeObject<List<Lobby>>(apiResponse);
                }
            }
            return View(lobbyList);
        }

    
    public async Task<IActionResult> NewGame()
        {
            return View();
        }


    public LobbyController(ILogger<LobbyController> logger)
    {
        _logger = logger;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
