using System.Threading.Tasks;
using Models;

namespace WebAPI.Data
{
    public interface IUserService
    {
        Task<User> ValidateLogin(string username, string password); 
        Task RegisterUser(string username, string password);
        
    }
}