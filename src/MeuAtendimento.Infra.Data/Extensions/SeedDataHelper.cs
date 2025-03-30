using Microsoft.EntityFrameworkCore;

namespace MeuAtendimento.Infra.Data.Extensions
{
    public static class SeedDataHelper
    {
        #region Metodos Publicos

        public static ModelBuilder SeedData(this ModelBuilder builder)
        {
            return builder;
        }

        #endregion Metodos Publicos
    }
}