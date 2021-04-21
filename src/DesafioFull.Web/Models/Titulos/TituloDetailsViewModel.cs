using DesafioFull.Domain.Titulos;
using DesafioFull.Web.Models.Parcelas;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DesafioFull.Web.Models.Titulos
{
    public class TituloDetailsViewModel
    {
        public TituloDetailsViewModel() { }
        public TituloDetailsViewModel(Titulo titulo)
        {
            Id = titulo.Id;
            NumeroTitulo = titulo.NumeroTitulo;
            NomeDevedor = titulo.NomeDevedor;
            QuantidadeParcelas = titulo.Parcelas.Count();
            ValorTitulo = titulo.CalculaValorTotal();
            DiasAtraso = titulo.DiasAtraso();
            ValorTituloAtualizado = titulo.CalculaValorComJuros();
            Parcelas = titulo.Parcelas?.Select(p => new ParcelaDetailsViewModel(p));
        }

        public int Id { get; set; }

        [Display(Name = "Título")]
        public int NumeroTitulo { get; set; }
        [Display(Name = "Devedor")]
        public string NomeDevedor { get; set; }
        [Display(Name = "Quantidade de parcelas")]
        public int QuantidadeParcelas { get; set; }

        [Display(Name = "Valor original")]
        public decimal ValorTitulo { get; set; }

        [Display(Name = "Dias atraso")]
        public int DiasAtraso { get; set; }
        [Display(Name = "Valor atualizado")]
        public decimal ValorTituloAtualizado { get; set; }

        public IEnumerable<ParcelaDetailsViewModel> Parcelas { get; set; }
    }
}
