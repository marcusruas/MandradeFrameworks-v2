using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MandradeFrameworks.Repositorios.Models
{
    public abstract class BaseSpecification<T>
    {
        /// <summary>
        /// Instancia a classe de specification para obter todos os registros da entidade genérica especificada
        /// </summary>
        public BaseSpecification()
        {
            Includes = new List<Expression<Func<T, object>>>();
        }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criterios = criteria;
            Includes = new List<Expression<Func<T, object>>>();
        }

        public Expression<Func<T, bool>> Criterios { get; }
        public List<Expression<Func<T, object>>> Includes { get; }
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> include)
        {
            Includes.Add(include);
        }
        protected void AddOrderBy(Expression<Func<T, object>> orderByExpressao)
        {
            OrderBy = orderByExpressao;
        }
        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpressao)
        {
            OrderByDescending = orderByDescExpressao;
        }
    }
}
