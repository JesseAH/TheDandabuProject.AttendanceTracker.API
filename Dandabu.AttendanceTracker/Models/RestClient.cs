
using Twilio;

namespace Dandabu.AttendanceTracker.Models
{

    public interface IRestClient
    {
        Twilio.Message SendMessage(string from, string to, string body, params string[] mediaUrl);
    }

    public class RestClient : IRestClient
    {
        private readonly TwilioRestClient _client;

        public RestClient()
        {
            _client = new TwilioRestClient("ACfbd63f7d8fba474d56e28f142a34f267", "8503abed011cc358e75300d7323b1397");
        }

        public Twilio.Message SendMessage(string from, string to, string body, params string[] mediaUrl)
        {
            return _client.SendMessage(from, to, body, mediaUrl);
        }

    }

}
