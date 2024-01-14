using Proiect.Data;
using Proiect.Models;

namespace Proiect.Services
{
    public class UserService : IUserService
    {
        private readonly CarContext _context;

        public UserService(CarContext context)
        {
            _context=context;
        }

        public void Register(User user)
        {
            _context.Users.Add(user);

        }

        public bool Login(User user)
        {
            return _context.Users.Any(x => x.UserName == user.UserName && x.Password == user.Password);
        }
    }
}
