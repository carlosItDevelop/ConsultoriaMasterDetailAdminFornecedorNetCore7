using Cooperchip.ITDeveloper.Farmacia.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Cooperchip.ITDeveloper.Farmacia.Domain.Interfaces
{
    public interface IEnderecoService : IDisposable
    {
        Task Adicionar(Endereco model);
        Task Atualizar(Endereco model);
        Task Remover(Guid id);
    }
}
