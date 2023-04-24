using System;
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

        public async Task<Fornecedor> ObterFornecedorEndereco(Guid id)
        {
            return await Db.Fornecedores.AsNoTracking()
                .Include(c => c.RepresentanteLegal)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id)
        {
            var fornecedor = await Db.Fornecedores
                .Include(c => c.RepresentanteLegal)
                .Include(c => c.Produtos).AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            //var fornecedor = Db.Fornecedores.FirstOrDefaultAsync(c => c.Id == id).Result;
            //var produtos = Db.Produtos.Where(c => c.FornecedorId == id).ToListAsync().Result;
            //var representante = Db.RepresentantesLegais.FirstOrDefaultAsync(c => c.FornecedorId == id).Result;


            //if (fornecedor != null)
            //{
            //    fornecedor.AdicionarRepresentante(representante);
            //    fornecedor.AdicionarListaDeProdutos(produtos);
            //}


            return await Task.FromResult(fornecedor);
        }
    }
}
