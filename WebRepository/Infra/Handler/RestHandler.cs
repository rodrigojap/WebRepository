using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using WebRepository.Infra.Configuration;
using WebRepository.Infra.Contracts;
using WebRepository.Models;
using WebRepository.Notifications.Contracts;

namespace WebRepository.Infra.Handler
{
    public class RestHandler : IRestHandler
    {
        private RestClient BaseClient = new RestClient("https://mybaseaddressAPI.com/");
        private RestRequest Request;
        private dynamic Response;
        private readonly IAPINotification APINotification;

        public RestHandler(IAPINotification notification)
        {
            BaseClient.Authenticator = new BaseApiAuthenticator();
            Request = new RestRequest();
            APINotification = notification;
        }

        public async Task<T> Execute<T>(Dictionary<string, object> keyValueParameters, string requestUri, Method method)
        {
            InitializeParametersRequest(requestUri, method, keyValueParameters);

            await SendRequestAsync<T>();

            return Response.Data;
        }

        public bool IsSucessfullResponse()
        {
            return Response ?? Response.IsSuccessful;
        }

        private void InitializeParametersRequest(string requestUri,
                                                 Method method,
                                                 Dictionary<string, object> keyValueParameters)
        {
            SetResource(requestUri);
            SetHttpMethod(method);
            AddParameters(keyValueParameters);
        }

        private void SetResource(string requestUri)
        {
            Request.Resource = requestUri;
        }

        private void SetHttpMethod(Method method)
        {
            Request.Method = method;
        }

        private void AddParameters(Dictionary<string, object> keyValueParameters)
        {
            foreach (var (key, value) in keyValueParameters)
            {
                Request.AddParameter(key, value);
            }
        }

        private async Task SendRequestAsync<T>()
        {
            Response = await this.BaseClient.ExecuteAsync<T>(this.Request);
        }

        private async Task SendRequestAsync()
        {
            Response = await this.BaseClient.ExecuteAsync(this.Request);
        }

        private void ValidadeReponse()
        {
            if (Response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                APINotification.Handle(new APINotificationModel("Oh no! You got a Bad Request ! Error: 400"));
            }
            else if (Response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                APINotification.Handle(new APINotificationModel("You shall not pass! Error: 401"));
            }
            else if (Response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                APINotification.Handle(new APINotificationModel("Something is wrong with the server :(! Error: 500 "));
            }
        }

        public void Dispose()
        {
            this.BaseClient = null;
            this.Request = null;
            this.Response = null;
        }
    }
}
