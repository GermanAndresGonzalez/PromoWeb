using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitarios
{
    public static class LectorExtensions
    {
        public static string SafeString(this SqlDataReader lector, string columna)
        {
            return !(lector[columna] is DBNull) ? (string)lector[columna] : string.Empty;
        }

        public static int SafeInt(this SqlDataReader lector, string columna)
        {
            return !(lector[columna] is DBNull) ? (int)lector[columna] : 0;
        }

        public static decimal SafeDecimal(this SqlDataReader lector, string columna)
        {
            return !(lector[columna] is DBNull) ? (decimal)lector[columna] : 0m;
        }
    }
}
