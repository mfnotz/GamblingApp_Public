using Core.DTOs;
using Core.Models;
using System.Linq.Expressions;

namespace Core.Abstractions.Repository
{
    public interface IBaseRepository<TEntityModel> where TEntityModel : BaseEntityModel
    {
        public Task<TEntityModel> Get(int id, List<Expression<Func<TEntityModel, object>>> includes = null);

        public Task<List<TEntityModel>> Get( List<Expression<Func<TEntityModel, object>>> includes = null);


        Task<PaginationResult<TEntityModel>> GetPage(int page, int pageSize, Func<IQueryable<TEntityModel>, IQueryable<TEntityModel>>? filter = null, Func<IQueryable<TEntityModel>, IOrderedQueryable<TEntityModel>>? orderBy = null);
        Task<List<TEntityModel>> GetBy(Func<IQueryable<TEntityModel>, IQueryable<TEntityModel>>? filter = null, Func<IQueryable<TEntityModel>, IOrderedQueryable<TEntityModel>>? orderBy = null, List<Expression<Func<TEntityModel, object>>>? includes = null);
        Task<IQueryable<TEntityModel>> GetQuery();

        public Task<TEntityModel> Add(TEntityModel entity);
        public Task<List<TEntityModel>> Add(List<TEntityModel> entities);

        public Task<TEntityModel> Update(TEntityModel entity);

        public Task Delete(int id);
    }
}
