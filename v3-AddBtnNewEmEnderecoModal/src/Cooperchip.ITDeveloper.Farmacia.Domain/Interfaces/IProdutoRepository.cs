using Cooperchip.ITDeveloper.Farmacia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cooperchip.ITDeveloper.Farmacia.Domain.Interfaces
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {
        Task<IEnumerable<Endereco>> ObterEnderecosPorFornecedor(Guid fornecedorId);
        Task<IEnumerable<Endereco>> ObterEnderecosFornecedores();
        Task<Endereco> ObterEnderecoFornecedor(Guid id);
    }
}
