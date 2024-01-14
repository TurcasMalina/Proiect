using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proiect.Models;

public class LoginModel : PageModel
{
    private readonly Proiect.Data.CarContext _context;

    public LoginModel(Proiect.Data.CarContext context)
    {
        _context = context;
    }
    [BindProperty]
    public string Username { get; set; }

    [BindProperty]
    public string Password { get; set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        // Validate the user credentials here
        // For now, let's assume that we have a method ValidateUser that checks the user's credentials
        var user = ValidateUser(Username, Password);
        if (user != null)
        {
            // Set the UserId in a session or cookie
            HttpContext.Session.SetInt32("UserId", user.Id);
            return RedirectToPage(nameof(Index));
        }
        // If user validation fails
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return Page();
    }

    private User ValidateUser(string username, string password)
    {
        var user = _context.Users.Where(x => x.UserName == username && x.Password == password).FirstOrDefault();
        // This should check against a user store (e.g., database)
        // For demonstration, we're using hardcoded values
        if (user != null)
        {
            return new Proiect.Models.User { Id = user.Id, UserName = username, Password = password };
        }
        return null;
    }
}
