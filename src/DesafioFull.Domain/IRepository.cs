using System.Linq;
using System.Threading.Tasks;

namespace DesafioFull.Domain
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> GetAsync(int id, params string[] includes);
        IQueryable<TEntity> GetAll(params string[] includes);
        Task SaveAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}