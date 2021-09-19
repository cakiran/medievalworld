using System.Threading.Tasks;
using medievalworldweb.Data;
using medievalworldweb.Dtos.Fight;
using medievalworldweb.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using AutoMapper;
using medievalworldweb.Repository;
using System.Dynamic;

namespace medievalworldweb.Services
{
    public class FightService : IFightService
    {
        #region Private Fields
        private readonly IFightRepository _fightRepository;
        private readonly ICharacterRepository _characterRepository;
        private readonly ISqlService _sqlService;
        private readonly IMapper _mapper; 
        #endregion

        #region Public Methods
        public FightService(IFightRepository fightRepository, ICharacterRepository characterRepository, ISqlService sqlService, IMapper mapper)
        {
            _mapper = mapper;
            _fightRepository = fightRepository;
            _characterRepository = characterRepository;
            _sqlService = sqlService;
        }

        public async Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto fightRequestDto)
        {
            ServiceResponse<FightResultDto> response = new ServiceResponse<FightResultDto>
            {
                Data = new FightResultDto()
            };
            await _sqlService.ExecuteSql("TRUNCATE TABLE [FightAttackDetails]");
            var result = await _fightRepository.GetAllFighters(x => fightRequestDto.Fighters.Contains(x.Id), x => new CharacterToFightDto
            {
                Id = x.Id,
                Defense = x.Defense,
                HitPoints = x.HitPoints,
                Intelligence = x.Intelligence,
                Weapon = x.Weapon,
                Skills = x.CharacterSkills.Select(x => x.Skill).ToArray(),
                Name = x.Name,
                Strength = x.Strength,
                Class = x.Class,
                User = x.User,
                Fights = x.Fights,
                Victories = x.Victories,
                Defeats = x.Defeats
            }).ConfigureAwait(false);
            bool defeated = false;
            while (defeated == false)
            {
                foreach (var attacker in result)
                {
                    var opponent = result.FirstOrDefault(c => c.Id != attacker.Id);
                    bool doWeaponAttack = new System.Random().Next(2) == 0 ? true : false;
                    string attackUsed = string.Empty;
                    int damage = 0;
                    if (doWeaponAttack)
                    {
                        damage = DoWeaponAttack(attacker.Weapon.Damage, attacker.Strength, opponent.Defense);
                        attackUsed = attacker.Weapon.Name;
                        response.Data.FightLog.Add($"{attacker.Name} attacks {opponent.Name} - {attackUsed} - {(damage > 0 ? damage : 0)} damage");
                    }
                    else
                    {
                        Skill skillInAttacker = attacker.Skills[new System.Random().Next(0, attacker.Skills.Count() - 1)];
                        damage = DoSkillAttack(skillInAttacker.Damage, attacker.Intelligence, opponent.Defense);
                        attackUsed = skillInAttacker.Name;
                        response.Data.FightLog.Add($"{attacker.Name} attacks {opponent.Name} using {attackUsed} - {(damage > 0 ? damage : 0)} damage");
                    }
                    if (damage > 0)
                    {
                        opponent.HitPoints -= damage;
                        FightAttackDetail fightAttackDetail = new FightAttackDetail
                        {
                            OpponentId = opponent.Id,
                            OpponentName = opponent.Name,
                            OpponentHP = opponent.HitPoints,
                            LogText = $"{attacker.Name} attacks {opponent.Name} using {attackUsed} - {(damage > 0 ? damage : 0)} damage"
                        };
                        await _fightRepository.AddFightAttackDetails(fightAttackDetail);
                    }
                    if (opponent.HitPoints <= 0)
                    {
                        defeated = true;
                        response.Data.FightLog.Add($"{opponent.Name} has been defeated by {attacker.Name}");
                        response.Data.FightLog.Add($"{attacker.Name} is the WINNER with HP {attacker.HitPoints} left!");
                        attacker.Victories++;
                        opponent.Defeats++;
                        attacker.Fights++;
                        opponent.Fights++;
                        FightAttackDetail fightAttackDetail = new FightAttackDetail
                        {
                            OpponentId = opponent.Id,
                            OpponentName = opponent.Name,
                            OpponentHP = opponent.HitPoints,
                            AttckerHP = attacker.HitPoints,
                            LogText = $"{attacker.Name} attacks {opponent.Name} using {attackUsed} - {(damage > 0 ? damage : 0)} damage",
                            Winner = attacker.Name,
                            Loser = opponent.Name,
                            WinnerId = attacker.Id
                        };
                        FightAttackDetail fightAttackDetail1 = new FightAttackDetail
                        {
                            OpponentId = opponent.Id,
                            OpponentName = opponent.Name,
                            OpponentHP = opponent.HitPoints,
                            LogText = $"{attacker.Name} is the WINNER with HP {attacker.HitPoints} left!",
                            Winner = attacker.Name,
                            Loser = opponent.Name,
                            WinnerId = attacker.Id,
                            AttckerHP = attacker.HitPoints
                        };
                        await _fightRepository.AddFightAttackDetails(fightAttackDetail1);
                        break;
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                }
            }
            var charactersToUpdate = result.Select(x => new Character
            {
                Id = x.Id,
                Fights = x.Fights,
                Victories = x.Victories,
                Defeats = x.Defeats,
                Name = x.Name,
                Intelligence = x.Intelligence,
                HitPoints = 100,
                Strength = 20,
                Defense = 20,
                Class = x.Class
            }).ToArray();
            await _characterRepository.UpdateCharacters(charactersToUpdate);
            response.Success = true;
            return response;
        }


        public async Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto skillAttackDto)
        {
            ServiceResponse<AttackResultDto> response = new ServiceResponse<AttackResultDto>();
            Character attacker = await _characterRepository.GetCharacterById(skillAttackDto.AttackerId);
            Character opponent = await _characterRepository.GetCharacterById(skillAttackDto.OpponentId);
            if (attacker == null || opponent == null)
            {
                response.Success = false;
                response.Message = "Attacker or opponent not found in the database.";
                return response;
            }
            CharacterSkill skillInAttacker = attacker.CharacterSkills.FirstOrDefault(cs => cs.SkillId == skillAttackDto.SkillId);
            if (skillInAttacker == null)
            {
                response.Success = false;
                response.Message = $"{attacker.Name} does not have this skill.";
                return response;
            }
            CharacterSkill skillInOpponent = opponent.CharacterSkills.FirstOrDefault(cs => cs.SkillId == skillAttackDto.SkillId);
            if (skillInOpponent == null)
            {
                response.Success = false;
                response.Message = $"{opponent.Name} does not have this skill.";
                return response;
            }
            int damage = DoSkillAttack(skillInAttacker.Skill.Damage, attacker.Intelligence, opponent.Defense);
            if (damage > 0)
                opponent.HitPoints -= damage;
            if (opponent.HitPoints <= 0)
                response.Message = $"{opponent.Name} has been defeated by {attacker.Name}";
            await _characterRepository.UpdateCharacter(opponent);
            response.Data = new AttackResultDto
            {
                AttackerName = attacker.Name,
                OpponentName = opponent.Name,
                AttackerHitPoints = attacker.HitPoints,
                OpponentHitPoints = opponent.HitPoints,
                Damage = damage
            };
            return response;
        }


