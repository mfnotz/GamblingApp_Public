using AutoMapper;
using Core.Abstractions.Helpers;
using Core.Abstractions.Repository;
using Core.Abstractions.Services;
using Core.DTOs;
using Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Serilog;
using Services.Helpers;
using System.Security.Cryptography;

namespace Services.Tests
{
    public class BetServiceTests
    {
        private Mock<ILogger> mockLogger = new Mock<ILogger>();
        private Mock<IBetRepository> mockBetRepository = new Mock<IBetRepository>();
        private Mock<IPlayerService> mockUserService = new Mock<IPlayerService>();
        private Mock<IRandomNumberGenerator> mockRandomNumberGenerator = new Mock<IRandomNumberGenerator>();
        private IMapper mockMapper;
        private BetService betService;
        public BetServiceTests()
        {
            // Setup AutoMapper
            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(AutoMapperProfile)); 
            var serviceProvider = services.BuildServiceProvider();
            mockMapper = serviceProvider.GetRequiredService<IMapper>();


            betService = new BetService(mockLogger.Object, mockBetRepository.Object, mockUserService.Object, mockRandomNumberGenerator.Object, mockMapper);
        }

        
        [Fact]
        public async Task PlaceBet_InvalidBetAmount_ThrowsException()
        {
            // Arrange
            var userDTO = new PlayerDTO { Id = 1, Credit = 100 };
            var bet = new Bet { BetAmount = 0, BetChoice = 5 };

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(async () => await betService.PlaceBet(bet, userDTO));
        }

        [Fact]
        public async Task PlaceBet_InvalidBetChoice_ThrowsException()
        {
            // Arrange
            var userDTO = new PlayerDTO { Id = 1, Credit = 100 };
            var bet = new Bet { BetAmount = 10, BetChoice = 11 };

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(async () => await betService.PlaceBet(bet, userDTO));
        }

        [Fact]
        public async Task PlaceBet_InsufficientBalance_ThrowsException()
        {
            // Arrange
            var userDTO = new PlayerDTO { Id = 1, Credit = 100 };
            var bet = new Bet { BetAmount = 200, BetChoice = 5 };

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(async () => await betService.PlaceBet(bet, userDTO));
        }

        [Fact]
        public async Task PlaceBet_ValidBet_ReturnsWinResult()
        {
            // Arrange
            var userDTO = new PlayerDTO { Id = 1, Credit = 100 };
            var bet = new Bet { BetAmount = 10, BetChoice = 5 };
            var currentCredit = userDTO.Credit;

            // Use the controlled random number generator
            var controlledRandomNumberGenerator = new ControlledRandomNumberGenerator(5);

            // Create the BetService instance with the controlled random number generator
            var betService = new BetService(mockLogger.Object, mockBetRepository.Object, mockUserService.Object, controlledRandomNumberGenerator, mockMapper);

            // Act
            var result = await betService.PlaceBet(bet, userDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(BetStatusEnum.Win, result.Status);
            Assert.Equal(currentCredit + (bet.BetAmount * 9), result.ResultingCredit);
        }

    }
}
