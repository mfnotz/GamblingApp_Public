using Core.DTOs;
using Core.Models;
using System.Linq.Expressions;

namespace Core.Abstractions.Services
{
    public interface IBaseService<TEntityModel, TEntityDTO> where TEntityModel : BaseEntityModel where TEntityDTO : BaseEntityDTO
    {
        public Task<TEntityDTO> Get(int id);

        public Task<List<TEntityDTO>> Get();
        public Task<List<TEntityDTO>> GetBy(Func<IQueryable<TEntityModel>, IQueryable<TEntityModel>>? filter, Func<IQueryable<TEntityModel>, IOrderedQueryable<TEntityModel>>? orderBy, List<Expression<Func<TEntityModel, object>>>? includes);

        Task<PaginationResult<TEntityDTO>> GetPage(int page, int pageSize);

        public Task<TEntityDTO> Add(TEntityDTO entity);

        public Task<TEntityDTO> Update(TEntityDTO entity);

        public Task<TEntityDTO> Delete(int id);
    }
}
