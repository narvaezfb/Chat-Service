using System;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Chat_Service.Services
{
	public class TokenValidationService: ITokenValidationService
	{
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7214";

        public TokenValidationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BaseUrl);
        }

        public async Task<bool> ValidateTokenAsync(string token)
		{
            try
            {
                var body = new StringContent($"\"{token}\"", Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"Auth/validateToken", body);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();

                    JObject responseData = JObject.Parse(responseContent);

                    bool isValidToken = (bool)responseData["isValidToken"];

                    if (isValidToken)
                    {
                        return true;
                    }
                    return false;
                }

                return false;
            }
            catch(Exception)
            {
                return false;
            }   
        }
	}
}

