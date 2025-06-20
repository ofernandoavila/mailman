namespace Ofernandoavila.Mailman.Api.ViewModels.DTO
{
    public class Paged<T> where T : class
    {
        public int TotalRecords { get; set; }
        public IEnumerable<T> PagedData { get; set; }
    }
}
