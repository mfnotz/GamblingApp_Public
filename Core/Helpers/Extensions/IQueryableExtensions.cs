using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.Helpers.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<TEntityModel> IncludeMultiple<TEntityModel>(this IQueryable<TEntityModel> query, IEnumerable<Expression<Func<TEntityModel, object>>> includes)
        where TEntityModel : BaseEntityModel
        {
            if (includes != null)
            {
                return includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query;
        }
    }
}
