﻿using System.Threading.Tasks;
using Cooperchip.ITDeveloper.Farmacia.Domain.Notificacoes;
using Microsoft.AspNetCore.Mvc;

namespace Cooperchip.ITDeveloper.Mvc.Extesions.ViewComponents.Summary
{
    [ViewComponent(Name = "summary")]

    public class SummaryViewComponents : ViewComponent
    {
        private readonly INotificador _notificador;

        public SummaryViewComponents(INotificador notificador)
        {
            _notificador = notificador;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notificacoes = await Task.FromResult(_notificador.ObterNotificacoes());
            notificacoes.ForEach(c => ViewData.ModelState.AddModelError(string.Empty, c.Mensagem));

            return View(await Task.FromResult(notificacoes));
        }
    }
}
