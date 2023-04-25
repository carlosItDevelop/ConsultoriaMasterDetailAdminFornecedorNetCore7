 using System;

namespace Cooperchip.ITDeveloper.Farmacia.Domain.Entities
{
    public class RepresentanteLegal : EntityBase
    {
        public Guid FornecedorId { get; set; }

        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        /* EF Relation */
        public Fornecedor Fornecedor { get; set; }

    }
}
