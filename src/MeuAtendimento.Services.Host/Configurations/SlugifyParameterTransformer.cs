using Microsoft.AspNetCore.Routing;
using System.Text.RegularExpressions;

namespace MeuAtendimento.API.Configurations
{
    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        #region Metodos Publicos

        public string TransformOutbound(object value)
        {
            if (value is null)
                return null;

            return Regex.Replace(input: value.ToString(),
                                 pattern: "([a-z])([A-Z])",
                                 replacement: "$1-$2")
                        .ToLower();
        }

        #endregion Metodos Publicos
    }
}