using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string ToTableName(this string entityName) => entityName.Substring(0, entityName.Length - 6);
    }
}