        public async Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto weaponAttackDto)
        {
            ServiceResponse<AttackResultDto> response = new Models.ServiceResponse<AttackResultDto>();
            var attacker = await _characterRepository.GetCharacterById(weaponAttackDto.AttackerId);
            var opponent = await _characterRepository.GetCharacterById(weaponAttackDto.OpponentId);
            if (attacker == null || opponent == null)
            {
                response.Success = false;
                response.Message = "Attacker or opponent not found in the database.";
                return response;
            }
            int damage = DoWeaponAttack(attacker.Weapon.Damage, attacker.Strength, opponent.Defense);

            if (damage > 0)
                opponent.HitPoints -= damage;
            if (opponent.HitPoints <= 0)
                response.Message = $"{opponent.Name} has been defeated by {attacker.Name}";
            await _characterRepository.UpdateCharacter(opponent);
            response.Data = new AttackResultDto
            {
                AttackerName = attacker.Name,
                OpponentName = opponent.Name,
                AttackerHitPoints = attacker.HitPoints,
                OpponentHitPoints = opponent.HitPoints,
                Damage = damage
            };
            return response;
        }

        public async Task<ServiceResponse<List<GetFightTallyDto>>> GetFightTally(int userId)
        {
            ServiceResponse<List<GetFightTallyDto>> response = new ServiceResponse<List<GetFightTallyDto>>();
            List<GetFightTallyDto> fightTally = new List<GetFightTallyDto>();
            var allCharacters = await _characterRepository.GetAllCharacters(x => x.User.Id == userId, x => new Character
            {
                Name = x.Name,
                Victories = x.Victories,
                Defeats = x.Defeats,
                Fights = x.Fights
            });
            var fightTallyDetails = allCharacters.GroupBy(x => x.Name).Select(x => new
            {
                Name = x.Key,
                Victories = x.Sum(c => c.Victories),
                Defeats = x.Sum(c => c.Defeats),
                Fights = x.Sum(c => c.Fights)
            });
            foreach (var ft in fightTallyDetails)
            {
                fightTally.Add(new GetFightTallyDto
                {
                    Id = 0,
                    Name = ft.Name,
                    Fights = ft.Fights,
                    Victories = ft.Victories,
                    Defeats = ft.Defeats,
                    Username = ft.Name
                });
            }
            response.Data = fightTally;
            response.Success = true;
            return response;
        }

        public async Task<ServiceResponse<List<GetFightAttactDetailDto>>> GetFightAttackDetails()
        {
            ServiceResponse<List<GetFightAttactDetailDto>> response = new ServiceResponse<List<GetFightAttactDetailDto>>
            {
                Data = new List<GetFightAttactDetailDto>()
            };
            var result = await _fightRepository.GetAllFightAttackDetails(x => x).ConfigureAwait(false);

            foreach (FightAttackDetail fa in result.ToList())
            {
                response.Data.Add(new GetFightAttactDetailDto
                {
                    OpponentId = fa.OpponentId,
                    OpponentName = fa.OpponentName,
                    LogText = fa.LogText,
                    Winner = fa.Winner,
                    Loser = fa.Loser,
                    WinnerId = fa.WinnerId,
                    AttackerHP = fa.AttckerHP,
                    OpponentHP = fa.OpponentHP
                });
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetFighterDto>>> GetFightersForUser(int userId)
        {
            ServiceResponse<List<GetFighterDto>> response = new ServiceResponse<List<GetFighterDto>>();
            var fighters = await _fightRepository.GetAllFighters(x => x.User.Id == Convert.ToInt32(userId), x => x);
            response.Data = new List<GetFighterDto>();
            response.Data = _mapper.Map<List<GetFighterDto>>(fighters);
            return response;
        }

        public async Task<ServiceResponse<string>> Reset(int userId)
        {
            ServiceResponse<string> response = new Models.ServiceResponse<string>();
            try
            {
                var characters = await _characterRepository.GetAllCharacters(x => x.User.Id == userId, x => x);
                foreach (var character in characters)
                {
                    character.HitPoints = 100;
                }
                await _characterRepository.UpdateCharacters(characters);
                await _sqlService.ExecuteSql("TRUNCATE TABLE [FightAttackDetails]");
                response.Message = "Success";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;

        }
        #endregion

        #region Private Methods

        private static int DoSkillAttack(int skillDamage, int attackerIntelligence, int opponentDefense)
        {
            int damage = skillDamage + new System.Random().Next(attackerIntelligence);
            damage -= new System.Random().Next(opponentDefense);
            return damage;
        }

        private static int DoWeaponAttack(int attackerDamage, int attackerStrength, int opponentDefense)
        {
            int damage = attackerDamage + new System.Random().Next(attackerStrength);
            damage -= new System.Random().Next(opponentDefense);
            return damage;
        } 
        #endregion
    }
}