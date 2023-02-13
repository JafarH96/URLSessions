using System;
using System.Collections.Generic;
using URLSessions;

namespace URLSessionTest
{
    struct ToDOs
    {
        public int userId {get; set;}
        public int id { get; set; }
        public string title { get; set; }
        public bool completed { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var url = new URL("https://jsonplaceholder.typicode.com/posts");
            var request = new URLRequest(url, URLRequest.Method.POST);
            request.SetValue("json/application", "Accept");
            Request(request);
            Console.ReadKey();
        }

        private static async void Request(URLRequest request)
        {
            var (model, status) = await URLSession.shared.DataTask<List<ToDOs>>(request);
        }
    }
}
