using System.Text.RegularExpressions;

namespace EcoLink.WebApi.Middlewares;

public partial class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    #pragma warning disable CS8767
    public string TransformOutbound(object value)
        => (value is null ? null : Regex.Replace(value.ToString()!, "([a-z])([A-Z])", "$1-$2").ToLower())!;
}


