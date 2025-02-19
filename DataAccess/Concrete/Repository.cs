using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Concrete
{
    public class Repository<T> : IRepository<T> where T : class
    {
        internal Context _context;
        internal DbSet<T> _dbSet;

        public Repository(Context context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public T Find(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T> FindAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

       

        public T Get(Expression<Func<T, bool>> expression)
        {
            return _dbSet.FirstOrDefault(expression);
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public List<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).ToList();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync(); // Burada filtreleme yapılacak
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {

            return await _dbSet.FirstOrDefaultAsync(expression);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }

        void IRepository<T>.Decrease(int id, Expression<Func<T, int>> propertySelector, int amount)
        {
            var entity = _dbSet.Find(id);
            var propertyInfo = (propertySelector.Body as MemberExpression)?.Member as System.Reflection.PropertyInfo;
            var currentValue = (int)propertyInfo.GetValue(entity);
            var updatedValue = currentValue - amount;

            if (updatedValue < 0)
            {
                throw new InvalidOperationException("Property value cannot be less than zero.");
            }

            propertyInfo.SetValue(entity, updatedValue);

            _context.Update(entity);
            _context.SaveChanges();
        }

        async Task IRepository<T>.DecreaseAsync(int id, Expression<Func<T, int>> propertySelector, int amount)
        {
            var entity = await _dbSet.FindAsync(id);
            
            var propertyInfo = (propertySelector.Body as MemberExpression)?.Member as System.Reflection.PropertyInfo;

            var currentValue = (int)propertyInfo.GetValue(entity);
            var updatedValue = currentValue - amount;

            if (updatedValue < 0)
            {
                throw new InvalidOperationException("Property value cannot be less than zero.");
            }
            propertyInfo.SetValue(entity, updatedValue);

            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        

        void IRepository<T>.Increase(int id, Expression<Func<T, int>> propertySelector, int amount)
        {
            var entity = _dbSet.Find(id);
            var propertyInfo = (propertySelector.Body as MemberExpression)?.Member as System.Reflection.PropertyInfo;

            var currentValue = (int)propertyInfo.GetValue(entity);
            var updatedValue = currentValue + amount;

            propertyInfo.SetValue(entity, updatedValue);

            _context.Update(entity);
            _context.SaveChanges();
        }

        async Task IRepository<T>.IncreaseAsync(int id, Expression<Func<T, int>> propertySelector, int amount)
        {
            var entity = await _dbSet.FindAsync(id);

            var propertyInfo = (propertySelector.Body as MemberExpression)?.Member as System.Reflection.PropertyInfo;

            var currentValue = (int)propertyInfo.GetValue(entity);
            var updatedValue = currentValue + amount;

            propertyInfo.SetValue(entity, updatedValue);

            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
