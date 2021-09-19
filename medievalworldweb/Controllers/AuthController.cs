using System.Threading.Tasks;
using medievalworldweb.Data;
using medievalworldweb.Dtos.Character;
using medievalworldweb.Models;
using Microsoft.AspNetCore.Mvc;

namespace medievalworldweb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            ServiceResponse<int> serviceResponse = await _authRepository.Register(userDto.Username, userDto.Password);
            return Ok(serviceResponse);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserDto userDto)
        {
            ServiceResponse<string> serviceResponse = await _authRepository.Login(userDto.Username, userDto.Password);
            return Ok(serviceResponse);
        }
    }
}