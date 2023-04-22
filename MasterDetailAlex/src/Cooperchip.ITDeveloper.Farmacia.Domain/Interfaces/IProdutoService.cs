using Cooperchip.ITDeveloper.Farmacia.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Cooperchip.ITDeveloper.Farmacia.Domain.Interfaces
{
    public interface IProdutoService : IDisposable
    {
        Task Adicionar(Produto produto);
        Task Atualizar(Produto produto);
        Task Remover(Guid id);
    }
}