using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;
using Weather.Domain.ApplicationServices;
using Weather.Domain.DataContracts;
using Weather.Domain.InfrastructureServices;

namespace Weather.Application.Services
{
    public class CepService : ICepService
    {
        private readonly string _viaCepUrl = "http://viacep.com.br/ws/{0}/json/";
        private readonly IHttpRequestService _requestService;

        public CepService(IHttpRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<RequestCity> GetCityByCep(string cep)
        {
            var result = await _requestService.ExecuteRequest(string.Format(_viaCepUrl, cep), Method.GET);

            if (result.IsSuccessful)
            {
                var viaCepResponse = JsonConvert.DeserializeObject<ViaCepResponse>(result.Content);

                if (viaCepResponse.Erro == false)
                    return new RequestCity
                    {
                        Name = viaCepResponse.Localidade,
                        State = viaCepResponse.UF
                    };
            }

            return null;
        }
    }
}