using System.Threading.Tasks;
using Weather.Domain.DataContracts;

namespace Weather.Domain.ApplicationServices
{
    public interface ICepService
    {
        Task<RequestCity> GetCityByCep(string cep);
    }
}