using System.Threading.Tasks;
using medievalworldweb.Models;
using medievalworldweb.Dtos.Fight;
using System.Collections.Generic;

namespace medievalworldweb.Services
{
    public interface IFightService
    {
        Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto weaponAttackDto);
        Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto skillAttackDto);
        Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto fightRequestDto);
        Task<ServiceResponse<List<GetFightTallyDto>>> GetFightTally(int userId);
        Task<ServiceResponse<List<GetFightAttactDetailDto>>> GetFightAttackDetails();
        Task<ServiceResponse<List<GetFighterDto>>> GetFightersForUser(int userId);
        Task<ServiceResponse<string>> Reset(int userId);
    }
}