using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyModelProject.Common;
using CompanyModelProject.Model;
using CompanyModelProject;
using System.Data.SqlClient;
using System.Data;

namespace CompanyModelProject.DataAccess
{
    public class baseInfoDataAccess
    {
        #region SQL
        /// <summary>
        /// 查询详情
        /// </summary>
        public const string SQL_SELECT_ONE = @"SELECT [ID],[webName] ,[domain] ,[keywords],[description]  FROM [HtmlManagement].[dbo].[baseInfo] with(nolock) where ID=@Id  ";
        public const string SQL_INSERT = "INSERT INTO [HtmlManagement].[dbo].[baseInfo]([webName] ,[domain] ,[description],[keywords]) VALUES ( @webName, @domain,@description,@keywords) SELECT @@identity";
        public const string SQL_UPDATE = @"UPDATE [HtmlManagement].[dbo].[baseInfo]
      SET  
      webName = @webName,
      domain=@domain,
      description=@description,
      keywords=@keywords 
      WHERE [ID]=@ID";
        public const string SQL_SELECT = @"SELECT [ID],[webName] ,[domain] ,[keywords],[description]  FROM [HtmlManagement].[dbo].[baseInfo] with(nolock)   ";
      
        #endregion
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(baseInfoModel model)
        {
            SqlParameter[] paras = { 
                                       new SqlParameter("@webName",SqlDbType.NVarChar),   
                                       new SqlParameter("@domain",SqlDbType.NVarChar) , 
                                         new SqlParameter("@description",SqlDbType.NVarChar) , 
                                       new SqlParameter("@keywords",SqlDbType.NVarChar)  
                                   };
            paras[0].Value = model.webName;
            paras[1].Value = model.domain;
            paras[2].Value = model.description;
            paras[3].Value = model.keywords;
            object oo = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteScalar(CommandType.Text, SQL_INSERT, paras);
            return oo == null ? 0 : int.Parse(oo.ToString());
        }

        public int update(baseInfoModel model)
        {
            SqlParameter[] paras = {   
                                       new SqlParameter("@webName",SqlDbType.NVarChar),   
                                       new SqlParameter("@domain",SqlDbType.NVarChar) , 
                                         new SqlParameter("@description",SqlDbType.NVarChar) , 
                                       new SqlParameter("@keywords",SqlDbType.NVarChar) ,
                                         new SqlParameter("@ID",SqlDbType.BigInt)
                                   };
            paras[0].Value = model.webName;
            paras[1].Value = model.domain;
            paras[2].Value = model.description;
            paras[3].Value = model.keywords;
            paras[4].Value = model.ID;
            int result = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteNonQuery(CommandType.Text, SQL_UPDATE, paras);
            return result;
        }
      
        public baseInfoModel GetOne(int id)
        {
            baseInfoModel model = new baseInfoModel();
            SqlParameter[] paras = { 
                                       new SqlParameter("@ID",SqlDbType.BigInt)   
                                   };
            paras[0].Value = id;
            DataSet ds = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteDataset(CommandType.Text, SQL_SELECT_ONE, paras);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    model = RowToModel(dr);
                }
            }
            else
            {
                return null;
            }
            return model;
        }

        public List<baseInfoModel> Getlist()
        {
            List<baseInfoModel> model = new List<baseInfoModel>();
           
            DataSet ds = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteDataset(CommandType.Text, SQL_SELECT);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    model.Add(RowToModel(dr)); 
                }
            }
            else
            {
                return null;
            }
            return model;
        } 
        private static baseInfoModel RowToModel(DataRow row)
        {
            var item = new baseInfoModel();
            item.ID = row.IsNull("ID") ? 0 : row.Field<int>("Id");
            item.webName = row.IsNull("webName") ? string.Empty : row.Field<string>("webName");
            item.keywords = row.IsNull("keywords") ? string.Empty : row.Field<string>("keywords");
            item.description = row.IsNull("description") ? string.Empty : row.Field<string>("description");
            item.domain = row.IsNull("domain") ? string.Empty : row.Field<string>("domain");
            return item;
        } 
    }
}
