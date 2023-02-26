using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Specification
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
        }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Include { get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDesc { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> expression)
        {
            Include.Add(expression);
        }

        protected void AddOrder(Expression<Func<T, object>> expression)
        {
            OrderBy = expression;
        }

        protected void AddOrderDesc(Expression<Func<T, object>> expression)
        {
            OrderByDesc = expression;
        }
    }
}
