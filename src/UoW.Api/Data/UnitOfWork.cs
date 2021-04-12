using System;
using System.Threading.Tasks;
using UoW.Api.Data.Repositories;
using UoW.Api.Domain.Interfaces;

namespace UoW.Api.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationContext _context;

        private IStudentRepository _studentRepository;
        private IClassRepository _classRepository;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }
        
        public IStudentRepository StudentRepository => 
            _studentRepository ??= new StudentRepository(_context);

        public IClassRepository ClassRepository =>
            _classRepository ??= new ClassRepository(_context);

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
