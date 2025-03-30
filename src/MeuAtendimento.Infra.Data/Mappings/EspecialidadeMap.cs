using MeuAtendimento.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeuAtendimento.Infra.Data.Mappings
{
    public class EspecialidadeMap : IEntityTypeConfiguration<Especialidade>
    {
        public void Configure(EntityTypeBuilder<Especialidade> builder)
        {
            builder.HasKey(especialidade => especialidade.ID);

            builder.Property(triagem => triagem.Nome)
                   .IsRequired();

            builder.HasMany(especialidade => especialidade.Triagens)
                   .WithOne(triagem => triagem.Especialidade)
                   .HasForeignKey(triagem => triagem.EspecialidadeID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}