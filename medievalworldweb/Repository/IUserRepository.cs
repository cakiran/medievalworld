using medievalworldweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace medievalworldweb.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUser(string userName);
        Task Adduser(User user);
    }

    public class UserRepository : GenericRepository<MedievalWorldDatabaseContext>, IUserRepository
    {
        public async Task Adduser(User user)
        {
            await Add(user);
        }

        public async Task<User> GetUser(string userName)
        {
            var user = await SingleOrDefaultAsync<User, User>(x => x.Username.ToLower().Equals(userName.ToLower()), x => x);
            return user;
        }
    }
}
