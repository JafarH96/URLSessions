using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace URLSessions
{
    class Utils
    {
        public static Regex URLValidator = new Regex("^https?:\\/\\/(?:www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b(?:[-a-zA-Z0-9()@:%_\\+.~#?&\\/=]*)$");

        public static Dictionary<string, string> JsonHTTPHeaders()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("Accept", "application/json");

            return dict;
        }
    }
}
