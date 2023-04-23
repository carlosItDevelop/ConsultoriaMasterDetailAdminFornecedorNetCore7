using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Cooperchip.ITDeveloper.Application.Interfaces;
using Cooperchip.ITDeveloper.Farmacia.Domain.Entities;
using Cooperchip.ITDeveloper.Farmacia.Domain.Interfaces;
using Cooperchip.ITDeveloper.Mvc.ServiceApp.ViewModels;

namespace Cooperchip.ITDeveloper.Application.Services
{
    public class ServicoAplicacaoFornecedor : IServicoAplicacaoFornecedor
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;

        public ServicoAplicacaoFornecedor(IFornecedorRepository fornecedorRepository,
                                           IFornecedorService fornecedorService,
                                           IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _fornecedorService = fornecedorService;
            _mapper = mapper;
        }

        // ========/ Leitura =========================================//
        public async Task<IEnumerable<RepresentanteLegalViewModel>> ObterTodosApplication()
        {
            return _mapper.Map<IEnumerable<RepresentanteLegalViewModel>>(await _fornecedorRepository.ObterTodos());
        }
        public async Task<RepresentanteLegalViewModel> ObterFornecedorEnderecoApplication(Guid id)
        {
            return _mapper.Map<RepresentanteLegalViewModel>(await _fornecedorRepository.ObterFornecedorEndereco(id));
        }
        public async Task<RepresentanteLegalViewModel> ObterFornecedorProdutosEnderecoApplication(Guid id)
        {
            return _mapper.Map<RepresentanteLegalViewModel>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));
        }

        // ========/ Escrita =========================================//

        public async Task AdicionarApplication(RepresentanteLegalViewModel fvm)
        {
            await _fornecedorService.Adicionar(_mapper.Map<RepresentanteLegal>(fvm));
        }
        public async Task AtualizarApplication(RepresentanteLegalViewModel fvm)
        {
            await _fornecedorService.Atualizar(_mapper.Map<RepresentanteLegal>(fvm));
        }
        public async Task RemoverApplication(Guid id)
        {
            await _fornecedorService.Remover(id);
        }
        public async Task AtualizarEnderecoApplication(RepresentanteLegalViewModel fvm)
        {
            await _fornecedorService.AtualizarEndereco(_mapper.Map<RepresentanteLegal>(fvm.RepresentanteLegal));
        }

    }
}
