using MeuAtendimento.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeuAtendimento.Infra.Data.Mappings
{
    public class AtendimentoMap : IEntityTypeConfiguration<Atendimento>
    {
        public void Configure(EntityTypeBuilder<Atendimento> builder)
        {
            builder.HasKey(atendimento => atendimento.ID);

            builder.Property(atendimento => atendimento.NumeroSequencial)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(atendimento => atendimento.DataHoraChegada)
                   .IsRequired();

            builder.Property(atendimento => atendimento.Status)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.HasIndex(a => a.PacienteID)
                   .HasName("IX_Atendimento_PacienteID");

            builder.HasOne(atendimento => atendimento.Paciente)
                   .WithMany(paciente => paciente.Atendimentos)
                   .HasForeignKey(atendimentos => atendimentos.PacienteID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}