using AutoMapper;
using Core.Abstractions.Repository;
using Core.Abstractions.Services;
using Core.DTOs;
using Core.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserInfoService : IUserInfoService
    {
        internal ILogger _logger { get; set; }
        internal IUserInfoRepository _userInfoRepository { get; set; }
        internal IMapper _mapper { get; set; }

        public UserInfoService(ILogger logger, IUserInfoRepository userRepository, IMapper mapper)
        {
            _logger = logger;
            _userInfoRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<UserInfoDTO> Add(UserInfoDTO entity)
        {
            var model = _mapper.Map<UserInfo>(entity);
            var added = await _userInfoRepository.Add(model);
            var dto = _mapper.Map<UserInfoDTO>(added);

            return dto;
        }

        public async Task<UserInfoDTO> Update(UserInfoDTO entity)
        {
            var found = await _userInfoRepository.Get(entity.Id);

            if (found == null)
                return null;

            var model = _mapper.Map<UserInfo>(entity);
            var updated = await _userInfoRepository.Update(model);
            var dto = _mapper.Map<UserInfoDTO>(found);

            return dto;
        }

        Task<UserInfoDTO> IBaseService<UserInfo, UserInfoDTO>.Delete(int id)
        {
            throw new NotImplementedException();
        }

        Task<UserInfoDTO> IBaseService<UserInfo, UserInfoDTO>.Get(int id)
        {
            throw new NotImplementedException();
        }

        Task<List<UserInfoDTO>> IBaseService<UserInfo, UserInfoDTO>.Get()
        {
            throw new NotImplementedException();
        }

        Task<PaginationResult<UserInfoDTO>> IBaseService<UserInfo, UserInfoDTO>.GetPage(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserInfoDTO>> GetBy(Func<IQueryable<UserInfo>, IQueryable<UserInfo>>? filter, Func<IQueryable<UserInfo>, IOrderedQueryable<UserInfo>>? orderBy, List<Expression<Func<UserInfo, object>>>? includes)
        {
            try
            {
                var found = await _userInfoRepository.GetBy(filter, orderBy, includes);
                var dto = found.Select(x => _mapper.Map<UserInfoDTO>(x)).ToList();
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
