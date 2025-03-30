using System;

namespace MeuAtendimento.Domain.Core.Models
{
    public abstract class Entity
    {
        #region Construtores Protegidos

        protected Entity()
        {
            ID = Guid.NewGuid();
            IsDeleted = false;
            CreatedDate = DateTime.Now;
        }

        #endregion Construtores Protegidos

        #region Propriedades Publicas

        public Guid ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

        #endregion Propriedades Publicas
    }
}