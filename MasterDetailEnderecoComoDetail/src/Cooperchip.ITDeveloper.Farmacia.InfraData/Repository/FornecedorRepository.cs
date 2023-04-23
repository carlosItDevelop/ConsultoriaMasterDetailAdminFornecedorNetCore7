using System;
using System.Threading.Tasks;
using Cooperchip.ITDeveloper.Farmacia.Domain.Entities;
using Cooperchip.ITDeveloper.Farmacia.Domain.Interfaces;
using Cooperchip.ITDeveloper.Farmacia.InfraData.Context;
using Microsoft.EntityFrameworkCore;

namespace Cooperchip.ITDeveloper.Farmacia.InfraData.Repository
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Fornecedor> ObterFornecedorEndereco(Guid id)
        {
            return await Db.Fornecedores.AsNoTracking()
                .Include(c => c.RepresentanteLegal)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id)
        {
            return await Db.Fornecedores.AsNoTracking()
                .Include(c => c.Produtos)
                .Include(c => c.RepresentanteLegal)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
