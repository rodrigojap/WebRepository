using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebRepository.Infra.Contracts
{
    public interface IRestHandler
    {
        Task<T> Execute<T>(Dictionary<string, object> keyValueParameters,
                                           string requestUri,
                                           Method method);        
        bool IsSucessfullResponse();
    }
}
