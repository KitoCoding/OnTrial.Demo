using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NQAP
{
    /// <summary>
    /// Provides HTTP calls for sending and receiving information from a HTTP server
    /// </summary>
    public static class WebRequests
    {
        /// <summary>
        /// GETs a web request to an URL and returns the raw http web response
        /// </summary>
        /// <remarks>IMPORTANT: Remember to close the returned <see cref="HttpWebResponse"/> stream once done</remarks>
        /// <param name="pURL">The URL</param>
        /// <param name="pConfigureRequest">Allows caller to customize and configure the request prior to the request being sent</param>
        /// <param name="pBearerToken">If specified, provides the Authorization header with `bearer token-here` for things like JWT bearer tokens</param>
        /// <returns></returns>
        public static async Task<HttpWebResponse> GetAsync(string pURL, Action<HttpWebRequest> pConfigureRequest = null, string pBearerToken = null)
        {
            #region Setup

            // Create the web request
            var request = WebRequest.CreateHttp(pURL);

            // Make it a GET request method
            request.Method = HttpMethod.Get.ToString();

            // If we have a bearer token then add the bearer token to header
            if (pBearerToken != null)
                request.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {pBearerToken}");

            // Any custom work
            pConfigureRequest?.Invoke(request);

            #endregion

            try
            {
                // Return the raw server response
                return await request.GetResponseAsync() as HttpWebResponse;
            }
            // Catch Web Exceptions (which throw for things like 401)
            catch (WebException ex)
            {
                // If we got a response...
                if (ex.Response is HttpWebResponse httpResponse)
                    return httpResponse;

                // Otherwise, we don't have any information to be able to return
                throw;
            }
        }

        /// <summary>
        /// POSTs a web request to an URL and returns the raw http web response
        /// </summary>
        /// <remarks>IMPORTANT: Remember to close the returned <see cref="HttpWebResponse"/> stream once done</remarks>
        /// <param name="pURL">The URL</param>
        /// <param name="pContent">The content to post</param>
        /// <param name="pSendType">The format to serialize the content into</param>
        /// <param name="pReturnType">The expected type of content to be returned from the server</param>
        /// <param name="pConfigureRequest">Allows caller to customize and configure the request prior to the content being written and sent</param>
        /// <param name="pBearerToken">If specified, provides the Authorization header with `bearer token-here` for things like JWT bearer tokens</param>
        /// <returns></returns>
        public static async Task<HttpWebResponse> PostAsync(string pURL, object pContent = null, ContentSerializers pSendType = ContentSerializers.Json, ContentSerializers pReturnType = ContentSerializers.Json, Action<HttpWebRequest> pConfigureRequest = null, string pBearerToken = null)
        {
            #region Setup

            // Create the web request
            var request = WebRequest.CreateHttp(pURL);

            // Make it a POST request method
            request.Method = HttpMethod.Post.ToString();

            // Set the appropriate return type
            request.Accept = pReturnType.ToMimeString();

            // Set the content type
            request.ContentType = pSendType.ToMimeString();

            // If we have a bearer token then add it
            if (pBearerToken != null)
                request.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {pBearerToken}");

            // Any custom work
            pConfigureRequest?.Invoke(request);

            #endregion

            #region Write Content

            // Set the content length
            if (pContent == null)
                request.ContentLength = 0;
            else
            {
                // Create content to write
                var contentString = string.Empty;

                // Serialize to Json?
                if (pSendType == ContentSerializers.Json)
                    contentString = JsonConvert.SerializeObject(pContent);
                else if (pSendType == ContentSerializers.Xml)
                {
                    // Create Xml serializer
                    var xmlSerializer = new XmlSerializer(pContent.GetType());

                    // Create a string writer to receive the serialized string
                    using (var stringWriter = new StringWriter())
                    {
                        // Serialize the object to a string
                        xmlSerializer.Serialize(stringWriter, pContent);

                        // Extract the string from the writer
                        contentString = stringWriter.ToString();
                    }
                }
                else
                {
                    // TODO: Throw error once we have Dna Framework exception types
                }

                using (var requestStream = await request.GetRequestStreamAsync())
                {
                    // Create a stream writer from the body stream...
                    using (var streamWriter = new StreamWriter(requestStream))
                        await streamWriter.WriteAsync(contentString);
                }
            }

            #endregion

            try
            {
                // Return the raw server response
                return await request.GetResponseAsync() as HttpWebResponse;
            }
            // Catch Web Exceptions (which throw for things like 401)
            catch (WebException ex)
            {
                // If we got a response...
                if (ex.Response is HttpWebResponse httpResponse)
                    // Return the response
                    return httpResponse;

                // Otherwise, we don't have any information to be able to return
                throw;
            }
        }
    }
}