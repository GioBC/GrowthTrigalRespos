using GrowthTrigal.Common.Models;
using System.Threading.Tasks;

namespace MyLeasing.Common.Services
{
    public interface IApiService
    {


        Task<Response<TokenResponse>> GetTokenAsync(
            string urlBase,
            string servicePrefix,
            string controller,
            TokenRequest request);



        Task<Response<object>> PutAsync<T>(
           string urlBase,
           string servicePrefix,
           string controller,
           T model,
           string tokenType,
           string accessToken);




        Task<Response<object>> PostAsync<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            T model,
            string tokenType,
            string accessToken);


        Task<Response<object>> PutAsync<T>(
          string urlBase,
          string servicePrefix,
          string controller,
          int id,
          T model,
          string tokenType,
          string accessToken);

        

        Task<Response<UPResponse>> GetUPByEmailAsync(
   string urlBase,
   string servicePrefix,
   string controller,
   string tokenType,
   string accessToken,
   string AliasFarm );


        Task<Response<FlowerResponse>> GetFlowerAsync(
   string urlBase,
   string servicePrefix,
   string controller,
   string tokenType,
   string accessToken,
   int id);





        Task<bool> CheckConnectionAsync(string url);


        Task<Response<object>> GetLastMeasuremetByFlowerId(
      string urlBase,
      string servicePrefix,
      string controller,
      string tokenType,
      string accessToken,
      int flowerId);



        Task<Response<object>> GetListAsync<T>(
           string urlBase,
           string servicePrefix,
           string controller,
           string tokenType,
           string accessToken);
        Task GetUPByEmailAsync(string url, string v1, string v2, string aliasFarm);
    }

}