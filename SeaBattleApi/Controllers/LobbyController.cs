using SeaBattleApi.Models;
using SeaBattleApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace SeaBattleApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LobbyController : ControllerBase
{
    public LobbyController()
    {
    }

    // GET all action
    [HttpGet]
    public ActionResult<List<Lobby>> GetAll() =>
        LobbyService.GetAll();

    // GET by Id action
    [HttpGet("{id}")]
    public ActionResult<Lobby> Get(int id)
    {
        var Lobby = LobbyService.Get(id);

        if (Lobby == null)
            return NotFound();

        return Lobby;
    }

    public class CreateLobbyArgs
    {
        public string Player { get; set; }
        public bool IsPrivate { get; set; }
    }
    // POST action
    [HttpPost]
    public IActionResult Create(CreateLobbyArgs args)
    {
        Lobby lobby = new Lobby(args.Player, args.IsPrivate);
        LobbyService.Add(lobby);
        return CreatedAtAction(nameof(Get), new { id = lobby.Id }, lobby);
    }

    // PUT action
    [HttpPut("{id}")]
    public IActionResult Update(int id, Lobby Lobby)
    {
        if (id != Lobby.Id)
            return BadRequest();

        var existingLobby = LobbyService.Get(id);
        if (existingLobby is null)
            return NotFound();

        LobbyService.Update(Lobby);

        return NoContent();
    }

    // DELETE action
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var Lobby = LobbyService.Get(id);

        if (Lobby is null)
            return NotFound();

        LobbyService.Delete(id);

        return NoContent();
    }
}