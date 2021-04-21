using DesafioFull.Domain.Excecoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioFull.Domain.Documentos
{
    public class CPF
    {
        private string _valor;
        public string Valor { get => FormatarValor(); protected set => _valor = value; }

        private string FormatarValor()
        {
            string valorSemFormatacao = RetirarFormatacao(_valor);
            if (!string.IsNullOrEmpty(valorSemFormatacao))
            {
                return valorSemFormatacao.Insert(3, ".").Insert(7, ".").Insert(11, "-");
            }
            else
            {
                return "";
            }
        }

        private string RetirarFormatacao(string valor)
        {
            return valor.Replace(".", "").Replace("-", "").Replace("/", "");
        }

        public CPF(string valor)
        {
            DomainException.When(!EhValido(valor), "CPF inválido.");
            _valor = valor;
        }

        protected CPF()
        {
        }

        public static readonly CPF Vazio = new CPF() { Valor = "" };

        protected bool EhValido(string valor)
        {
            DomainException.When(string.IsNullOrEmpty(valor), "O CPF é obrigatório.");

            var invalidos = new string[]
            {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            };

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            var cpf = valor.Trim();
            cpf = RetirarFormatacao(cpf);

            if (Array.IndexOf(invalidos, cpf) != -1)
            {
                return false;
            }
            if (cpf.Length != 11)
            {
                return false;
            }

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        public static implicit operator CPF(string valor) => new CPF(valor);

        public static implicit operator string(CPF cpf) => cpf.Valor;
    }
}