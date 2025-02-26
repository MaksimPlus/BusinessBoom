using BusinessBoom.Implementations;
using BusinessBoom.Models;

namespace BusinessBoom.Interfaces
{
    public interface IUoW
    {
        IRepository<User> Repository { get; }
        void SaveChanges();
    }
}
