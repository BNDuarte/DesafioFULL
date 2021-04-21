using DesafioFull.Domain.Parcelas;
using System;
using System.ComponentModel.DataAnnotations;

namespace DesafioFull.Web.Models.Parcelas
{
    public class ParcelaCreateEditViewModel
    {
        public ParcelaCreateEditViewModel() { }
        public ParcelaCreateEditViewModel(Parcela parcela)
        {
            Id = parcela.Id;
            NumeroParcela = parcela.NumeroParcela;
            DataVencimento = parcela.DataVencimento.Date;
            ValorParcela = parcela.ValorParcela;
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "O campo número da parcela é obrigatório.")]
        [Display(Name = "Número da parcela", Prompt = "Ex: 1")]
        public int? NumeroParcela { get; set; } = null;

        [Required(ErrorMessage = "O campo data de vencimento é obrigatório.")]
        [Display(Name = "Data de vencimento")]
        [DataType(DataType.Date)]
        public DateTime DataVencimento { get; set; } = DateTime.Now.Date;

        [Required(ErrorMessage = "O campo valor da parcela é obrigatório.")]
        [Display(Name = "Valor da parcela", Prompt = "100,00")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal? ValorParcela { get; set; } = null;
    }
}
