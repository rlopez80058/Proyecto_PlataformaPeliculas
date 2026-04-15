using System.Net.Http.Json;
using Proyecto_PlataformaPeliculas.Models;

namespace Proyecto_PlataformaPeliculas.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly HttpClient _httpClient;

        public DashboardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DashboardViewModel?> GetDashboardAsync()
        {
            return await _httpClient.GetFromJsonAsync<DashboardViewModel>("api/dashboard");
        }
    }
}