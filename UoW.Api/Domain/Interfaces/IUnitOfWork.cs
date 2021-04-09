using System.Threading.Tasks;

namespace UoW.Api.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}