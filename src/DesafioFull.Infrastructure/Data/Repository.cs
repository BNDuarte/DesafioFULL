using DesafioFull.Domain;
using DesafioFull.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioFull.Infrastructure.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly ApplicationDbContext _contexto;

        public Repository(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _contexto.Remove(entity);
            await _contexto.SaveChangesAsync();
        }

        public async Task<TEntity> GetAsync(int id, params string[] includes) => await _contexto.Set<TEntity>().Include(includes).AsQueryable().FirstOrDefaultAsync(e => e.Id == id);

        public IQueryable<TEntity> GetAll(params string[] includes)
        {
            return _contexto.Set<TEntity>().Include(includes);
        }


        public async Task SaveAsync(TEntity entity)
        {
            var entidadeAntiga = _contexto.Set<TEntity>().Find(entity.Id);

            if (entidadeAntiga == null)
            {
                _contexto.Add(entity);
            }
            else
            {
                _contexto.Entry(entidadeAntiga).State = EntityState.Detached;
                _contexto.Update(entity);
            }
            await _contexto.SaveChangesAsync();
        }
    }
}
