using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using MeuAtendimento.Domain.Models;

namespace MeuAtendimento.Infra.Data.Mappings
{
    public class PacienteMap : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.HasKey(paciente => paciente.ID);

            builder.Property(paciente => paciente.Nome)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(paciente => paciente.Telefone)
                   .HasMaxLength(20);

            builder.Property(paciente => paciente.Sexo)
                   .HasMaxLength(10);

            builder.Property(paciente => paciente.Email)
                   .HasMaxLength(255);

            builder.HasMany(paciente => paciente.Atendimentos)
                   .WithOne(atendimento => atendimento.Paciente)
                   .HasForeignKey(a => a.PacienteID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}