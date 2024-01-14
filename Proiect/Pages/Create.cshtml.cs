using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proiect.Models;

namespace Proiect.Pages
{
    public class CreateModel : PageModel
    {
        private readonly Proiect.Data.CarContext _context;

        public CreateModel(Proiect.Data.CarContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Dealers =  Dealers = _context.Dealers.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name
            }).ToList();
            return Page();
        }

        [BindProperty]
        public Car Car { get; set; } = default!;
        public List<SelectListItem> Dealers { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Cars == null || Car == null)
            {
                return Page();
            }

            Car.UserId = HttpContext.Session.GetInt32("UserId") ?? 0;

            _context.Cars.Add(Car);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
