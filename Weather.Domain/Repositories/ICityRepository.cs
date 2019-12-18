using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.Domain.DataContracts;
using Weather.Domain.Models;

namespace Weather.Domain.Repositories
{
    public interface ICityRepository
    {
        Task<List<City>> GetAll();

        Task<City> GetByName(RequestCity city);

        Task<City> AddCity(RequestCity city);

        Task<bool> DeleteCity(RequestCity city);

        Task<bool> DeleteTemperatures(RequestCity city);

        Task<City> AddTemperature(Temperature temperature);
    }
}