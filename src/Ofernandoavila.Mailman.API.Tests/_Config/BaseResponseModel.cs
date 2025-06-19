namespace Ofernandoavila.Mailman.API.Tests._Config
{
    public class BaseResponseModel
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
