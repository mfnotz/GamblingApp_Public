using Core.Abstractions.Repository;
using Core.DTOs;
using Core.Helpers.Extensions;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Linq.Expressions;

namespace Repository
{
    public abstract class BaseRepository<TEntityModel> : IBaseRepository<TEntityModel> where TEntityModel : BaseEntityModel
        {
            internal IGamblingContext _context { get; set; }
            internal ILogger _logger { get; set; }

            public BaseRepository(ILogger logger, IGamblingContext context)
            {
                _logger = logger;
                _context = context;
            }

            public async virtual Task<TEntityModel> Get(int id, List<Expression<Func<TEntityModel, object>>> includes)
            {
                return await _context.Set<TEntityModel>().AsQueryable().IncludeMultiple(includes).FirstOrDefaultAsync(x => x.Id == id);
            }

            public async virtual Task<List<TEntityModel>> Get(List<Expression<Func<TEntityModel, object>>> includes)
            {
                return await _context.Set<TEntityModel>().AsQueryable().IncludeMultiple(includes).ToListAsync();
            }

            public async Task<List<TEntityModel>> GetBy(Func<IQueryable<TEntityModel>, IQueryable<TEntityModel>>? filter, Func<IQueryable<TEntityModel>, IOrderedQueryable<TEntityModel>>? orderBy, List<Expression<Func<TEntityModel, object>>>? includes)
            {
                var query = _context.Set<TEntityModel>().AsQueryable().IncludeMultiple(includes);
                if (filter != null)
                {
                    query = filter(query);
                }
                if (orderBy != null)
                {
                    query = orderBy(query);
                }
                return await query.ToListAsync();
            }

            public async virtual Task<PaginationResult<TEntityModel>> GetPage(int page, int pageSize, Func<IQueryable<TEntityModel>, IQueryable<TEntityModel>>? filter = null,
                Func<IQueryable<TEntityModel>, IOrderedQueryable<TEntityModel>>? orderBy = null)
            {
                var query = _context.Set<TEntityModel>().AsQueryable();
                if (filter != null)
                {
                    query = filter(query);
                }
                if (orderBy != null)
                {
                    query = orderBy(query);
                }

                var totalResults = await query.CountAsync();
                var results = await query.Skip((page - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToListAsync();

                return new PaginationResult<TEntityModel>
                {
                    Items = results,
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = totalResults,
                    TotalPages = Convert.ToInt32(totalResults / pageSize)
                };
            }

            public async Task<IQueryable<TEntityModel>> GetQuery()
            {
                return await Task.Run(() =>
                {
                    var query = _context.Set<TEntityModel>().AsQueryable();
                    return query;
                });
            }

            public async virtual Task<TEntityModel> Add(TEntityModel entity)
            {
                _context.Set<TEntityModel>().Add(entity);
                await _context.SaveChangesAsync();
                entity = await _context.Set<TEntityModel>().FirstOrDefaultAsync(x => x.Id == entity.Id);
                return entity;
            }

            public async virtual Task<List<TEntityModel>> Add(List<TEntityModel> entities)
            {
                _context.Set<TEntityModel>().AddRange(entities);
                await _context.SaveChangesAsync();
                return await _context.Set<TEntityModel>().Where(x => entities.FindIndex(m => m.Id == x.Id) > 0).ToListAsync();
            }

            public async virtual Task<TEntityModel> Update(TEntityModel entity)
            {
                _context.Set<TEntityModel>().Update(entity);
                await _context.SaveChangesAsync();
                entity = await _context.Set<TEntityModel>().FirstOrDefaultAsync(x => x.Id == entity.Id);

                return entity;
            }

            public async virtual Task<TEntityModel> AddOrUpdate(TEntityModel entity)
            {
                if (entity.Id == 0)
                    entity = await Add(entity);
                else
                    entity = await Update(entity);

                return entity;
            }

            public async virtual Task Delete(int id)
            {
                var foundObject = await _context.Set<TEntityModel>().FirstOrDefaultAsync(x => x.Id == id);

                _context.Set<TEntityModel>().Remove(foundObject);
                await _context.SaveChangesAsync();
            }

        }
    }
