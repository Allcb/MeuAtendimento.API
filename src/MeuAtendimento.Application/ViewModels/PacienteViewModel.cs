using MeuAtendimento.Domain.Core.Models;

namespace MeuAtendimento.Application.ViewModels
{
    public class PacienteViewModel : EntityViewModel
    {
        #region Propriedades Publicas

        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Sexo { get; set; }
        public string Email { get; set; }

        #endregion Propriedades Publicas
    }
}