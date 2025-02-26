using BusinessBoom.Data;
using BusinessBoom.Interfaces;
using BusinessBoom.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace BusinessBoom.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private DbSet<T> _objectSet;
        public Repository(ApplicationContext context)
        {
            _objectSet = context.Set<T>();
        }
        public T Create(T model)
        {
            _objectSet.Add(model);
            return model;
        }

        public T GetById(Guid id)
        {
            var model = _objectSet.FirstOrDefault(x => x.Id == id);
            if (model != null)
                return model;
            return null;
        }
    }
}
