using System.Collections.Generic;
using System.Linq;

namespace DatabaseApi
{
    public static class Extensions
    {

        public static string Implode(this IEnumerable<string> list, string glue)
        {
            if (list.Any())
            {
                return list.Aggregate((cumulative, current) => cumulative + glue + current);
            }
            else
            {
                return string.Empty;
            }
        }

    }
}