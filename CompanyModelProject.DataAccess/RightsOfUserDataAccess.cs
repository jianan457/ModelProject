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
    public class RightsOfUserDataAccess
    {
        #region SQL
        /// <summary>
        /// 查询详情
        /// </summary>
        public const string SQL_INSERT = "INSERT INTO [HtmlManagement].[dbo].[RightsOfUser]([UserId] ,[RightIds]) VALUES ( @UserId, @RightIds) SELECT @@identity";
         public const string SQL_UPDATE = @"UPDATE [HtmlManagement].[dbo].[RightsOfUser]
      SET  
      UserId = @UserId,
      RightIds=@RightIds 
      WHERE [ID]=@ID";
         public const string SQL_SELECT = @"SELECT [ID],[UserId] ,[RightIds] ,IsDel FROM [HtmlManagement].[dbo].[RightsOfUser] with(nolock)   ";
         public const string SQL_SELECT_ONE = @"SELECT [ID],[UserId] ,[RightIds],IsDel  FROM [HtmlManagement].[dbo].[RightsOfUser] with(nolock) WHERE ID=@ID  ";
     
        #endregion
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(RightsOfUserModel model)
        {
            SqlParameter[] paras = {  
                                       new SqlParameter("@UserId",SqlDbType.Int) , 
                                          new SqlParameter("@RightIds",SqlDbType.NVarChar)
                                   };
            paras[0].Value = model.UserId;
            paras[1].Value = model.RightIds; 
            object oo = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteScalar(CommandType.Text, SQL_INSERT, paras);
            return oo == null ? 0 : int.Parse(oo.ToString());
        }

        public int update(RightsOfUserModel model)
        {
            SqlParameter[] paras = {   
                                      new SqlParameter("@UserId",SqlDbType.Int),   
                                       new SqlParameter("@RightIds",SqlDbType.NVarChar),
                                         new SqlParameter("@ID",SqlDbType.BigInt)
                                   };
          paras[0].Value = model.UserId;
          paras[1].Value = model.RightIds; 
            paras[2].Value = model.ID;
            int result = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteNonQuery(CommandType.Text, SQL_UPDATE, paras);
            return result;
        }

        public RightsOfUserModel GetOne(int id)
        {
            RightsOfUserModel model =new  RightsOfUserModel();
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

        public List<RightsOfUserModel> Getlist()
        {
            List<RightsOfUserModel> model = new List<RightsOfUserModel>();
           
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
        private static RightsOfUserModel RowToModel(DataRow row)
        {
            var item = new RightsOfUserModel();
            item.ID = row.IsNull("ID") ? 0 : row.Field<int>("Id");
            item.RightIds = row.IsNull("RightIds") ? string.Empty : row.Field<string>("RightIds");
            item.UserId = row.IsNull("UserId") ? 0 : row.Field<int>("UserId");
            item.IsDel = row.IsNull("IsDel") ? false : true;
            return item;
        } 
    }
}
