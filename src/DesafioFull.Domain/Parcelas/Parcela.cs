using DesafioFull.Domain.Excecoes;
using DesafioFull.Domain.Titulos;
using System;

namespace DesafioFull.Domain.Parcelas
{
    public class Parcela : Entity
    {
        private int _numeroParcela;
        private DateTime _dataVencimento;
        private decimal _valorParcela;

        public int NumeroParcela
        {
            get => _numeroParcela;
            set
            {
                DomainException.When(value == 0, "O número da parcela é obrigatório");
                _numeroParcela = value;
            }
        }

        public DateTime DataVencimento
        {
            get => _dataVencimento;
            set
            {
                _dataVencimento = value;
            }
        }

        public decimal ValorParcela
        {
            get => _valorParcela;
            set
            {
                DomainException.When(value == 0, "O número da parcela é obrigatório");
                _valorParcela = value;
            }
        }

        public int TituloId { get; set; }
        public Titulo Titulo { get; set; }

    }
}