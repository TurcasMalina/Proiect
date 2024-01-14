using Proiect.Models;

namespace Proiect.Services
{
    public interface IUserService
    {
        bool Login(User user);
        void Register(User user);
    }
}