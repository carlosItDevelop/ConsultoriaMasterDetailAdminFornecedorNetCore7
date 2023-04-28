using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Cooperchip.ITDeveloper.Farmacia.Domain.Entities;
using Cooperchip.ITDeveloper.Farmacia.Domain.Interfaces;
using Cooperchip.ITDeveloper.Farmacia.Domain.Notificacoes;
using Cooperchip.ITDeveloper.Mvc.ServiceApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Cooperchip.ITDeveloper.Mvc.Controllers
{
    public class FornecedoresController : BaseController
    {
        private readonly IEnderecoRepository _enderecoRepositorio;


        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;

        public FornecedoresController(INotificador notificador,
                                      IFornecedorRepository fornecedorRepository,
                                      IMapper mapper,
                                      IFornecedorService fornecedorService,
                                      IEnderecoRepository enderecoRepositorio) : base(notificador)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
            _fornecedorService = fornecedorService;
            _enderecoRepositorio = enderecoRepositorio;
        }

        [Route("lista-de-fornecedores")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos()));
        }

        [Route("dados-do-fornecedor/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var fornecedorViewModel = _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorRepresentanteLegal(id));

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

            await _fornecedorService.Adicionar(_mapper.Map<Fornecedor>(fornecedorViewModel));

            // Antes de Gravar, verificar se a operação é válida, com Regras de Negósios.
            if (!OperacaoValida()) return View(fornecedorViewModel);

            return RedirectToAction("Index");
        }



        [Route("editar-fornecedor/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var fornecedorViewModel =
                _mapper.Map<FornecedorViewModel>(await _fornecedorRepository
                .ObterFornecedorProdutosRepresentanteLegal(id));

            var enderecoViewModel = await PopularFornecedores(new EnderecoViewModel());

            fornecedorViewModel.FornecedorEEnderecoViewModel = new FornecedorEEnderecoViewModel(fornecedorViewModel, enderecoViewModel);

            if (fornecedorViewModel == null) return NotFound();            

            return View(fornecedorViewModel);
        }



        [Route("editar-fornecedor/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, FornecedorViewModel vModel)
        {

            vModel.FornecedorEEnderecoViewModel.EnderecoVModel = await PopularFornecedores(vModel.FornecedorEEnderecoViewModel.EnderecoVModel);

            if (id != vModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(vModel);

            await _fornecedorRepository.Atualizar(_mapper.Map<Fornecedor>(vModel));

            if (!OperacaoValida()) return View(_mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosRepresentanteLegal(id)));

            return RedirectToAction("Index");
        }

        [Route("excluir-fornecedor/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var fornecedorViewModel = _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorRepresentanteLegal(id));

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
            var fornecedor = _fornecedorRepository.ObterFornecedorProdutosRepresentanteLegal(id);

            if (fornecedor == null) return NotFound();

            await _fornecedorRepository.Remover(id);

            if (!OperacaoValida()) return View(fornecedor);

            return RedirectToAction("Index");
        }

        [Route("obter-representante-fornecedor/{id:guid}")]
        public async Task<IActionResult> ObterRepresentanteLegal(Guid id)
         {
            var fornecedorViewModel = _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosRepresentanteLegal(id));

            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            return PartialView("_DetalhesRepresentante", fornecedorViewModel);
        }

        // Todo: Não mostra o erro, mas no GoogleDevTools mostra um bloqueio de Id; pesquisar

        #region: Ajustar Lista de Enderecos em FornecedorController
        [Route("obter-listaendereco-fornecedor/{id:guid}")]
        public async Task<IActionResult> ObterListaEndereco(Guid id)
        {
            var fornecedor = _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorRepresentanteLegal(id));

            if (fornecedor == null)
            {
                return NotFound();
            }

            return PartialView("_ListaEnderecos", new FornecedorViewModel { Enderecos = fornecedor.Enderecos });
        }
        #endregion

        [Route("atualizar-representante-fornecedor/{id:guid}")]
        public async Task<IActionResult> AtualizarRepresentanteLegal(Guid id)
        {
            var fornecedor = _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorRepresentanteLegal(id));

            if (fornecedor == null)
            {
                return NotFound();
            }

            return PartialView("_AtualizarRepresentante", new FornecedorViewModel { RepresentanteLegal = fornecedor.RepresentanteLegal });
        }

        [Route("atualizar-representante-fornecedor/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> AtualizarRepresentanteLegal(FornecedorViewModel fornecedorViewModel)
        {
            ModelState.Remove("Nome");
            ModelState.Remove("Documento");

            if (!ModelState.IsValid) return PartialView("_AtualizarRepresentante", fornecedorViewModel);

            if (fornecedorViewModel.RepresentanteLegal == null)
            {
                Console.WriteLine($"\n\n\nfornecedorViewModel.RepresentanteLegal é nulo");
                return PartialView("_AtualizarRepresentante", fornecedorViewModel);
            }

            await _fornecedorService.AtualizarRepresentanteLegal(_mapper.Map<RepresentanteLegal>(fornecedorViewModel.RepresentanteLegal));

            if (!OperacaoValida()) {
                Console.WriteLine("Operáção Inválida");
                return PartialView("_AtualizarRepresentante", fornecedorViewModel);
            }

            var url = Url.Action("ObterRepresentanteLegal", "Fornecedores", new { id = fornecedorViewModel.RepresentanteLegal.FornecedorId });
            return Json(new { success = true, url });


        }


        /// <summary>
        /// Usado para popular a lista de Fornecedores para o DropDown
        /// de onde pode-se escolher de quel Fornecedor pertence o Endereco!
        /// </summary>
        /// <param name="enderecoViewModel"></param>
        /// <returns></returns>
        private async Task<EnderecoViewModel> PopularFornecedores(EnderecoViewModel enderecoViewModel)
        {
            enderecoViewModel.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            return enderecoViewModel;
        }


    }

}
