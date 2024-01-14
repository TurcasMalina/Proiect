using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Proiect.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Proiect.Data.CarContext _context;

        public IndexModel(Proiect.Data.CarContext context)
        {
            _context = context;
        }

        public IList<CarViewModel> Car { get; set; } = default!;

        public async Task OnGetAsync()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (_context.Cars != null)
            {
                Car = await _context.Cars
            .Where(x => x.UserId == userId)
            .Join(_context.Dealers,
                  car => car.DealerId,
                  dealer => dealer.Id,
                  (car, dealer) => new CarViewModel
                  {
                      Manufacturer = car.Manufacturer,
                      Model = car.Model,
                      Id = car.Id,
                      NumberOfKilometers = car.NumberOfKilometers,
                      Year = car.Year,
                      DealerName = dealer.Name
                  })
            .ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Login");
        }

    }

    public class CarViewModel
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string DealerName { get; set; }
        public int Year { get; set; }
        public int NumberOfKilometers { get; set; }
    }
}
