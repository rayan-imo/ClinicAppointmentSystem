namespace ClinicAppointment.Data.Common.Pagination
{

    public class PagedResult<T>(List<T> Items, int PageNumber, int PageSize, int TotalCount);
}
