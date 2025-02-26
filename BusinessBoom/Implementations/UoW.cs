using BusinessBoom.Data;
using BusinessBoom.Interfaces;
using BusinessBoom.Models;

namespace BusinessBoom.Implementations
{
    public class UoW : IUoW, IDisposable
    {
        private ApplicationContext _context;
        public UoW(ApplicationContext context)
        {
            _context = context;
            Repository = new Repository<User>(_context); 
        }
        public IRepository<User> Repository { get; }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
