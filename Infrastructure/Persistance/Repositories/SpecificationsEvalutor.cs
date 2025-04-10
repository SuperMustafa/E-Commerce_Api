using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public static class SpecificationsEvalutor
    {
        public static IQueryable<T> GetQuery<T>(IQueryable<T> Inputquery,Specifications<T> specifications) where T : class 
        {
            var query = Inputquery;
            if(specifications.Criteria is not null)
            {
                query = query.Where(specifications.Criteria);
            }
            query = specifications.IncludeExpressions.Aggregate(query, (currentQuery, incluedExpression) => currentQuery.Include(incluedExpression));
            if(specifications.OrderBy is not null)
            {
                query=query.OrderBy(specifications.OrderBy);
            }
            else if (specifications.OrderByDescending is not null)
            {
                query = query.OrderByDescending(specifications.OrderByDescending);
            }
            if (specifications.IsPaginated)
            {
                query=query.Skip(specifications.Skip).Take(specifications.Take);
            }
            return query;
        }
    }
}
