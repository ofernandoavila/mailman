namespace Ofernandoavila.Mailman.Api.ViewModels.DTO
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; } = string.Empty;
        public string Search { get; set; }
        public bool? Active { get; set; }
        public bool Desc { get; set; } = false;

        public void SetPaginationDefaults(int totalRecords)
        {
            PageNumber = PageNumber == 0 ? 1 : PageNumber;
            PageSize = PageSize == 0 ? totalRecords : PageSize;
        }

        public void FilterForSelect()
        {
            Active = true;
        }
    }
}
