using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Weather.Common.Extensions;
using Weather.Domain.ApplicationServices;
using Weather.Domain.DataContracts;
using Weather.Domain.InfrastructureServices;
using Weather.Domain.Models;

namespace Weather.Application.Services
{
    public class HgWeatherService : IHgWeatherService
    {
        private readonly IHttpRequestService _httpRequestService;

        private readonly string _HgWeatherKey = "ba25438b";
        private readonly string _HgWeatherUrl = "https://api.hgbrasil.com/weather?key={0}&city_name={1},{2}&fields=only_results,temp,city_name,date,time";

        public HgWeatherService(IHttpRequestService httpRequestService)
        {
            _httpRequestService = httpRequestService;
        }

        public async Task<Temperature> GetTemperature(City city)
        {
            var cultureInfo = new CultureInfo("pt-BR");
            var completeUrl = String.Format(_HgWeatherUrl, _HgWeatherKey, city.Name, city.State);
            var result = await _httpRequestService.ExecuteRequest(completeUrl, RestSharp.Method.GET);

            if (result.IsSuccessful)
            {
                var response = JsonConvert.DeserializeObject<HGWeatherResponse>(result.Content);

                if (response != null && response.City_Name.RemoveDiacritics(true) == city.Name)
                {
                    return new Temperature()
                    {
                        Degrees = response.Temp,
                        Created = DateTime.Parse($"{response.Date} {response.Time}", cultureInfo),
                        CityId = city.Id
                    };
                }
            }

            return null;
        }
    }
}