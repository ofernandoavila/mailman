using System.Diagnostics.CodeAnalysis;

namespace Ofernandoavila.Mailman.Business.Models.Settings;

[ExcludeFromCodeCoverage]
public class Notification(string message)
{
    public string Message { get; } = message;
}