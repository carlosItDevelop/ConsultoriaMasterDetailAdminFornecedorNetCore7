using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Cooperchip.ITDeveloper.Farmacia.Domain.Entities;
using Cooperchip.ITDeveloper.Farmacia.Domain.Interfaces;
using Cooperchip.ITDeveloper.Farmacia.Domain.Notificacoes;
using Cooperchip.ITDeveloper.Mvc.ServiceApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooperchip.ITDeveloper.Mvc.Controllers
{
    public class EnderecosController : BaseController
    {

        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IEnderecoService _enderecoService;
        private readonly IMapper _mapper;

        public EnderecosController(INotificador notificador, IEnderecoRepository enderecoRepository,
            IFornecedorRepository fornecedorRepository, IEnderecoService enderecoService, IMapper mapper) : base(notificador)
        {
            _enderecoRepository = enderecoRepository;
            _fornecedorRepository = fornecedorRepository;
            _enderecoService = enderecoService;
            _mapper = mapper;

        }

        [Route("lista-de-enderecos")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<EnderecoViewModel>>(await _enderecoRepository.ObterEnderecosFornecedores()));
        }

        [Route("dados-do-endereco/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {

            var enderecoViewModel = _mapper.Map<EnderecoViewModel>(await _enderecoRepository.ObterEnderecoFornecedor(id));
            enderecoViewModel.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());

            if (enderecoViewModel == null)
            {
                return NotFound();
            }

            return View(enderecoViewModel);
        }


        #region: Usando PopularFornecedores
        [Route("novo-endereco")]
        public async Task<IActionResult> Create()
        {
            var enderecoViewModel = await PopularFornecedores(new EnderecoViewModel());

            return View(enderecoViewModel);
        }



        [Route("novo-endereco")]
        [HttpPost]
        public async Task<IActionResult> Create(EnderecoViewModel enderecoViewModel)
        {
            enderecoViewModel = await PopularFornecedores(enderecoViewModel);

            if (!ModelState.IsValid) return View(enderecoViewModel);

            await _enderecoService.Adicionar(_mapper.Map<Endereco>(enderecoViewModel));

            if (!OperacaoValida()) return View(enderecoViewModel);

            return RedirectToAction("Index");
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
        #endregion



        [Route("editar-endereco/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var enderecoViewModel = _mapper.Map<EnderecoViewModel>(await _enderecoRepository.ObterEnderecoFornecedor(id));

            if (enderecoViewModel == null)
            {
                return NotFound();
            }

            return View(enderecoViewModel);
        }

        [Route("editar-endereco/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, EnderecoViewModel enderecoViewModel)
        {
            if (id != enderecoViewModel.Id) return NotFound();

            var enderecoAtualizacao = _mapper.Map<EnderecoViewModel>(await _enderecoRepository.ObterEnderecoFornecedor(id));

            enderecoAtualizacao.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());

            enderecoViewModel.Fornecedor = enderecoAtualizacao.Fornecedor;

            if (!ModelState.IsValid) return View(enderecoViewModel);


            enderecoAtualizacao.Cep = enderecoViewModel.Cep;
            enderecoAtualizacao.Estado = enderecoViewModel.Estado;
            enderecoAtualizacao.Bairro = enderecoViewModel.Bairro;
            enderecoAtualizacao.Logradouro = enderecoViewModel.Logradouro;
            enderecoAtualizacao.Cidade = enderecoViewModel.Cidade;
            enderecoAtualizacao.Estado = enderecoViewModel.Estado;
            enderecoAtualizacao.Complemento = enderecoViewModel.Complemento;
            enderecoAtualizacao.Numero = enderecoViewModel.Numero;


            await _enderecoService.Atualizar(_mapper.Map<Endereco>(enderecoAtualizacao));

            if (!OperacaoValida()) return View(enderecoViewModel);

            return RedirectToAction("Index");
        }

        [Route("excluir-endereco/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var endereco = _mapper.Map<EnderecoViewModel>(await _enderecoRepository.ObterEnderecoFornecedor(id));
            endereco.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());

            if (endereco == null)
            {
                return NotFound();
            }

            return View(endereco);
        }


        [Route("excluir-endereco/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var enderecoViewModel = _mapper.Map<EnderecoViewModel>(await _enderecoRepository.ObterEnderecoFornecedor(id));

            enderecoViewModel.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());

            if (enderecoViewModel == null)
            {
                return NotFound();
            }

            await _enderecoService.Remover(id);

            if (!OperacaoValida()) return View(enderecoViewModel);

            TempData["Sucesso"] = "Endereco excluido com sucesso!";

            return RedirectToAction("Index");
        }
        
    }
}
