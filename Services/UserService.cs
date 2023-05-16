using AutoMapper;
using Core.Abstractions.Repository;
using Core.Abstractions.Services;
using Core.DTOs;
using Core.Models;
using Serilog;
using System.Linq.Expressions;

namespace Services
{
    public class UserService : IUserService
    {
        internal ILogger _logger { get; set; }
        internal IUserRepository _userRepository { get; set; }
        internal IMapper _mapper { get; set; }

        public UserService(ILogger logger, IUserRepository userRepository, IMapper mapper)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<UserDTO> Add(UserDTO entity)
        {
            var model = _mapper.Map<User>(entity);
            var added = await _userRepository.Add(model);
            var dto = _mapper.Map<UserDTO>(added);

            return dto;
        }

        public async Task<UserDTO> Update(UserDTO entity)
        {
            var found = await _userRepository.Get(entity.Id);

            if (found == null)
                return null;

            var model = _mapper.Map<User>(entity);
            var updated = await _userRepository.Update(model);
            var dto = _mapper.Map<UserDTO>(found);

            return dto;
        }

        Task<UserDTO> IBaseService<User, UserDTO>.Delete(int id)
        {
            throw new NotImplementedException();
        }

        Task<UserDTO> IBaseService<User, UserDTO>.Get(int id)
        {
            throw new NotImplementedException();
        }

        Task<List<UserDTO>> IBaseService<User, UserDTO>.Get()
        {
            throw new NotImplementedException();
        }

        Task<PaginationResult<UserDTO>> IBaseService<User, UserDTO>.GetPage(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserDTO>> GetBy(Func<IQueryable<User>, IQueryable<User>>? filter, Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy, List<Expression<Func<User, object>>>? includes)
        {
            try
            {
                var found = await _userRepository.GetBy(filter, orderBy, includes);
                var dto = found.Select(x => _mapper.Map<UserDTO>(x)).ToList();
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
