using Cooperchip.ITDeveloper.Farmacia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cooperchip.ITDeveloper.Farmacia.InfraData.Mappings
{
    public class RepresentanteLegalMapping : IEntityTypeConfiguration<RepresentanteLegal>
    {
        public void Configure(EntityTypeBuilder<RepresentanteLegal> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome)
                .IsRequired().HasColumnType("varchar(50)");
            builder.Property(x => x.SobreNome)
                 .IsRequired().HasColumnType("varchar(50)");
            builder.Property(x => x.Documento)
                 .IsRequired().HasColumnType("varchar(14)");
            builder.Property(x => x.Email)
                 .HasColumnType("varchar(255)");
            builder.Property(x => x.Telefone).IsRequired().HasColumnType("varchar(13)");

            // 1 : 1 => Representante : Fornecedor
            builder.HasOne(f => f.Fornecedor)
                .WithOne(e => e.RepresentanteLegal);


            builder.ToTable("RepresentanteLegal");

        }
    }
}
