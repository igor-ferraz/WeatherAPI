using System.Threading.Tasks;
using Weather.Domain.Models;

namespace Weather.Domain.ApplicationServices
{
    public interface IHgWeatherService
    {
        Task<Temperature> GetTemperature(City city);
    }
}