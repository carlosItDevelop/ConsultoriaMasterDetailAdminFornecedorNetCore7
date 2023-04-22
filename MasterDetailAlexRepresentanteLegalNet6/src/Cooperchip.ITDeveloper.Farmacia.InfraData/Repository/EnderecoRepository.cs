using System;
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

        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
        {
            return await Db.Enderecos.AsNoTracking()
                .FirstOrDefaultAsync(f => f.FornecedorId == fornecedorId);
        }
    }
}
