using System.Threading.Tasks;
using medievalworldweb.Models;

namespace medievalworldweb.Data
{
    public interface IAuthRepository
    {
         Task<ServiceResponse<int>> Register(string username,string password);
         Task<ServiceResponse<string>> Login(string username,string password);
         Task<bool> UserAlreadyExists(string username);
    }
}