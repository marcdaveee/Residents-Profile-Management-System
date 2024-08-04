namespace RPMS.Models
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; set; }
        public int PageIndex{ get; set; }

        public int TotalPages { get; set; }

        public bool HasPrevPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public PaginatedList(List<T> items, int pageIndex, int totalPage)
        {
            Items = items;
            PageIndex = pageIndex;
            TotalPages = totalPage ;
        }
    }
}
