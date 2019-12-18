using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Weather.Common.Extensions;
using Weather.Domain.ApplicationServices;
using Weather.Domain.DataContracts;

namespace WeatherApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly ICepService _cepService;

        public CitiesController(ICityService cityService, ICepService cepService)
        {
            _cityService = cityService;
            _cepService = cepService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCities()
        {
            var cities = await _cityService.GetAll();
            return Ok(cities);
        }

        [HttpGet("{cityName}/{state}/temperatures")]
        public async Task<IActionResult> GetTemperatures(string cityName, string state)
        {
            var cityReceived = new RequestCity
            {
                Name = cityName.RemoveDiacritics(true),
                State = state.RemoveDiacritics(true)
            };

            var cityResponse = await _cityService.GetByName(cityReceived);
            return Ok(cityResponse);
        }

        [HttpPut]
        public async Task<IActionResult> AddCity(RequestCity cityReceived)
        {
            cityReceived.Name = cityReceived.Name.RemoveDiacritics(true);
            cityReceived.State = cityReceived.State.RemoveDiacritics(true);

            var cityAdded = await _cityService.AddCity(cityReceived);
            return Ok(cityAdded);
        }

        [HttpDelete("{cityName}/{state}")]
        public async Task<IActionResult> DeleteCity(string cityName, string state)
        {
            var cityReceived = new RequestCity
            {
                Name = cityName.RemoveDiacritics(true),
                State = state.RemoveDiacritics(true)
            };

            var success = await _cityService.DeleteCity(cityReceived);
            return Ok(success);
        }

        [HttpDelete("{cityName}/{state}/temperatures")]
        public async Task<IActionResult> DeleteTemperatures(string cityName, string state)
        {
            var cityReceived = new RequestCity
            {
                Name = cityName.RemoveDiacritics(true),
                State = state.RemoveDiacritics(true)
            };

            var success = await _cityService.DeleteTemperatures(cityReceived);
            return Ok(success);
        }

        [HttpPost("cep/{cep}")]
        public async Task<IActionResult> AddCityByCEP(string cep)
        {
            if (cep.Length != 8)
                return BadRequest("CEP length must be exactly 8.\nWithout dots (.) and dashes (-).");

            var city = await _cepService.GetCityByCep(cep);

            if (city == null)
                return BadRequest(String.Format("City not found (using CEP \"{0}\").", cep));

            return await AddCity(city);
        }

        [HttpPost("temperatures/update")]
        public async Task<IActionResult> UpdateTemperatures()
        {
            var success = await _cityService.UpdateTemperatures();
            return Ok(success);
        }
    }
}