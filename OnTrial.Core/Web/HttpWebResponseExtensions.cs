using System.IO;
using System.Net;

namespace NQAP
{
    /// <summary>
    /// Extension methods for <see cref="HttpWebResponse"/>
    /// </summary>
    public static class HttpWebResponseExtensions
    {
        /// <summary>
        /// Returns a <see cref="WebRequestResult{T}"/> pre-populated with the <see cref="HttpWebResponse"/> information
        /// </summary>
        /// <typeparam name="T">The type of response to create</typeparam>
        /// <param name="pServerResponse">The server response</param>
        /// <returns></returns>
        public static T ToObject<T>(this HttpWebResponse pServerResponse)
        {
            // Return a new web request result
            var result = new WebRequestResult<T>
            {
                ContentType = pServerResponse.ContentType,
                Headers = pServerResponse.Headers,
                Cookies = pServerResponse.Cookies,
                StatusCode = pServerResponse.StatusCode,
                StatusDescription = pServerResponse.StatusDescription,
            };

            if (result.StatusCode == HttpStatusCode.OK)
            {
                using (var responseStream = pServerResponse.GetResponseStream())
                {
                    using (var streamReader = new StreamReader(responseStream))
                        result.RawServerResponse = streamReader.ReadToEnd();
                }
            }

            return result.ServerResponse;
        }
    }
}