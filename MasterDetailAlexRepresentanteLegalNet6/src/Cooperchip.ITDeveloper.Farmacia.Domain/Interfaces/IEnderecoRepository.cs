using Cooperchip.ITDeveloper.Farmacia.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Cooperchip.ITDeveloper.Farmacia.Domain.Interfaces
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {
        Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId);
    }
}