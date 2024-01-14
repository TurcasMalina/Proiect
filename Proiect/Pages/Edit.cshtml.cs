using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect.Models;

namespace Proiect.Pages
{
    public class EditModel : PageModel
    {
        private readonly Proiect.Data.CarContext _context;

        public EditModel(Proiect.Data.CarContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Car Car { get; set; } = default!;
        public List<SelectListItem> Dealers { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Dealers = _context.Dealers.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name
            }).ToList();

            var userId = HttpContext.Session.GetInt32("UserId");
            if (id == null || _context.Cars == null || userId == null)
            {
                return NotFound();
            }

            var car =  await _context.Cars.FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
            if (car == null)
            {
                return NotFound();
            }
            Car = car;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!ModelState.IsValid || userId == null)
            {
                return Page();
            }
            Car.UserId = userId ?? 0;

            _context.Attach(Car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(Car.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CarExists(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            return (_context.Cars?.Any(e => e.Id == id && e.UserId == userId)).GetValueOrDefault();
        }
    }
}
