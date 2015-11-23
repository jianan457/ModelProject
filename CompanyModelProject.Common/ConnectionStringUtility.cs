using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyModelProject.Common
{
    public class ConnectionStringUtility
    {
        public static string ConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["DefaultConnection"].ToString();
            }
        }
      
      
    }
}