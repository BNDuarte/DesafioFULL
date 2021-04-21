using DesafioFull.Domain;
using DesafioFull.Domain.Excecoes;
using DesafioFull.Domain.Titulos;
using DesafioFull.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioFull.Services
{
    public class TituloService : ITituloService
    {
        private readonly IRepository<Titulo> _repository;

        public TituloService(IRepository<Titulo> repositoty)
        {
            _repository = repositoty;
        }

        public async Task<Titulo> GetAsync(int id)
        {
            return await _repository.GetAsync(id, nameof(Titulo.Parcelas));
        }

        public IEnumerable<Titulo> GetAll()
        {
            return _repository.GetAll(nameof(Titulo.Parcelas));
        }

        public async Task SaveAsync(Titulo titulo)
        {
            DomainException.When(VerificaNumeroTituloExiste(titulo), "O título já existe no sistema.");
            await _repository.SaveAsync(titulo);
        }

        private bool VerificaNumeroTituloExiste(Titulo titulo)
        {
            return _repository.GetAll().Any(t => t.NumeroTitulo == titulo.NumeroTitulo && t.Id != titulo.Id);
        }

        public async Task DeleteAsync(int id)
        {
            Titulo titulo = await GetAsync(id);
            if (titulo != null)
            {
                await _repository.DeleteAsync(titulo);
            }
        }
    }
}
