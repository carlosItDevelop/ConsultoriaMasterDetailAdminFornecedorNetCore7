using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Fornecedor> ObterFornecedorRepresentanteLegal(Guid id)
        {
            return await Db.Fornecedores.AsNoTracking()
                .Include(c => c.RepresentanteLegal)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Fornecedor> ObterFornecedorProdutosRepresentanteLegal(Guid id)
        {
            return await Db.Fornecedores.AsNoTracking()
                .Include(c => c.Enderecos)
                .Include(c => c.RepresentanteLegal)
                .FirstOrDefaultAsync(c => c.Id == id);            
        }

        public async Task<IEnumerable<Endereco>> ObterListaEnderecoPorFornecedor(Guid id)
        {
            return await Db.Enderecos
                .AsNoTracking()
                .Where(c => c.FornecedorId == id)
                .ToListAsync();
        }
    }
}
