using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using WebRepository.Infra.Contracts;

namespace WebRepository.Infra.Repository
{
    public class RestRepository : IWebRepository
    {
        private readonly IRestHandler RestHandler;

        public RestRepository(IRestHandler restHandler)
        {
            RestHandler = restHandler;
        }

        public async Task<T> AddAsync<T>(Dictionary<string, object> keyValueParameters, string requestUri)
        {
            return await RestHandler.Execute<T>(keyValueParameters, requestUri, Method.POST);
        }

        public async Task<T> GetAsync<T>(Dictionary<string, object> keyValueParameters, string requestUri)
        {
            return await RestHandler.Execute<T>(keyValueParameters, requestUri, Method.GET);
        }

        public async Task<bool> DeleteAsync<T>(Dictionary<string, object> keyValueParameters, string requestUri)
        {
            await RestHandler.Execute<T>(keyValueParameters, requestUri, Method.DELETE);

            return RestHandler.IsSucessfullResponse();
        }

        public async Task<T> EditAsync<T>(Dictionary<string, object> keyValueParameters, string requestUri)
        {
            return await RestHandler.Execute<T>(keyValueParameters, requestUri, Method.PUT);
        }
    }
}
