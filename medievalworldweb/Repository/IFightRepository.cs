using medievalworldweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace medievalworldweb.Repository
{
    public interface IFightRepository
    {
        Task<TResult[]> GetAllFighters<TResult>(Expression<Func<Character, bool>> predicate, Expression<Func<Character, TResult>> selector);
        Task AddFightAttackDetails(FightAttackDetail fightAttackDetail);
        Task<TResult[]> GetAllFightAttackDetails<TResult>(Expression<Func<FightAttackDetail, TResult>> selector);
    }
    public class FightRepository : GenericRepository<MedievalWorldDatabaseContext>, IFightRepository
    {
        public async Task<TResult[]> GetAllFighters<TResult>(Expression<Func<Character, bool>> predicate, Expression<Func<Character, TResult>> selector)
        {
            var character = await FindAllWithFilterAsync(predicate, selector);
            return character;
        }
        public async Task AddFightAttackDetails(FightAttackDetail fightAttackDetail)
        {
            await Add(fightAttackDetail);
        }

        public async Task<TResult[]> GetAllFightAttackDetails<TResult>(Expression<Func<FightAttackDetail, TResult>> selector)
        {
            var characters = await FindAllAsync(selector);
            return characters;
        }
    }
}
