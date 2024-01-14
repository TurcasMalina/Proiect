using Proiect.Data;
using Proiect.Models;

namespace Proiect.Services
{
    public class CarService : ICarService
    {
        private readonly CarContext _context;

        public CarService(CarContext context)
        {
            _context=context;
        }

        public List<Car> GetAllCars()
        {
            return _context.Cars.ToList();
        }
    }
}
