using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> expression);
        T Get(Expression<Func<T, bool>> expression);
        T Find(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        int Save();
        void Increase(int id, Expression<Func<T, int>> propertySelector, int amount);
        void Decrease(int id, Expression<Func<T, int>> propertySelector, int amount);

        //Async Methods
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression); 
        Task<int> SaveAsync();
        Task<T> FindAsync(int id);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task IncreaseAsync(int id, Expression<Func<T, int>> propertySelector, int amount);
        Task DecreaseAsync(int id, Expression<Func<T, int>> propertySelector, int amount);
    }
}
