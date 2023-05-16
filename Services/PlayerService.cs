using AutoMapper;
using Core.Abstractions.Repository;
using Core.Abstractions.Services;
using Core.DTOs;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Linq.Expressions;

namespace Services
{
    public class PlayerService : IPlayerService
    {
        internal ILogger _logger { get; set; }
        internal IPlayerRepository _playerRepository { get; set; }
        internal IMapper _mapper { get; set; }

        public PlayerService(ILogger logger, IPlayerRepository userRepository, IMapper mapper)
        {
            _logger = logger;
            _playerRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<PlayerDTO> Add(PlayerDTO entity)
        {
            var model = _mapper.Map<Player>(entity);
            var added = await _playerRepository.Add(model);
            var dto = _mapper.Map<PlayerDTO>(added);

            return dto;
        }

        public async Task<PlayerDTO> Update(PlayerDTO entity)
        {
            var found = await _playerRepository.Get(entity.Id);

            if (found == null)
                return null;

            var model = _mapper.Map<Player>(entity);
            var updated = await _playerRepository.Update(model);
            var dto = _mapper.Map<PlayerDTO>(found);

            return dto;
        }

        async Task<PlayerDTO> IBaseService<Player, PlayerDTO>.Delete(int Id)
        {
            throw new NotImplementedException();
        }

        async Task<PlayerDTO> IBaseService<Player, PlayerDTO>.Get(int Id)
        {
            var found = await _playerRepository.Get(Id);

            if (found == null)
                return null;

            return _mapper.Map<PlayerDTO>(found);
        }

        Task<List<PlayerDTO>> IBaseService<Player, PlayerDTO>.Get()
        {
            throw new NotImplementedException();
        }

        Task<PaginationResult<PlayerDTO>> IBaseService<Player, PlayerDTO>.GetPage(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PlayerDTO>> GetBy(Func<IQueryable<Player>, IQueryable<Player>>? filter, Func<IQueryable<Player>, IOrderedQueryable<Player>>? orderBy, List<Expression<Func<Player, object>>>? includes)
        {
            try
            {
                var found = await _playerRepository.GetBy(filter, orderBy, includes);
                var dto = found.Select(x => _mapper.Map<PlayerDTO>(x)).ToList();
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
