using System;

namespace URLSessions
{
    public class URL
    {
        /// <summary>
        /// The absolute string for the URL
        /// </summary>
        public string UrlString { get; }
        public string BaseURL {
            get
            {
                var uri = new Uri(UrlString);
                return uri.Host;
            }
        }

        /// <summary>
        /// A value that identifies the location of a resource, such as an item on a remote server
        /// </summary>
        /// <param name="urlString">A URL location</param>
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
