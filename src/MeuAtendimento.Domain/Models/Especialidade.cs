using MeuAtendimento.Domain.Core.Models;
using System.Collections.Generic;

namespace MeuAtendimento.Domain.Models
{
    public class Especialidade : Entity
    {
        #region Construtores Publicos

        public Especialidade()
        {
        }

        public Especialidade(string nome)
        {
            Nome = nome;
        }

        #endregion Construtores Publicos

        #region Propriedades Publicas

        public string Nome { get; set; }

        public virtual ICollection<Triagem> Triagens { get; set; }

        #endregion Propriedades Publicas
    }
}