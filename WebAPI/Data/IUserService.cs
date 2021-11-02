using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace WebAPI.Data
{
    public interface IUserService
    {
        Task<User> ValidateUser(string username, string password);
        Task<User> RegisterUser(User user);

    }
}