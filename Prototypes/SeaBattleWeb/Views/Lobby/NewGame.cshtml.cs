using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class NewGameModel : PageModel {
    [BindProperty]
    public string? Visibility { get; set; }
    public Task<IActionResult> OnGetAsync() {
        return Task.FromResult<IActionResult>(Page());
    }
}