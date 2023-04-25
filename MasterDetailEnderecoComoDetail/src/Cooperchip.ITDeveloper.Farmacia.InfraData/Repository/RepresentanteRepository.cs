using System;
using System.Threading.Tasks;
using Cooperchip.ITDeveloper.Farmacia.Domain.Entities;
using Cooperchip.ITDeveloper.Farmacia.Domain.Interfaces;
using Cooperchip.ITDeveloper.Farmacia.InfraData.Context;
using Microsoft.EntityFrameworkCore;

namespace Cooperchip.ITDeveloper.Farmacia.InfraData.Repository
{
    public class RepresentanteRepository : Repository<RepresentanteLegal>, IRepresentanteRepository
    {
        public RepresentanteRepository(ApplicationDbContext context) : base(context) { }

        public async Task<RepresentanteLegal> ObterRepresentantePorFornecedor(Guid fornecedorId)
        {
            return await Db.RepresentantesLegais.AsNoTracking()
                .FirstOrDefaultAsync(f => f.FornecedorId == fornecedorId);
        }
    }
}
