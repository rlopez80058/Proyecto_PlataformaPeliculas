using Proyecto_PlataformaPeliculas.Models;

namespace Proyecto_PlataformaPeliculas.Services
{
    public interface IDashboardService
    {
        Task<DashboardViewModel?> GetDashboardAsync();
    }
}