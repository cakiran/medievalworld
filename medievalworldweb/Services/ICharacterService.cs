using System.Threading.Tasks;
using medievalworldweb.Dtos.Character;
using medievalworldweb.Models;

namespace medievalworldweb.Services
{
    public interface ICharacterService
    {
         Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
    }
}