using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
            return Page();
        }

        [BindProperty]
        public Car Car { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Cars == null || Car == null)
            {
                return Page();
            }

            Car.UserId = HttpContext.Session.GetInt32("UserId") ?? 0;
            Car.DealerId = 1;
            _context.Cars.Add(Car);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
