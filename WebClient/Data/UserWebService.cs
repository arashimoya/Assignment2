using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Models;

namespace WebAPI.Data
{
    public class UserWebService : IUserService
    {
        private readonly HttpClient client;
        
        public UserWebService()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => {
                return true;
            };

            client = new HttpClient(clientHandler);
        }
        
        public async Task<User> ValidateLogin(string username, string password)
        {
            HttpResponseMessage response =
                await client.GetAsync(
                    $"https://localhost:5003/users?username={username}&password={password}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string userAsJson = await response.Content.ReadAsStringAsync();
                User resultUser = JsonSerializer.Deserialize<User>(userAsJson);
                return resultUser;
            }

            throw new Exception("User not found");
        }

        public async Task RegisterUser(string username, string password)
        {
            User newUser = new User();
            newUser.Username = username;
            newUser.Password = password;

            string userAsJson = JsonSerializer.Serialize(newUser);
            HttpContent content = new StringContent(userAsJson,
                Encoding.UTF8,
                "application/json");
            
            HttpResponseMessage response =
            await client.PostAsync("https://localhost:5003/users", content);

            if (response.StatusCode != HttpStatusCode.Created)
                throw new Exception("This user is already taken!");

        }
    }
}