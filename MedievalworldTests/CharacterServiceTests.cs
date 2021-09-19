using AutoMapper;
using medievalworldweb.Models;
using medievalworldweb.Repository;
using medievalworldweb.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace MedievalworldTests
{
    [TestFixture]
    public class CharacterServiceTests
    {
        [Test]
        public async Task GetCharacterById_WhenValidCharacterIsReturned_ServiceResponseSucceeds()
        {
            //arrange
            var loggerMock = new Mock<IMapper>();
            var characterRepoMock = new Mock<ICharacterRepository>();
            Character c = new Character();
            characterRepoMock.Setup(x => x.GetCharacterById(It.IsAny<int>())).ReturnsAsync(c);
            var _target = new CharacterService(loggerMock.Object, characterRepoMock.Object);
            //act
            var res = await _target.GetCharacterById(It.IsAny<int>());
            //assert
            Assert.That(res.Success == true);
        }
        [Test]
        public async Task GetCharacterById_WhenNoCharacterIsReturned_ServiceResponseFails()
        {
            //arrange
            var loggerMock = new Mock<IMapper>();
            var characterRepoMock = new Mock<ICharacterRepository>();
            Character c = null;
            characterRepoMock.Setup(x => x.GetCharacterById(It.IsAny<int>())).ReturnsAsync(c);
            var _target = new CharacterService(loggerMock.Object, characterRepoMock.Object);
            //act
            var res = await _target.GetCharacterById(It.IsAny<int>());
            //assert
            Assert.That(res.Success == false);
        }
    }
}
