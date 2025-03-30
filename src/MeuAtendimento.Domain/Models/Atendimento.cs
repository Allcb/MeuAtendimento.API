using MeuAtendimento.Domain.Core.Models;
using System;

namespace MeuAtendimento.Domain.Models
{
    public class Atendimento : Entity
    {
        #region Construtores Publicos

        public Atendimento()
        {
        }

        public Atendimento(Guid pacienteID,
                           string numeroSequencial,
                           DateTime dataHoraChegada,
                           string status)
        {
            PacienteID = pacienteID;
            NumeroSequencial = numeroSequencial;
            DataHoraChegada = dataHoraChegada;
            Status = status;
        }

        #endregion Construtores Publicos

        #region Propriedades Publicas

        public Guid PacienteID { get; set; }
        public string NumeroSequencial { get; set; }
        public DateTime DataHoraChegada { get; set; }
        public string Status { get; set; }

        public virtual Paciente Paciente { get; set; }
        public virtual Triagem Triagem { get; set; }

        #endregion Propriedades Publicas
    }
}