using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.Domain.ApplicationServices;
using Weather.Domain.DataContracts;
using Weather.Domain.Models;
using Weather.Domain.Repositories;

namespace Weather.Application.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IHgWeatherService _hgWeatherService;

        public CityService(ICityRepository cityRepository, IHgWeatherService hgWeatherService)
        {
            _cityRepository = cityRepository;
            _hgWeatherService = hgWeatherService;
        }

        public async Task<List<City>> GetAll()
        {
            return await _cityRepository.GetAll();
        }

        public async Task<City> GetByName(RequestCity city)
        {
            return await _cityRepository.GetByName(city);
        }

        public async Task<City> AddCity(RequestCity city)
        {
            return await _cityRepository.AddCity(city);
        }

        public async Task<bool> DeleteCity(RequestCity city)
        {
            return await _cityRepository.DeleteCity(city);
        }

        public async Task<bool> DeleteTemperatures(RequestCity city)
        {
            return await _cityRepository.DeleteTemperatures(city);
        }

        public async Task<City> AddTemperature(Temperature temperature)
        {
            return await _cityRepository.AddTemperature(temperature);
        }

        public async Task<bool> UpdateTemperatures()
        {
            var cities = await GetAll();

            foreach (var city in cities)
            {
                var temperature = await _hgWeatherService.GetTemperature(city);

                if (temperature != null)
                {
                    var dbCity = await AddTemperature(temperature);

                    if (dbCity == null)
                        return false;
                }
            }

            return true;
        }
    }
}