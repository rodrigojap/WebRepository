using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebRepository.Infra.Contracts
{
    public interface IWebRepository
    {
        Task<T> AddAsync<T>(Dictionary<string, object> keyValueParameters, string requestUri);

        Task<bool> DeleteAsync<T>(Dictionary<string, object> keyValueParameters, string requestUri);

        Task<T> EditAsync<T>(Dictionary<string, object> keyValueParameters, string requestUri);

        Task<T> GetAsync<T>(Dictionary<string, object> keyValueParameters, string requestUri);        
    }
}
