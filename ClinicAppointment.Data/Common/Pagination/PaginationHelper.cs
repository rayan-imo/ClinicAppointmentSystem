namespace ClinicAppointment.Data.Common.Pagination
{
    public static class PaginationHelper
    {
        public static async Task<PagedResult<T>> ToPagedAsync<T>(List<T> list, int pageNumber=1,int pageSize = 10)
        {
            var query = list.AsQueryable();
            int total = query.Count();
            int page = pageNumber ;
            int size = pageSize ;
            var items = query.Skip((page - 1) * size)
                .Take(size)
                .ToList();
            return new PagedResult<T>(items, page, size, total);
        }
    }
}
