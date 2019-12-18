using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Weather.Domain.InfrastructureServices
{
    public interface IHttpRequestService
    {
        Task<IRestResponse> ExecuteRequest(string url, Method method, Dictionary<string, string> parameters = null);
    }
}