using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_260323_SimpleAds.Data
{
    public static class Extensions
    {
        public static T GetOrNull<T>(this SqlDataReader reader, string columnName)
        {
            var value = reader[columnName];
            if (value == DBNull.Value)
            {
                return default(T);
            }

            return (T)value;
        }
    }
}
