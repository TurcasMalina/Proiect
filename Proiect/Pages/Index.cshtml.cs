using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect.Models;

namespace Proiect.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Proiect.Data.CarContext _context;

        public IndexModel(Proiect.Data.CarContext context)
        {
            _context = context;
        }

        public IList<Car> Car { get; set; } = default!;

        public async Task OnGetAsync()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (_context.Cars != null)
            {
                Car = await _context.Cars.Where(x => x.UserId == userId).ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            // Clear the session
            HttpContext.Session.Clear();

            // Redirect to the login page
            return RedirectToPage("/Login");
        }
    }
}
