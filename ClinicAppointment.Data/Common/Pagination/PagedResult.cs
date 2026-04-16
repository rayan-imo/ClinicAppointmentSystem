namespace ClinicAppointment.Data.Common.Pagination
{

    public record PagedResult<T>(List<T> Items, int PageNumber, int PageSize, int TotalCount);
}
