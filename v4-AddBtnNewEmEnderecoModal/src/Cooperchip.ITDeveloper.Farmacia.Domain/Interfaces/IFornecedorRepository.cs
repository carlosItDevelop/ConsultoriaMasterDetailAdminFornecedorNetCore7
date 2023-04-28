using Cooperchip.ITDeveloper.Farmacia.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cooperchip.ITDeveloper.Farmacia.Domain.Interfaces
{
    public interface IFornecedorRepository : IRepository<Fornecedor>
    {
        Task<Fornecedor> ObterFornecedorRepresentanteLegal(Guid id);
        Task<Fornecedor> ObterFornecedorProdutosRepresentanteLegal(Guid id);
        Task<IEnumerable<Endereco>> ObterListaEnderecoPorFornecedor(Guid id);       
    }
}
