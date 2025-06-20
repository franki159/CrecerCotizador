using Newtonsoft.Json;
using System.Collections.Generic;
using System.Infrastructure.Tools.Services.Models;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.Tools.Services
{
    public class ApiService
    {

        //public async Task<Response> SetPhoto(int customerId, Stream stream)
        //{
        //    try
        //    {
        //        var array = Utilities.ReadFully(stream);

        //        var photoRequest = new PhotoRequest
        //        {
        //            Id = customerId,
        //            Array = array,
        //        };

        //        var request = JsonConvert.SerializeObject(photoRequest);
        //        var body = new StringContent(request, Encoding.UTF8, "application/json");
        //        var client = new HttpClient();
        //        client.BaseAddress = new Uri("http://zulu-software.com");
        //        var url = "/ECommerce/api/Customers/SetPhoto";
        //        var response = await client.PostAsync(url, body);

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            return new Response
        //            {
        //                IsSuccess = false,
        //                Message = response.StatusCode.ToString(),
        //            };
        //        }

        //        return new Response
        //        {
        //            IsSuccess = true,
        //            Message = "Foto asignada Ok",
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response
        //        {
        //            IsSuccess = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}


        //public async Task<string> GetToken(string urlBase, string username, string password)
        //{
        //    try
        //    {
        //        var user = new LoginUser
        //        {
        //            usuario = username,
        //            clave = password,
        //        };

        //        var request = JsonConvert.SerializeObject(user);
        //        var client = new HttpClient();
        //        client.BaseAddress = new Uri(urlBase);
        //        var response = await client.PostAsync("login",
        //            new StringContent(request,
        //            Encoding.UTF8, "application/json"));//"application/x-www-form-urlencoded")/;);
        //        var resultJSON = await response.Content.ReadAsStringAsync();
        //        //var result = JsonConvert.DeserializeObject<ResponseT<object>>(resultJSON);//TokenResponse

        //        return resultJSON;
        //    }
        //    catch (Exception ex)
        //    {
        //        var aaa = ex;
        //        return null;
        //    }
        //}

        //public async Task<Response<NULL>> GetUserByEmail(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, string email)
        //{
        //    try
        //    {
        //        var userRequest = new UserRequest { Email = email, };
        //        var request = JsonConvert.SerializeObject(userRequest);
        //        var content = new StringContent(request, Encoding.UTF8, "application/json");
        //        var client = new HttpClient();
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
        //        client.BaseAddress = new Uri(urlBase);
        //        var url = string.Format("{0}{1}", servicePrefix, controller);
        //        var response = await client.PostAsync(url, content);

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            return new Response<>
        //            {
        //                IsSuccess = false,
        //                Message = response.StatusCode.ToString(),
        //            };
        //        }

        //        var result = await response.Content.ReadAsStringAsync();
        //        var newRecord = JsonConvert.DeserializeObject<User>(result);

        //        return new Response
        //        {
        //            IsSuccess = true,
        //            Message = "Record added OK",
        //            Resullt = newRecord,
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response
        //        {
        //            IsSuccess = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}


        public async Task<Response> Get<T>(string urlBase, string servicePrefix, string urlParameters, string tokenType, string accessToken, bool condicion = true)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                if (condicion)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                }
                var url = string.Format("{0}{1}", servicePrefix, urlParameters);
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                        //error = true
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<T>(result);

                return new Response
                {
                    IsSuccess = true,
                    //Message = newRecord.msj",
                    Resullt = res,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task PostConection_3(LogRequest model)
        {
            HttpClient client = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var values = new Dictionary<string, string>
            {
                  {"idSequence","0"},
                  { "origin","sdasdasd" },
                  {"nameClass","CargarAsientosData" },
                  { "nameMethod","asdasdasd"},
                  { "ipOrigin","30.30.30.30" },
                  { "type","prueba"},
                  {"user","asdsadasd" },
                  {"message","Error al realizar el borrado de rechazos de primas: "},
                  {"content","Error" },
                  {"key","asdasdasd" }



            };

            var content = new FormUrlEncodedContent(values);

            try
            {
                var response = await client.PostAsync("https://crecerdesarrolloapi.azure-api.net/LogCrecer/LogSeguridad/RegistrarLog", content);

                var responseString = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {

                throw;
            }



        }



        public async Task PostDataAsync(LogRequest model)
        {
            try
            {
                // Create HttpClient instance
                using (var httpClient = new HttpClient())
                {
                    // Define the request URL
                    var apiUrl = "https://crecerdesarrolloapi.azure-api.net/LogCrecer/LogSeguridad/RegistrarLog";

                    // Define JSON payload
                    //var jsonPayload = "{\"idSequence\":0,\"origin\":\"sdasdasd\",\"nameClass\":\"CargarAsientosData\",\"nameMethod\":\"asdasdasd\",\"ipOrigin\":\"30.30.30.30\",\"type\":\"prueba\",\"user\":\"asdsadasd\",\"message\":\"Error al realizar el borrado de rechazos de primas: \",\"content\":\"Error\",\"key\":\"asdasdasd\"}";
                    var request = JsonConvert.SerializeObject(model);
                    //var content = new StringContent(request);

                    // Create StringContent with JSON payload
                    var content = new StringContent(request, Encoding.UTF8, "application/json");
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    // Send POST request
                    var response = await httpClient.PostAsync(apiUrl, content);

                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {

                        var result = await response.Content.ReadAsStringAsync();
                        var newRecord = JsonConvert.DeserializeObject<object>(result);
                        Console.WriteLine("Data sent successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Failed to send data. Status code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public VIEW_Respuesta PostConection_2(StringConecctionRequest model)
        {

            VIEW_Respuesta respuesta = new VIEW_Respuesta();

            respuesta.Ok();

            try

            {

                WebRequest wrGETURL;

                wrGETURL = WebRequest.Create("https://tokendesarrollocrecer.azurewebsites.net/SecretoSeguridad/SecretoSeguridad");

                wrGETURL.Method = "POST";

                wrGETURL.ContentType = @"application/json; charset=utf-8";

                using (var stream = new StreamWriter(wrGETURL.GetRequestStream()))

                {

                    var json = JsonConvert.SerializeObject(model);

                    stream.Write(json);

                }

                wrGETURL.Timeout = 180000;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                HttpWebResponse webresponse = wrGETURL.GetResponse() as HttpWebResponse;

                Encoding enc = System.Text.Encoding.GetEncoding("utf-8");

                StreamReader loResponseStream = new StreamReader(webresponse.GetResponseStream(), enc);

                string strResult = loResponseStream.ReadToEnd();

                respuesta = JsonConvert.DeserializeObject<VIEW_Respuesta>(strResult);

                // close the stream object

                loResponseStream.Close();

                // close the response object

                webresponse.Close();

            }

            catch (Exception ex)

            {

                respuesta.Error("Error al Momento de registrar en el API", ex);

                return respuesta;

            }

            return respuesta;
        }

        public async Task<Response> Post<T, U>(string urlBase, string servicePrefix, string controller,
            string tokenType, string accessToken, T model, bool condicion = true)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                if (condicion)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                }

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}/{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var newRecord = JsonConvert.DeserializeObject<U>(result);

                return new Response
                {
                    IsSuccess = true,
                    //Message = newRecord.msj",
                    Resullt = newRecord,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }




        public Response PostNotAsinc<T, U>(string urlBase, string servicePrefix, string controller,
            string tokenType, string accessToken, T model, bool condicion = true)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                if (condicion)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                }

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}/{1}", servicePrefix, controller);
                HttpResponseMessage response = client.PostAsync(url, content).Result;

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = response.Content.ReadAsStringAsync().Result;
                var newRecord = JsonConvert.DeserializeObject<U>(result);

                return new Response
                {
                    IsSuccess = true,
                    //Message = newRecord.msj",
                    Resullt = newRecord,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<T> GetGoogleService<T>(string cadena, string UrlServerMethod) where T : class
        {
            try
            {
                var client = new HttpClient();
                var urlSend = cadena + "" + UrlServerMethod;
                var response = await client.GetAsync(urlSend);
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                var result = await response.Content.ReadAsStringAsync();
                var lista = JsonConvert.DeserializeObject<T>(result);

                if (lista == null)
                {
                    return null;
                }

                return lista;
            }
            catch
            {
                return null;
            }

        }
    }
}
