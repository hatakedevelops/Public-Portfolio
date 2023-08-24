using OrchestratorService.DTO;

namespace OrchestratorService.Business;

public class Logic : ILogic
{
    const string ACCOUNT_API = "http://localhost:XXXX/api/Account";
    const string User_API = "http://localhost:XXXX/api/User";

    public async Task<List<TransferDTO>> RequestAccountServiceApiAsync(int userId, string token) 
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authroization", token);
            HttpResponseMessage response = await client.GetAsync(ACCOUNT_API + "/" + userId);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            var output = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TransferDTO>>(responseBody);
            return output ?? new List<TransferDTO>();
        }
    }

    public async void RequestAccountServiceApiAsync(TransferDTO transfer, string token)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", token);

            JsonContent jsonContent = JsonContent.Create(transfer);
            HttpResponseMessage response = await client.PostAsync(ACCOUNT_API + "/transfer", jsonContent);
            response.EnsureSuccessStatusCode();
        }
    }
    public async void RequestRegistrationServiceApiAsync(RegisterDTO register) 
    {
        using (HttpClient client = new HttpClient())
        {
            JsonContent jsonContent = JsonContent.Create(register);
            HttpResponseMessage response = await client.PostAsync(User_API + "/profile", jsonContent);
            response.EnsureSuccessStatusCode();
        }
    }
    public async Task<bool> CheckIfUsernameAvailAsync(string userName) 
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(User_API + "/username/checkavail/" + userName);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            var output = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(responseBody);
            return output;
        }
    }

}