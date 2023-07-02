using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices
{
    public class LineNotifyExtension
    {
        private const string _lineNotifyApiUrl = "https://notify-api.line.me/api/notify";

        private readonly string _accessToken;

        public LineNotifyExtension(string accessToken)
        {
            this._accessToken = accessToken;
        }
        public async Task<HttpResponseMessage> SendMessageAsync(string message, string notificationDisabled)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");
                    var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("message", message),
                        new KeyValuePair<string, string>("notificationDisabled", notificationDisabled)
                    });
                    var response = await client.PostAsync(_lineNotifyApiUrl, content);
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
