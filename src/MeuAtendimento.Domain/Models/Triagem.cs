using MeuAtendimento.Domain.Core.Models;
using System;

namespace MeuAtendimento.Domain.Models
{
    public class Triagem : Entity
    {
        #region Construtores Publicos

        public Triagem()
        {
        }

        public Triagem(Guid atendimentoID,
                       string sintomas,
                       string pressaoArterial,
                       decimal peso,
                       decimal altura)
        {
            AtendimentoID = atendimentoID;
            Sintomas = sintomas;
            PressaoArterial = pressaoArterial;
            Peso = peso;
            Altura = altura;
        }

        #endregion Construtores Publicos

        #region Propriedades Publicas

        public Guid AtendimentoID { get; set; }
        public Guid EspecialidadeID { get; set; }
        public string Sintomas { get; set; }
        public string PressaoArterial { get; set; }
        public decimal Peso { get; set; }
        public decimal Altura { get; set; }

        public virtual Atendimento Atendimento { get; set; }
        public virtual Especialidade Especialidade { get; set; }

        #endregion Propriedades Publicas
    }
}