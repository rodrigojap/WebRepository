using RestSharp;
using RestSharp.Authenticators;

namespace WebRepository.Infra.Configuration
{
    public class BaseApiAuthenticator : IAuthenticator
    {
        //this method is called before the API call
        public void Authenticate(IRestClient client, IRestRequest request)
        {
            //We can use this method to set required or default headers.            
            //We can get values by claims or another storage.

            //ex:
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "Bearer " + "myacces token by some claim");
        }
    }
}
