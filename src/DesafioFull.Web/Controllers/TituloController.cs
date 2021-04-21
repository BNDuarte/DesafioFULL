using DesafioFull.Domain.Parcelas;
using DesafioFull.Domain.Titulos;
using DesafioFull.Services.Interfaces;
using DesafioFull.Web.Models.Parcelas;
using DesafioFull.Web.Models.Titulos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioFull.Web.Controllers
{
    public class TituloController : Controller
    {
        private readonly ITituloService _tituloService;

        public TituloController(ITituloService tituloService)
        {
            _tituloService = tituloService;
        }

        public ActionResult Create()
        {
            TituloCreateEditViewModel tituloVM = new TituloCreateEditViewModel();
            return View(tituloVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Id,Titulo,NomeDevedor,CPF,PorcentagemJuros,PorcentagemMulta,Parcelas")] TituloCreateEditViewModel tituloVM)
        {
            if (!ModelState.IsValid)
            {
                return View(tituloVM);
            }

            try
            {
                await SaveTitulo(tituloVM);
                return RedirectToAction(nameof(Index), "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(tituloVM);
            }
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Titulo titulo = await _tituloService.GetAsync(id.GetValueOrDefault());

            if (titulo == null)
            {
                return NotFound();
            }

            TituloCreateEditViewModel tituloVM = new TituloCreateEditViewModel(titulo);
            return View(tituloVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("Id,Titulo,NomeDevedor,CPF,PorcentagemJuros,PorcentagemMulta,Parcelas")] TituloCreateEditViewModel tituloVM)
        {
            if (!ModelState.IsValid)
            {
                return View(tituloVM);
            }

            try
            {
                await SaveTitulo(tituloVM);
                return RedirectToAction(nameof(Index), "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(tituloVM);
            }
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Titulo titulo = await _tituloService.GetAsync(id.GetValueOrDefault());

            if (titulo == null)
            {
                return NotFound();
            }

            TituloDetailsViewModel tituloVM = new TituloDetailsViewModel(titulo);
            return View(tituloVM);
        }

        //// GET: MercadoController1/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: MercadoController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _tituloService.DeleteAsync(id);
                return RedirectToAction(nameof(Index), "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }

        private async Task SaveTitulo(TituloCreateEditViewModel tituloVM)
        {
            Titulo titulo = await _tituloService.GetAsync(tituloVM.Id) ?? new Titulo();

            titulo.Id = tituloVM.Id;
            titulo.NumeroTitulo = tituloVM.Titulo.GetValueOrDefault();
            titulo.NomeDevedor = tituloVM.NomeDevedor;
            titulo.CPF = tituloVM.CPF;
            titulo.PorcentagemJuros = tituloVM.PorcentagemJuros.GetValueOrDefault();
            titulo.PorcentagemMulta = tituloVM.PorcentagemMulta.GetValueOrDefault();

            titulo.AtualizarParcelas(tituloVM.Parcelas?.Select(p => new Parcela()
            {
                Id = p.Id,
                NumeroParcela = p.NumeroParcela.GetValueOrDefault(),
                DataVencimento = p.DataVencimento.Date,
                ValorParcela = p.ValorParcela.GetValueOrDefault()
            }));

            await _tituloService.SaveAsync(titulo);
        }
    }
}