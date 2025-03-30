using MeuAtendimento.Domain.Core.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeuAtendimento.Domain.Models
{
    public class Paciente : Entity
    {
        #region Construtores Publicos

        public Paciente()
        {
        }

        public Paciente(string nome,
                        string telefone,
                        string sexo,
                        string email)
        {
            Nome = nome;
            Telefone = telefone;
            Sexo = sexo;
            Email = email;
        }

        #endregion Construtores Publicos

        #region Propriedades Publicas

        [Required, MaxLength(100)]
        public string Nome { get; set; }

        [Phone, MaxLength(20)]
        public string Telefone { get; set; }

        [MaxLength(10)]
        public string Sexo { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public virtual ICollection<Atendimento> Atendimentos { get; set; }

        #endregion Propriedades Publicas
    }
}