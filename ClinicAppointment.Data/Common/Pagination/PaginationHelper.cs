namespace ClinicAppointment.Data.Common.Pagination
{
    public static class PaginationHelper
    {
        public static async Task<PagedResult<T>> ToPagedAsync<T>(List<T> list, int? pageNumber, int? pageSize)
        {
            var query = list.AsQueryable();
            int total = query.Count();
            int page = pageNumber ?? 1;
            int size = pageSize ?? (total == 0 ? 1 : total);
            var items = query.Skip((page - 1) * size)
                .Take(size)
                .ToList();
            return new PagedResult<T>(items, page, size, total);
        }
    }
}
