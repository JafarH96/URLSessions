using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace URLSessions
{
    public class URLSession
    {
        public static URLSession shared = new URLSession();

        public URLSession()
        {
        }

        public async Task<(T, HttpStatusCode)> DataTask<T>(URLRequest request)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response;
            HttpContent httpContent;
            string content;

            client.Timeout = request.timeout;
            foreach (var header in request.allHTTPHeaders)
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }

            switch (request.method)
            {
                case URLRequest.Method.GET:
                    response = await client.GetAsync(request.url.UrlString);
                    if(response.IsSuccessStatusCode)
                    {
                        content = await response.Content.ReadAsStringAsync();
                        var des = JsonSerializer.Deserialize(content, typeof(T));
                        return ((T)des, HttpStatusCode.OK);
                    }
                    return (default(T), response.StatusCode);

                case URLRequest.Method.POST:
                    httpContent = new StringContent(request.jsonBody, System.Text.Encoding.UTF8, "application/json");
                    response = await client.PostAsync(request.url.UrlString, httpContent);
                    
                    if(response.IsSuccessStatusCode)
                    {
                        content = await response.Content.ReadAsStringAsync();
                        return ((T)JsonSerializer.Deserialize(content, typeof(T)), HttpStatusCode.OK);
                    }
                    return (default(T), response.StatusCode);

                case URLRequest.Method.PUT:
                    httpContent = new StringContent(request.jsonBody, System.Text.Encoding.UTF8, "application/json");
                    response = await client.PutAsync(request.url.UrlString, httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        content = await response.Content.ReadAsStringAsync();
                        return ((T)JsonSerializer.Deserialize(content, typeof(T)), HttpStatusCode.OK);
                    }
                    return (default(T), response.StatusCode);

                case URLRequest.Method.DELETE:
                    response = await client.DeleteAsync(request.url.UrlString);

                    if (response.IsSuccessStatusCode)
                    {
                        content = await response.Content.ReadAsStringAsync();
                        return ((T)JsonSerializer.Deserialize(content, typeof(T)), HttpStatusCode.OK);
                    }
                    return (default(T), response.StatusCode);

                default:
                    return (default(T), HttpStatusCode.BadRequest);
            }
        }

        public async Task<(T, HttpStatusCode)> Data<T>(URL url, int timeout = 30)
        {
            if (url.UrlString is null)
            {
                throw new NullReferenceException("Invalid URL!");
            }
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeout);

            var response = await client.GetAsync(url.UrlString);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return ((T)JsonSerializer.Deserialize(content, typeof(T)), HttpStatusCode.OK);
            }
            return (default(T), response.StatusCode);
        }

        public async Task<HttpStatusCode> Download(URL url, string destination, int timeout = 30)
        {
            if (url.UrlString is null)
            {
                throw new NullReferenceException("Invalid URL!");
            }
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeout);

            var response = await client.GetAsync(url.UrlString);
            if (response.IsSuccessStatusCode)
            {
                using (FileStream fs = new FileStream(destination, FileMode.CreateNew))
                {
                    await response.Content.CopyToAsync(fs);
                    return HttpStatusCode.OK;
                }
            }
            return response.StatusCode;
        }
    }
}
