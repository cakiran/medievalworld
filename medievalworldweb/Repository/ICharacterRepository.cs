using medievalworldweb.Dtos.Character;
using medievalworldweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace medievalworldweb.Repository
{
    public interface ICharacterRepository
    {
        Task<Character> GetCharacterById(int id);
        Task UpdateCharacters(Character[] characters);
        Task UpdateCharacter(Character character);
        Task<TResult[]> GetAllCharacters<TResult>(Expression<Func<Character, bool>> filter, Expression<Func<Character, TResult>> selector);
    }
    public class CharacterRepository : GenericRepository<MedievalWorldDatabaseContext>, ICharacterRepository
    {

        public async Task<Character> GetCharacterById(int id)
        {
            var character = await SingleOrDefaultAsync<Character, Character>(x => x.Id == id, x => x);
            return character;
        }

        public async Task UpdateCharacters(Character[] characters)
        {
        //update comment
            await UpdateChanges(characters);
        }

        public async Task UpdateCharacter(Character model)
        {
            await UpdateChange(model);
        }

        public async Task<TResult[]> GetAllCharacters<TResult>(Expression<Func<Character, bool>> filter,Expression<Func<Character, TResult>> selector)
        {
                var characters = await FindAllWithFilterAsync(filter,selector);
                return characters;
        }
    }
}
