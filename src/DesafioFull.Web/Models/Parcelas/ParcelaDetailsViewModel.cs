using DesafioFull.Domain.Parcelas;
using System.ComponentModel.DataAnnotations;

namespace DesafioFull.Web.Models.Parcelas
{
    public class ParcelaDetailsViewModel
    {
        public ParcelaDetailsViewModel() { }
        public ParcelaDetailsViewModel(Parcela parcela)
        {
            Id = parcela.Id;
            DataVencimento = parcela.DataVencimento.ToString("dd/MM/yyyy");
            ValorParcela = parcela.ValorParcela;
            numeroTitulo = parcela.NumeroParcela;
        }

        public int Id { get; set; }
        public int numeroTitulo { get; set; }
        public string DataVencimento { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal? ValorParcela { get; set; } = null;
    }
}
