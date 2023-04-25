using Cooperchip.ITDeveloper.Farmacia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cooperchip.ITDeveloper.Farmacia.InfraData.Mappings
{
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Documento)
                .IsRequired()
                .HasColumnType("varchar(14)");


            // 1 : 1 => Fornecedor : Repreentante
            builder.HasOne(f => f.RepresentanteLegal)
                .WithOne(e => e.Fornecedor);

            // 1 : N => Fornecedor : Endereco
            builder.HasMany(f => f.Enderecos)
                .WithOne(p => p.Fornecedor)
                .HasForeignKey(p => p.FornecedorId);

            builder.ToTable("Fornecedores");
        }
    }
}
