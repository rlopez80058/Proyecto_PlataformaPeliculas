using Movie.Api.DTOs;

namespace Movie.Api.Services
{
    public interface IDashboardService
    {
        Task<DashboardDto> GetAsync();
    }
}