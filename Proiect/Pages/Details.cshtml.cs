using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Proiect.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly Proiect.Data.CarContext _context;

        public DetailsModel(Proiect.Data.CarContext context)
        {
            _context = context;
        }

        public CarViewModel Car { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            else
            {
                Car = await _context.Cars
            .Where(x => x.Id == id)
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
                  }).FirstAsync();
            }
            return Page();
        }
    }
}
