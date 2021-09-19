using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using medievalworldweb.Data;
using medievalworldweb.Dtos.Character;
using medievalworldweb.Models;
using medievalworldweb.Repository;
using Microsoft.EntityFrameworkCore;

namespace medievalworldweb.Services
{
    public class CharacterService : ICharacterService
    {
        #region Private Fields
        private readonly IMapper _mapper;
        private readonly ICharacterRepository _characterRepository; 
        #endregion

        #region Public Methods
        public CharacterService(IMapper mapper, ICharacterRepository characterRepository)
        {
            _mapper = mapper;
            _characterRepository = characterRepository;
        }
        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            var character = await _characterRepository.GetCharacterById(id);
            if (character == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Item not found";
                serviceResponse.Data = null;
                return serviceResponse;
            }
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            return serviceResponse;
        } 
        #endregion
    }
}