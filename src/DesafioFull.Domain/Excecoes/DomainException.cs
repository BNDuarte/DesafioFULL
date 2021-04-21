using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioFull.Domain.Excecoes
{
    public class DomainException : Exception
    {
        public DomainException(string mensagem) : base(mensagem) { }

        public static void When(bool condicao, string mensagem)
        {
            if (condicao)
            {
                throw new DomainException(mensagem);
            }
        }
    }
}
