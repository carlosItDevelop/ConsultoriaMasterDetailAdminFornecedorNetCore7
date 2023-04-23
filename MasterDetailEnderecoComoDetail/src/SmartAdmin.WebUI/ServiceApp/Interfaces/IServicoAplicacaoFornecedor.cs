using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cooperchip.ITDeveloper.Mvc.ServiceApp.ViewModels;

namespace Cooperchip.ITDeveloper.Application.Interfaces
{
    public interface IServicoAplicacaoFornecedor
    {

        // ========/ Leitura =========================================//
        Task<IEnumerable<RepresentanteLegalViewModel>> ObterTodosApplication();
        Task<RepresentanteLegalViewModel> ObterFornecedorEnderecoApplication(Guid id);
        Task<RepresentanteLegalViewModel> ObterFornecedorProdutosEnderecoApplication(Guid id);

        // ========/ Escrita =========================================//
        Task AdicionarApplication(RepresentanteLegalViewModel fvm);
        Task AtualizarApplication(RepresentanteLegalViewModel fvm);
        Task RemoverApplication(Guid id);
        Task AtualizarEnderecoApplication(RepresentanteLegalViewModel fvm);
    }
}
