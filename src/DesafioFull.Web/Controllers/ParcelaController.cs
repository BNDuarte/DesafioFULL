using DesafioFull.Web.Models.Parcelas;
using Microsoft.AspNetCore.Mvc;

namespace DesafioFull.Web.Controllers
{
    public class ParcelaController : Controller
    {
        public IActionResult AdicionarParcela([Bind("Id,NumeroParcela,DataVencimento,ValorParcela")] ParcelaCreateEditViewModel parcela)
        {
            return PartialView("~/Views/Parcela/_LinhaTabelaParcela.cshtml", parcela);
        }
    }
}
