using System;
using System.Linq;
using System.Threading.Tasks;
using Cooperchip.ITDeveloper.Farmacia.Domain.Entities;
using Cooperchip.ITDeveloper.Farmacia.Domain.Entities.Validations;
using Cooperchip.ITDeveloper.Farmacia.Domain.Interfaces;
using Cooperchip.ITDeveloper.Farmacia.Domain.Notificacoes;

namespace Cooperchip.ITDeveloper.Farmacia.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IRepresentanteRepository _resentanteRepository;

        public FornecedorService(
            IFornecedorRepository fornecedorRepository, 
            IRepresentanteRepository resentanteRepository,
            INotificador notificador) : base(notificador)
        {
            _fornecedorRepository = fornecedorRepository;
            _resentanteRepository = resentanteRepository;
        }

        public async Task Adicionar(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor) 
                || !ExecutarValidacao(new RepresentanteLegalValidation(), fornecedor.RepresentanteLegal)) return;

            if (_fornecedorRepository.Buscar(f => f.Documento == fornecedor.Documento).Result.Any())
            {
                Notificar("Já existe um fornecedor com este documento infomado.");
                return;
            }

            await _fornecedorRepository.Adicionar(fornecedor);
        }

        public async Task Atualizar(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)) return;

            if (_fornecedorRepository.Buscar(f => f.Documento == fornecedor.Documento && f.Id != fornecedor.Id).Result.Any())
            {
                Notificar("Já existe um fornecedor com este documento infomado.");
                return;
            }

            await _fornecedorRepository.Atualizar(fornecedor);
        }

        public async Task AtualizarRepresentanteLegal(RepresentanteLegal representante)
        {
            if (!ExecutarValidacao(new RepresentanteLegalValidation(), representante)) return;

            await _resentanteRepository.Atualizar(representante);
        }

        public async Task Remover(Guid id)
        {
            if (_fornecedorRepository.ObterFornecedorProdutosRepresentanteLegal(id).Result.Enderecos.Any())
            {
                Notificar("O fornecedor possui Endereços cadastrados!");
                return;
            }

            var resentante = await _resentanteRepository.ObterRepresentantePorFornecedor(id);

            if (resentante != null)
            {
                await _resentanteRepository.Remover(resentante.Id);
            }

            await _fornecedorRepository.Remover(id);
        }

        public void Dispose()
        {
            _fornecedorRepository?.Dispose();
            _resentanteRepository?.Dispose();
        }
    }
}
