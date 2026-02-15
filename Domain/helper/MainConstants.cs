using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helper
{
    public static class MainConstants
    {
        static string access_token = "access_token=0699F386CB16161E0AD3E86AA0A08CA8F178C0823C23119CDF34A53C84E7783C";

        static string facePrintURL = "https://10.0.250.169:8098/api/";
        public static string Use(string path="",string queryString="")
        {
            return facePrintURL+path +'?'+ queryString + '&'+access_token;
        }



    }
}
