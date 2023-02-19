using System;
using System.Collections.Generic;
using System.Text.Json;

namespace URLSessions
{
    public class URLRequest
    {
        /// <summary>
        /// HTTP method
        /// </summary>
        public enum Method
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        public URL url;
        public Method method;
        /// <summary>
        /// HTTP body as json
        /// </summary>
        public string jsonBody;
        /// <summary>
        /// Request timeout
        /// </summary>
        public TimeSpan timeout;
        public Dictionary<string, string> allHTTPHeaders;

        /// <summary>
        /// Encapsulates two essential properties of a load request: the URL to load and the policies used to load it
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="timeout"></param>
        public URLRequest(URL url, Method method, int timeout = 30)
        {
            if(url.UrlString is null)
            {
                throw new NullReferenceException("Invalid URL!");
            }
            this.url = url;
            this.method = method;
            this.timeout = TimeSpan.FromSeconds(timeout);
            allHTTPHeaders = new Dictionary<string, string>();
            if(method == Method.POST || method == Method.PUT)
            {
                foreach(var header in Utils.JsonHTTPHeaders())
                {
                    allHTTPHeaders.Add(header.Key, header.Value);
                }
            }
        }

        /// <summary>
        /// Set http body for POST or PUT requests
        /// </summary>
        /// <param name="body">Any struct or class models</param>
        public void SetHTTPBody(object body)
        {
            jsonBody = JsonSerializer.Serialize(body);
        }

        /// <summary>
        /// Set HTTP header
        /// </summary>
        /// <param name="value"></param>
        /// <param name="forHTTPHeader">Header key</param>
        public void SetValue(string value, string forHTTPHeader)
        {
            if (allHTTPHeaders.ContainsKey(forHTTPHeader))
            {
                allHTTPHeaders[forHTTPHeader] = value;
            }
            else
            {
                allHTTPHeaders.Add(forHTTPHeader, value);
            }
        }

        /// <summary>
        /// Returns http method as string
        /// </summary>
        /// <returns>HTTP method string</returns>
        public string GetMethodString()
        {
            switch (method)
            {
                case Method.GET:
                    return "GET";
                case Method.POST:
                    return "POST";
                case Method.PUT:
                    return "PUT";
                case Method.DELETE:
                    return "DELETE";
                default:
                    return null;
            }
        }
    }
}
