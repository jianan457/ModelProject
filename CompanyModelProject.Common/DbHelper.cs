
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Zhaopin.Universal.ZPDBHelper;
namespace CompanyModelProject.Common
{ 
    public class DbHelper
    {
        public DbHelper()
        { }

        /// <summary>
        /// 获取DbHelperService实例化对象
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static DbHelperService GetInstance(string connectionString)
        {
            return new DbHelperService(DatabaseType.MSSqlserver, connectionString);
        }

         
      
    }
}
