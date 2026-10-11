using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace webclient.Modules
{
    public class WebApi
    {
        private readonly IConfiguration _conf;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;

        public WebApi(
            IConfiguration conf,
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory)
        {
            _conf = conf;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }


        // ============================================================
        // CALL API
        // ============================================================

        public async Task<string?> Response<T>(
            T model,
            string url,
            string method)
        {
            if(string.IsNullOrEmpty(method))
            {
                method = "POST";
            }
            
            var token = GetToken();

            return await CallApi(
                url,
                token,
                JsonConvert.SerializeObject(model),
                method);
        }


        // ============================================================
        // HTTP REQUEST
        // ============================================================

        private async Task<string?> CallApi(
            string url,
            string token,
            string json,
            string method)
        {
            try
            {
                var webApiUrl =
                    _conf["Url:webapi"] ?? "";

                if (string.IsNullOrWhiteSpace(webApiUrl))
                {
                    throw new InvalidOperationException(
                        "Url:webapi is not configured.");
                }

                var fullUrl =
                    $"{webApiUrl.TrimEnd('/')}/{url.TrimStart('/')}";


                var client =
                    _httpClientFactory.CreateClient();


                using var request =
                    new HttpRequestMessage(
                        new HttpMethod(method),
                        fullUrl);


                request.Content =
                    new StringContent(
                        json,
                        Encoding.UTF8,
                        "application/json");


                // ====================================================
                // JWT AUTHENTICATION
                // ====================================================

                if (!string.IsNullOrWhiteSpace(token))
                {
                    request.Headers.Authorization =
                        new AuthenticationHeaderValue(
                            "Bearer",
                            token);
                }


                using var response =
                    await client.SendAsync(request);


                var responseBody =
                    await response.Content.ReadAsStringAsync();


                if (!response.IsSuccessStatusCode)
                {
                    // 401 = JWT missing/invalid/expired
                    if (response.StatusCode ==
                        System.Net.HttpStatusCode.Unauthorized)
                    {
                        await SignOutAsync();
                    }

                    throw new HttpRequestException(
                        $"API returned {(int)response.StatusCode} " +
                        $"{response.StatusCode}: {responseBody}");
                }


                return responseBody;
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Web API Error: {ex}");

                return null;
            }
        }


        // ============================================================
        // GET JWT TOKEN
        // ============================================================

        public string? GetToken()
        {
            return _httpContextAccessor
                .HttpContext?
                .User?
                .FindFirst("access_token")
                ?.Value;
        }


        // ============================================================
        // GET CURRENT USER ID
        // ============================================================

        public string? GetUserId()
        {
            return _httpContextAccessor
                .HttpContext?
                .User?
                .FindFirst("itemid")
                ?.Value;
        }


        // ============================================================
        // GET USERNAME
        // ============================================================

        public string? GetUsername()
        {
            return _httpContextAccessor
                .HttpContext?
                .User?
                .FindFirst(
                    System.Security.Claims.ClaimTypes.Name)
                ?.Value;
        }


        // ============================================================
        // GET USER ROLE
        // ============================================================

        public string? GetUserRole()
        {
            return _httpContextAccessor
                .HttpContext?
                .User?
                .FindFirst("role")
                ?.Value;
        }


        // ============================================================
        // LOGOUT
        // ============================================================

        private async Task SignOutAsync()
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                await _httpContextAccessor.HttpContext
                    .SignOutAsync("ApplicationCookie");
            }
        }
    }
}
