using System;

namespace URLSessions
{
    public class URL
    {
        public string UrlString { get; }
        public string BaseURL {
            get
            {
                var uri = new Uri(UrlString);
                return uri.Host;
            }
        }

        public URL(string urlString)
        {
            if (Utils.URLValidator.IsMatch(urlString))
            {
                UrlString = urlString;
            }
            else
            {
                UrlString = null;
            }
        }
    }
}
