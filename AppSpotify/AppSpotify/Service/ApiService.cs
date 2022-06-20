using AppSpotify.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppSpotify.Service
{
    public class ApiService
    {
        // Variables locales
        private string ApiUrl = "https://webapisongnjbp.azurewebsites.net/";

        // Metodos
        public async Task<ApiResponse> GetDataAsync(string controller)
        {
            try
            {
                // Invocamos la webapi con el metodo get
                var client = new HttpClient
                {
                    BaseAddress = new Uri(ApiUrl)
                };
                var response = await client.GetAsync(controller);
                var result = await response.Content.ReadAsStringAsync();

                // Validamos que nuestro resultado esta ok
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<ApiResponse>(result);
                }

                // Hubo un problema con el resultado de la webApi
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = result
                };

            }
            catch (Exception ex)
            {

                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }

        }

        public async Task<ApiResponse> PostDataAsync(string controller, object data)
        {
            try
            {
                // Serializamos nuestro objeto a texto en formato json
                var serialize = JsonConvert.SerializeObject(data);
                var content = new StringContent(serialize, Encoding.UTF8, "application/json");

                // Invocamos a webapi con el metodo post
                var client = new HttpClient
                {
                    BaseAddress = new Uri(ApiUrl)
                };
                var response = await client.PostAsync(controller, content);
                var result = await response.Content.ReadAsStringAsync();

                // Validamos que nuestro resultado esta ok
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<ApiResponse>(result);
                }

                // Hubo un problema con el resultado de la webApi
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ApiResponse> PutDataAsync(string controller, object data)
        {
            try
            {
                // Serializamos nuestro objeto a texto en formato json
                var serialize = JsonConvert.SerializeObject(data);
                var content = new StringContent(serialize, Encoding.UTF8, "application/json");

                // Invocamos a webapi con el metodo put
                var client = new HttpClient
                {
                    BaseAddress = new Uri(ApiUrl)
                };
                var response = await client.PutAsync(controller, content);
                var result = await response.Content.ReadAsStringAsync();

                // Validamos que nuestro resultado esta ok
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<ApiResponse>(result);
                }

                // Hubo un problema con el resultado de la webApi
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ApiResponse> DeleteDataAsync(string controller, int id)
        {
            try
            {
                // Invocamos a webapi con el metodo delete
                var client = new HttpClient
                {
                    BaseAddress = new Uri(ApiUrl)
                };
                var response = await client.DeleteAsync($"{controller}/{id}");
                var result = await response.Content.ReadAsStringAsync();

                // Validamos que nuestro resultado esta ok
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<ApiResponse>(result);
                }

                // Hubo un problema con el resultado de la webApi
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
