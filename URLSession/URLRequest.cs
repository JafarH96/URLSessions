using System;
using System.Collections.Generic;
using System.Text.Json;

namespace URLSessions
{
    public class URLRequest
    {
        public enum Method
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        public URL url;
        public Method method;
        public string jsonBody;
        public TimeSpan timeout;
        public Dictionary<string, string> allHTTPHeaders;
       

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

        public void SetHTTPBody(object body)
        {
            jsonBody = JsonSerializer.Serialize(body);
        }

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
