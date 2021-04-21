using DesafioFull.Domain.Documentos;
using DesafioFull.Domain.Excecoes;
using DesafioFull.Domain.Parcelas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesafioFull.Domain.Titulos
{
    public class Titulo : Entity
    {
        private int _numeroTitulo;
        private string _nomeDevedor;
        private CPF _documentoCpf;
        private decimal _porcentagemJuros;
        private decimal _porcentagemMulta;
        private readonly List<Parcela> _parcelas = new List<Parcela>();

        public int NumeroTitulo
        {
            get => _numeroTitulo;
            set
            {
                DomainException.When(value == 0, "O número do título não pode ser igual a zero.");
                _numeroTitulo = value;
            }
        }

        public string NomeDevedor
        {
            get => _nomeDevedor;
            set
            {
                DomainException.When(string.IsNullOrEmpty(value), "O nome do devedor é obrigatório.");
                _nomeDevedor = value.Trim();
            }
        }

        public string CPF
        {
            get => _documentoCpf;
            set
            {
                DomainException.When(string.IsNullOrEmpty(value), "O CPF é obrigatório.");
                _documentoCpf = value.Trim();
            }
        }

        public decimal PorcentagemJuros
        {
            get => _porcentagemJuros;
            set
            {
                _porcentagemJuros = value;
            }
        }

        public decimal PorcentagemMulta
        {
            get => _porcentagemMulta;
            set
            {
                _porcentagemMulta = value;
            }
        }

        #region Parcelas
        public ICollection<Parcela> Parcelas => _parcelas;

        public void AdicionarParcela(Parcela parcela)
        {
            DomainException.When(parcela == null, "A parcela não deve ser vazia.");

            parcela.Titulo = this;
            _parcelas.Add(parcela);
        }

        public void AtualizarParcelas(IEnumerable<Parcela> parcelas)
        {
            foreach (var parcela in parcelas.ToList())
            {
                if (!_parcelas.Any(d => d.Id == parcela.Id) || parcela.Id == 0)
                {
                    AdicionarParcela(parcela);
                }
                else
                {
                    Parcela parcelaSalva = _parcelas.ToList().FirstOrDefault(s => s.Id == parcela.Id);
                    if (parcelaSalva != null)
                    {
                        parcelaSalva.Id = parcela.Id;
                        parcelaSalva.NumeroParcela = parcela.NumeroParcela;
                        parcelaSalva.DataVencimento = parcela.DataVencimento;
                        parcelaSalva.ValorParcela = parcela.ValorParcela;
                    }
                }
            }

            var parcelasRemovidas = new List<Parcela>();

            foreach (var parcela in _parcelas)
            {
                if (!parcelas.Any(d => d.Id == parcela.Id))
                {
                    parcelasRemovidas.Add(parcela);
                }
            }

            foreach (var parcela in parcelasRemovidas)
            {
                _parcelas.Remove(parcela);
            }
        }

        public decimal CalculaValorTotal()
        {
            return Parcelas.Sum(p => p.ValorParcela);
        }

        public decimal CalculaValorComJuros()
        {
            decimal valorTotal = CalculaValorTotal();
            decimal juros = 0;

            List<Parcela> parcelasAtrasadas = _parcelas.Where(p => p.DataVencimento.Date < DateTime.Now.Date).ToList();

            foreach (var parcela in parcelasAtrasadas ?? Enumerable.Empty<Parcela>())
            {
                int dias = (DateTime.Now.Date - parcela.DataVencimento.Date).Days;
                juros += Convert.ToDecimal((PorcentagemJuros / 100) / 30) * dias * parcela.ValorParcela;
            }

            return parcelasAtrasadas.Count() > 1 ? valorTotal + (valorTotal * (PorcentagemMulta / 100)) + juros : valorTotal;
        }

        public int DiasAtraso()
        {
            int diasAtraso = 0;
            List<Parcela> parcelasAtrasadas = _parcelas.Where(p => p.DataVencimento.Date < DateTime.Now.Date).ToList();

            foreach (var parcela in parcelasAtrasadas ?? Enumerable.Empty<Parcela>())
            {
                int dias = (DateTime.Now.Date - parcela.DataVencimento.Date).Days;
                diasAtraso = diasAtraso < dias ? dias : diasAtraso;
            }

            return diasAtraso;
        }
        #endregion
    }
}
