using RestSharp;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Weather.Domain.InfrastructureServices;

namespace Weather.Infrastructure.Services
{
    public class HttpRequestService : IHttpRequestService
    {
        private string FormatQueryParameters(Dictionary<string, string> parameters)
        {
            var formatedParameters = new List<string>();

            foreach (var parameter in parameters)
            {
                formatedParameters.Add($"{parameter.Key}={parameter.Value}");
            }

            return "?" + string.Join("&", formatedParameters);
        }

        public async Task<IRestResponse> ExecuteRequest(string url, Method method, Dictionary<string, string> parameters = null)
        {
            if (parameters != null)
                url += FormatQueryParameters(parameters);

            var client = new RestClient(url);
            var request = new RestRequest(method);

            using var cancellationTokenSource = new CancellationTokenSource();
            return await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);
        }
    }
}