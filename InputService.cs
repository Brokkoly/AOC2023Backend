namespace AOC2023Backend
{
    public class InputService : IInputService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public InputService(IHttpClientFactory httpClientFactory) { _httpClientFactory = httpClientFactory; }

        public async Task<string> GetInputString(string dayNumber, string cookieValue)
        {
            using HttpClient client = _httpClientFactory.CreateClient();
            var url = String.Format("https://adventofcode.com/2023/day/{0}/input", dayNumber);
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            requestMessage.Headers.Add("Cookie", String.Format("session={0}", cookieValue));
            var response = await client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            return body ?? "";
        }
    }

    public interface IInputService
    {
        Task<string> GetInputString(string dayNumber, string cookieValue);
    }
}
