using System;
using System.Threading.Tasks;
using Cooperchip.ITDeveloper.Application.Interfaces;
using Cooperchip.ITDeveloper.Farmacia.Domain.Notificacoes;
using Cooperchip.ITDeveloper.Mvc.ServiceApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Cooperchip.ITDeveloper.Mvc.Controllers
{
    public class FornecedoresController : BaseController
    {

        private readonly IServicoAplicacaoFornecedor _appServiceFornecedor;

        public FornecedoresController(INotificador notificador,
                                      IServicoAplicacaoFornecedor appServiceFornecedor) : base(notificador)
        {
            _appServiceFornecedor = appServiceFornecedor;
        }

        [Route("lista-de-fornecedores")]
        public async Task<IActionResult> Index()
        {
            return View(await _appServiceFornecedor.ObterTodosApplication());
        }

        [Route("dados-do-fornecedor/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorEndereco(id);

            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            return View(fornecedorViewModel);
        }

        [Route("novo-fornecedor")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("novo-fornecedor")]
        [HttpPost]
        public async Task<IActionResult> Create(FornecedorViewModel fornecedorViewModel)
        {
            if (!ModelState.IsValid) return View(fornecedorViewModel);

            await _appServiceFornecedor.AdicionarApplication(fornecedorViewModel);

            // Antes de Gravar, verificar se a operação é válida, com Regras de Negósios.
            if (!OperacaoValida()) return View(fornecedorViewModel);

            return RedirectToAction("Index");
        }

        [Route("editar-fornecedor/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorProdutosEndereco(id);

            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            return View(fornecedorViewModel);
        }

        [Route("editar-fornecedor/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, FornecedorViewModel fornecedorViewModel)
        {
            if (id != fornecedorViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(fornecedorViewModel);

            await _appServiceFornecedor.AtualizarApplication(fornecedorViewModel);

            if (!OperacaoValida()) return View(await ObterFornecedorProdutosEndereco(id));

            return RedirectToAction("Index");
        }

        [Route("excluir-fornecedor/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorEndereco(id);

            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            return View(fornecedorViewModel);
        }

        [Route("excluir-fornecedor/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);

            if (fornecedor == null) return NotFound();

            await _appServiceFornecedor.RemoverApplication(id);

            if (!OperacaoValida()) return View(fornecedor);

            return RedirectToAction("Index");
        }

        [Route("obter-endereco-fornecedor/{id:guid}")]
        public async Task<IActionResult> ObterEndereco(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);

            if (fornecedor == null)
            {
                return NotFound();
            }

            return PartialView("_DetalhesEndereco", fornecedor);
        }

        [Route("atualizar-endereco-fornecedor/{id:guid}")]
        public async Task<IActionResult> AtualizarEndereco(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);

            if (fornecedor == null)
            {
                return NotFound();
            }

            return PartialView("_AtualizarEndereco", new FornecedorViewModel { Endereco = fornecedor.Endereco });
        }

        [Route("atualizar-endereco-fornecedor/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> AtualizarEndereco(FornecedorViewModel fornecedorViewModel)
        {
            ModelState.Remove("Nome");
            ModelState.Remove("Documento");

            if (!ModelState.IsValid) return PartialView("_AtualizarEndereco", fornecedorViewModel);

            await _appServiceFornecedor.AtualizarEnderecoApplication(fornecedorViewModel);

            if (!OperacaoValida()) return PartialView("_AtualizarEndereco", fornecedorViewModel);

            var url = Url.Action("ObterEndereco", "Fornecedores", new { id = fornecedorViewModel.Endereco.FornecedorId });
            return Json(new { success = true, url });
        }

        private async Task<FornecedorViewModel> ObterFornecedorEndereco(Guid id)
        {
            return await _appServiceFornecedor.ObterFornecedorEnderecoApplication(id);
        }

        private async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid id)
        {
            return await _appServiceFornecedor.ObterFornecedorProdutosEnderecoApplication(id);
        }
    }
}
