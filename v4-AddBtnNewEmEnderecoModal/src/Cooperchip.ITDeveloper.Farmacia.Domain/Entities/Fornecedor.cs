using System.Collections.Generic;

namespace Cooperchip.ITDeveloper.Farmacia.Domain.Entities
{
    public class Fornecedor : EntityBase
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        public TipoFornecedor TipoFornecedor { get; set; }

        public RepresentanteLegal RepresentanteLegal { get; set; }

        public bool Ativo { get; set; }

        /* EF Relations */
        public IEnumerable<Endereco> Enderecos { get; set; }         
    }
}
