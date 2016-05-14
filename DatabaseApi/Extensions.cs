using System.Collections.Generic;
using System.Linq;

namespace DatabaseApi
{
    public static class Extensions
    {

        public static string Implode(this IEnumerable<string> list, string glue)
        {
            return list.Aggregate((cumulative, current) => cumulative + glue);
        }

    }
}