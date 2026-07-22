namespace KiaKooshar.Application.DTOs.Commons.Pagination
{
    public class PaginationResponseDTO<T>
    {
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T> ();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public long TotalCount { get; set; }
        public int TotalPages => (int) Math.Ceiling ((double) TotalCount / PageSize);
    }
}
