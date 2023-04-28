using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooperchip.ITDeveloper.Farmacia.Domain.Entities;
using Cooperchip.ITDeveloper.Farmacia.Domain.Interfaces;
using Cooperchip.ITDeveloper.Farmacia.InfraData.Context;
using Microsoft.EntityFrameworkCore;

namespace Cooperchip.ITDeveloper.Farmacia.InfraData.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Endereco> ObterEnderecoFornecedor(Guid id)
        {
            return await Db.Enderecos.AsNoTracking().Include(f => f.Fornecedor)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Endereco>> ObterEnderecosFornecedores()
        {
            return await Db.Enderecos.AsNoTracking().Include(f => f.Fornecedor)
                .OrderBy(p => p.Logradouro).ToListAsync();
        }

        public async Task<IEnumerable<Endereco>> ObterEnderecosPorFornecedor(Guid fornecedorId)
        {
            return await Buscar(p => p.FornecedorId == fornecedorId);
        }
    }
}
