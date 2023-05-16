using AutoMapper;
using Core.Abstractions.Helpers;
using Core.Abstractions.Repository;
using Core.Abstractions.Services;
using Core.DTOs;
using Core.Helpers.Extensions;
using Core.Models;
using Serilog;
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace Services
{
    public class BetService : IBetService
    {
        internal ILogger _logger { get; set; }
        internal IBetRepository _betRepository { get; set; }
        internal IPlayerService _userService { get; set; }
        internal IMapper _mapper { get; set; }
        internal IRandomNumberGenerator _randomNumberGenerator { get; set; }

        public BetService(ILogger logger, IBetRepository betRepository, IPlayerService userService, IRandomNumberGenerator randomNumberGenerator, IMapper mapper)
        {
            _logger = logger;
            _betRepository = betRepository;
            _userService = userService;
            _randomNumberGenerator = randomNumberGenerator;
            _mapper = mapper;
        }

        public async Task<BetResult> PlaceBet(Bet bet, PlayerDTO userDTO)
        {
            // Check if player is allowed to play
            if (userDTO == null)
                throw new ArgumentNullException("User not allowed.");

            //Check if valid bet
            if (bet.BetAmount <= 0)
            {
                throw new Exception("Invalid bet amount.");
            }

            //Check if valid bet
            if (bet.BetChoice > 10 || bet.BetChoice < 0)
            {
                throw new Exception("Invalid bet amount.");
            }

            // Check if player has enough balance for the bet
            if (bet.BetAmount > userDTO.Credit)
            {
                throw new Exception("Insufficient balance.");
            }

            
            //Initialize variables
            var resObj = new BetResult();
            var initialCredit = userDTO.Credit;

            // Generate random number between 0 and 9
            resObj.BetActualResult = _randomNumberGenerator.Generate(0, 10);

            bet.UserId = userDTO.Id;
            // Check if player's bet matches the random number
            if (bet.BetChoice == resObj.BetActualResult)
            {
                // Update player's account balance
                userDTO.Credit += (bet.BetAmount * 9);

                //Fill return object
                resObj.ResultingCredit = userDTO.Credit;
                resObj.Status = BetStatusEnum.Win;
                
            }
            else
            {
                // Update player's account balance
                userDTO.Credit -= bet.BetAmount;

                // Return failure response
                resObj.ResultingCredit = userDTO.Credit;
                resObj.Status = BetStatusEnum.Loss;
            }

            try
            {
                //Save history
                var betDTO = _mapper.Map<BetDTO>(bet);
                betDTO.BetActualResult = resObj.BetActualResult;

                Add(betDTO);
                await _userService.Update(userDTO);
            }
            catch(Exception ex) // If there is any issue saving the bet, we rollback the credit for the user
            {
                resObj.ResultingCredit = initialCredit;
                resObj.Status = BetStatusEnum.Error;

                _logger.Error("Error while saving Bet {0} from User {1} ==> {2}", bet.ToJSON(), userDTO.ToJSON(), ex.Message);
            }

            return resObj;

        }

        public Task<BetDTO> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<BetDTO>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<PaginationResult<BetDTO>> GetPage(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async virtual Task<BetDTO> Add(BetDTO entity)
        {
            var model = _mapper.Map<Bet>(entity);
            var added = await _betRepository.Add(model);
            var dto = _mapper.Map<BetDTO>(added);

            return dto;
        }

        public Task<BetDTO> Update(BetDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task<BetDTO> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BetDTO>> GetBy(Func<IQueryable<Bet>, IQueryable<Bet>>? filter, Func<IQueryable<Bet>, IOrderedQueryable<Bet>>? orderBy, List<Expression<Func<Bet, object>>>? includes)
        {
            try
            {
                var found = await _betRepository.GetBy(filter, orderBy, includes);
                var dto = found.Select(x => _mapper.Map<BetDTO>(x)).ToList();
                return dto;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return null;
            }
        }
    }
}