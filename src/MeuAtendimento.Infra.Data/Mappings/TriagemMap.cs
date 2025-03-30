using MeuAtendimento.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeuAtendimento.Infra.Data.Mappings
{
    public class TriagemMap : IEntityTypeConfiguration<Triagem>
    {
        public void Configure(EntityTypeBuilder<Triagem> builder)
        {
            builder.HasKey(triagem => triagem.ID);

            builder.Property(triagem => triagem.Sintomas)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(triagem => triagem.PressaoArterial)
                   .HasMaxLength(20);

            builder.Property(triagem => triagem.Peso)
                   .HasColumnType("decimal(5,2)")
                   .IsRequired();

            builder.Property(triagem => triagem.Altura)
                   .HasColumnType("decimal(4,2)")
                   .IsRequired();

            builder.HasIndex(triagem => triagem.AtendimentoID)
                   .HasName("IX_Triagem_AtendimentoID");

            builder.HasIndex(triagem => triagem.EspecialidadeID)
                   .HasName("IX_Triagem_EspecialidadeID");

            builder.HasOne(triagem => triagem.Atendimento)
                   .WithOne(atendimento => atendimento.Triagem)
                   .HasForeignKey<Triagem>(triagem => triagem.AtendimentoID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(triagem => triagem.Especialidade)
                   .WithMany(especialidade => especialidade.Triagens)
                   .HasForeignKey(triagem => triagem.EspecialidadeID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}