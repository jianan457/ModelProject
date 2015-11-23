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
    public class RightsDataAccess
    {
        #region SQL
        /// <summary>
        /// 查询详情
        /// </summary>
        public const string SQL_SELECT_ONE = @"SELECT [ID],upid,[RightsName] ,[Orders]  FROM [HtmlManagement].[dbo].[Rights] with(nolock) where upid=@upid  ";
        public const string SQL_INSERT = "INSERT INTO [HtmlManagement].[dbo].[Rights]([RightsName] ,upid,[Orders]) VALUES ( @RightsName,upid, @Orders) SELECT @@identity";
        public const string SQL_UPDATE = @"UPDATE [HtmlManagement].[dbo].[RightsName]
      SET  
       RightsName = @RightsName,
      Orders=@Orders 
      WHERE [ID]=@ID";
        public const string SQL_SELECT = @"SELECT [ID],[RightsName] ,upid,[Orders]  FROM [HtmlManagement].[dbo].[RightsName] with(nolock)   ";
      
        #endregion
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(RightsModel model)
        {
            SqlParameter[] paras = { 
                                       new SqlParameter("@RightsName",SqlDbType.NVarChar),   
                                       new SqlParameter("@Orders",SqlDbType.Int) 
                                   };
            paras[0].Value = model.RightsName;
            paras[1].Value = model.Orders; 
            object oo = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteScalar(CommandType.Text, SQL_INSERT, paras);
            return oo == null ? 0 : int.Parse(oo.ToString());
        }

        public int update(RightsModel model)
        {
            SqlParameter[] paras = {   
                                      new SqlParameter("@RightsName",SqlDbType.NVarChar),   
                                       new SqlParameter("@Orders",SqlDbType.Int),
                                         new SqlParameter("@ID",SqlDbType.BigInt)
                                   };
          paras[0].Value = model.RightsName;
            paras[1].Value = model.Orders; 
            paras[2].Value = model.ID;
            int result = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteNonQuery(CommandType.Text, SQL_UPDATE, paras);
            return result;
        }

        public List<RightsModel> Getonelist(int upid)
        {
            List<RightsModel> model = new List<RightsModel>();
            SqlParameter[] paras = { 
                                       new SqlParameter("@upid",SqlDbType.BigInt)   
                                   };
            paras[0].Value = upid;
            DataSet ds = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteDataset(CommandType.Text, SQL_SELECT_ONE, paras);
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

        public List<RightsModel> Getlist()
        {
            List<RightsModel> model = new List<RightsModel>();
           
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
        private static RightsModel RowToModel(DataRow row)
        {
            var item = new RightsModel();
            item.ID = row.IsNull("ID") ? 0 : row.Field<int>("Id");
            item.upid = row.IsNull("upid") ? 0 : row.Field<int>("upid"); 
            item.RightsName = row.IsNull("RightsName") ? string.Empty : row.Field<string>("RightsName");
            item.Orders = row.IsNull("Orders") ? 0: row.Field<int>("Orders"); 
            return item;
        } 
    }
}
