using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Ofernandoavila.Mailman.Api.Extensions;

[ExcludeFromCodeCoverage]
public class NotEmptyAttribute : ValidationAttribute
{
    public NotEmptyAttribute() : base() {}

    public override bool IsValid(object value)
    {
        if(value is null) return true;

        return value switch
        {
            Guid guid => guid != Guid.Empty,
            _ => true
        };
    }
}