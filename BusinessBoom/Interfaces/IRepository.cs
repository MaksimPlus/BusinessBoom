using BusinessBoom.Models.Base;

namespace BusinessBoom.Interfaces
{
    public interface IRepository<T> where T : BaseModel
    {
        public T GetById(Guid id);
        public T Create(T model);
    }
}
