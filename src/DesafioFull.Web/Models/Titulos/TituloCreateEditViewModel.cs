using DesafioFull.Domain.Titulos;
using DesafioFull.Web.Models.Parcelas;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DesafioFull.Web.Models.Titulos
{
    public class TituloCreateEditViewModel
    {
        public TituloCreateEditViewModel() { }
        public TituloCreateEditViewModel(Titulo titulo)
        {
            Id = titulo.Id;
            Titulo = titulo.NumeroTitulo;
            NomeDevedor = titulo.NomeDevedor;
            CPF = titulo.CPF;
            PorcentagemMulta = titulo.PorcentagemMulta;
            PorcentagemJuros = titulo.PorcentagemJuros;
            Parcelas = titulo.Parcelas.Select(p => new ParcelaCreateEditViewModel(p));
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "O campo título é obrigatório.")]
        [Display(Name = "Título", Prompt = "Número do título")]
        public int? Titulo { get; set; } = null;

        [Required(ErrorMessage = "O campo devedor é obrigatório.")]
        [Display(Name = "Devedor", Prompt = "Nome do devedor")]
        public string NomeDevedor { get; set; }

        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        [Display(Name = "CPF", Prompt = "CPF do devedor")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "A porcentagem de juros é obrigatório.")]
        [Display(Name = "Porcentagem de juros", Prompt = "0,00%")]
        public decimal? PorcentagemJuros { get; set; } = null;

        [Required(ErrorMessage = "A porcentagem de multa é obrigatório.")]
        [Display(Name = "Porcentagem de multa", Prompt = "0,00%")]
        public decimal? PorcentagemMulta { get; set; } = null;

        [Required(ErrorMessage = "Deve existir ao menos umas parcela"), MinLength(1)]
        public IEnumerable<ParcelaCreateEditViewModel> Parcelas { get; set; }
    }
}
