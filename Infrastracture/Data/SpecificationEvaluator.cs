using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastracture.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity:BaseEntity
    {

        public static IQueryable<TEntity> getQuery(IQueryable<TEntity> inputQuery,
                                                   ISpecification<TEntity> spec){
            var query=inputQuery;
            if (spec.Criteria!=null){
                query=query.Where(spec.Criteria);
            }
            query = spec.Includes.Aggregate(query,(current,include)=>current.Include(include));            
            return query;
        }
        
    }
}