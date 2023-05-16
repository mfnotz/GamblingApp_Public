
using AutoMapper;
using Core.Abstractions.Repository;
using Core.Abstractions.Services;
using Core.DTOs;
using Core.Models;
using Serilog;
using System.Linq.Expressions;

namespace Services
{
    public abstract class BaseService<TEntityModel, TEntityDTO> : IBaseService<TEntityModel, TEntityDTO>
        where TEntityDTO : BaseEntityDTO
        where TEntityModel : BaseEntityModel
    {

        internal ILogger _logger { get; set; }
        internal IBaseRepository<TEntityModel> _baseRepository { get; set; }
        internal IMapper _mapper { get; set; }

        public BaseService(ILogger logger, IBaseRepository<TEntityModel> genericRepository, IMapper mapper)
        {
            _logger = logger;
            _baseRepository = genericRepository;
            _mapper = mapper;
        }

        public async virtual Task<TEntityDTO> Get(int id)
        {
            try
            {
                var found = await _baseRepository.Get(id);
                if (found == null)
                {
                    return null;
                }
                var dto = _mapper.Map<TEntityDTO>(found);
                return dto;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return null;
            }
        }

        public async virtual Task<List<TEntityDTO>> Get()
        {
            try
            {
                var found = await _baseRepository.Get();
                var dto = found.Select(x => _mapper.Map<TEntityDTO>(x)).ToList();
                return dto;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return null;
            }
        }

        public async virtual Task<List<TEntityDTO>> GetBy(Func<IQueryable<TEntityModel>, IQueryable<TEntityModel>>? filter, Func<IQueryable<TEntityModel>, IOrderedQueryable<TEntityModel>>? orderBy, List<Expression<Func<TEntityModel, object>>>? includes)
        {
            try
            {
                var found = await _baseRepository.GetBy(filter, orderBy, includes);
                var dto = found.Select(x => _mapper.Map<TEntityDTO>(x)).ToList();
                return dto;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return null;
            }
        }

        public async virtual Task<PaginationResult<TEntityDTO>> GetPage(int page, int pageSize)
        {
            try
            {
                var found = await _baseRepository.GetPage(page, pageSize);
                PaginationResult<TEntityDTO> dto = EntityPageToDTO(found);
                return dto;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return null;
            }
        }

        public async virtual Task<TEntityDTO> Add(TEntityDTO entity)
        {
            try
            {
                var model = _mapper.Map<TEntityModel>(entity);
                var added = await _baseRepository.Add(model);
                var dto = _mapper.Map<TEntityDTO>(added);
                return dto;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return null;
            }
        }

        public async virtual Task<TEntityDTO> Update(TEntityDTO entity)
        {
            try
            {
                var found = await _baseRepository.Get(entity.Id);
                if (found == null)
                    return null;
                var model = _mapper.Map<TEntityModel>(entity);
                var updated = await _baseRepository.Update(model);
                var dto = _mapper.Map<TEntityDTO>(found);
                return dto;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return null;
            }
        }

        public async virtual Task<TEntityDTO> Delete(int id)
        {
            try
            {
                var found = await _baseRepository.Get(id);
                if (found == null)
                    return null;
                await _baseRepository.Delete(id);
                var dto = _mapper.Map<TEntityDTO>(found);
                return dto;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return null;
            }
        }

        internal PaginationResult<TEntityDTO> EntityPageToDTO(PaginationResult<TEntityModel> found)
        {
            return new PaginationResult<TEntityDTO>()
            {
                TotalItems = found.TotalItems,
                ItemsPerPage = found.ItemsPerPage,
                CurrentPage = found.CurrentPage,
                Items = found.Items.Select(x => _mapper.Map<TEntityDTO>(x)).ToList(),
                TotalPages = found.TotalPages
            };
        }
    }
}
