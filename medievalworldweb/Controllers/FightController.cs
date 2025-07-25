using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using medievalworldweb.Dtos.Fight;
using medievalworldweb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace medievalworldweb.Controllers
{
    [Authorize]
    [EnableCors]
    [ApiController]
    [Route("[controller]")]
    public class FightController : ControllerBase
    {
        private readonly IFightService _fightService;
        public FightController(IFightService fightService)
        {
            _fightService = fightService;
        }
        [HttpPost, Route("Weapon")]
        public async Task<IActionResult> WeaponAttack([FromBody] WeaponAttackDto weaponAttackDto)
        {
            var response = await _fightService.WeaponAttack(weaponAttackDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost, Route("WeaponAttackDecRelease")]
        public async Task<IActionResult> WeaponAttackDecRelease([FromBody] WeaponAttackDto weaponAttackDto)
        {
            var response = await _fightService.WeaponAttack(weaponAttackDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        public async Task<IActionResult> EffectiveWeaponAttack([FromBody] WeaponAttackDto weaponAttackDto)
        {
            var response = await _fightService.WeaponAttack(weaponAttackDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost, Route("Skill")]
        public async Task<IActionResult> SkillAttack([FromBody] SkillAttackDto skillAttackDto)
        {
            var response = await _fightService.SkillAttack(skillAttackDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Fight([FromBody] FightRequestDto fightRequestDto)
        {
            var response = await _fightService.Fight(fightRequestDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet, Route("Fighttally/{userId:int}")]
        public async Task<IActionResult> GetFightTally(int userId)
        {
            var response = await _fightService.GetFightTally(userId);
            if (!response.Success)
            {
                return StatusCode(StatusCodes.Status404NotFound, response.Message);
            }
            return Ok(response);
        }
        [HttpGet, Route("Fightdetail")]
        public async Task<IActionResult> GetFightAttachDetails()
        {
            var response = await _fightService.GetFightAttackDetails();
            if (!response.Success)
            {
                return StatusCode(StatusCodes.Status404NotFound, response.Message);
            }
            return Ok(response);
        }
        [HttpGet, Route("Fighters")]
        public async Task<IActionResult> GetFighters()
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            return Ok(await _fightService.GetFightersForUser(userId));
        }

        [HttpGet, Route("Reset/{userId:int}")]
        public async Task<IActionResult> Reset(int userId)
        {
            var response = await _fightService.Reset(userId);
            if (!response.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.Message);
            }
            return Ok(response);
        }
    }
}
