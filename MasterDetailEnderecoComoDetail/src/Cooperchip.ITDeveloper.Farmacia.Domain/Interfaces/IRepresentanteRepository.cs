using Cooperchip.ITDeveloper.Farmacia.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Cooperchip.ITDeveloper.Farmacia.Domain.Interfaces
{
    public interface IRepresentanteRepository : IRepository<RepresentanteLegal>
    {
        Task<RepresentanteLegal> ObterRepresentantePorFornecedor(Guid fornecedorId);
    }
}
