using DesafioFull.Domain.Titulos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioFull.Services.Interfaces
{
    public interface ITituloService
    {
        Task<Titulo> GetAsync(int id);
        IEnumerable<Titulo> GetAll();
        Task SaveAsync(Titulo titulo);
        Task DeleteAsync(int id);
    }
}
