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
    public class AdminInfoDataAccess
    {
        #region  SQL
        /// <summary>
        /// 查询单个用户信息
        /// </summary>
        public const string SQL_SELECT_one = @"SELECT [ID]
      ,[AdminName]
      ,[Pwd]
,Email
 ,[IsShow]
      ,[isDel]
      FROM [HtmlManagement].[dbo].[AdminInfo] with(nolock) where ID=@Id and isDel=0";
        /// <summary>
        /// 查询用户信息
        /// </summary>
        public const string SQL_SELECT = @"SELECT [ID]
      ,[AdminName]
      ,[Pwd]
      ,Email
      ,[isDel]
      ,[IsShow]
      FROM [HtmlManagement].[dbo].[AdminInfo] with(nolock) where isDel=0 and isshow=1 order by [ID] desc ";
        /// <summary>
        /// 根据用户名查询用户
        /// </summary>
        public const string SQL_SELECT_Name = @"SELECT  [ID]
      ,[AdminName]
      ,[Pwd]
,Email
 ,[IsShow]
      ,[isDel] 
  FROM [HtmlManagement].[dbo].[AdminInfo]  with(nolock) where AdminName=@AdminName and isDel=0";
        /// <summary>
        /// 查询用户名和密码信息
        /// </summary>
        public const string SQL_SELECT_user = @"SELECT [ID]
       ,[AdminName]
      ,[Pwd]
,Email
 ,[IsShow]
      ,[isDel] 
  FROM [HtmlManagement].[dbo].[AdminInfo]  with(nolock) where AdminName=@AdminName and Pwd=@Pwd and isDel=0 ";

        /// <summary>
        /// 注册用户
        /// </summary>

        public const string SQL_INSERT = @"INSERT INTO [HtmlManagement].[dbo].[AdminInfo]
           ([AdminName]
      ,[Pwd]
,Email
      ,[isDel] 
           )
     VALUES
           (  @AdminName,@Pwd,@Email,@isDel
         ) SELECT @@identity";
        /// <summary>
        /// 删除
        /// </summary>
        public const string SQL_DELETE = @"DELETE FROM  [HtmlManagement].[dbo].[AdminInfo] where ID=@Id";
        /// <summary>
        /// 修改密码
        /// </summary>
        public const string SQL_UPDATE_pwd = @"UPDATE [HtmlManagement].[dbo].[AdminInfo]
      SET  
      [Pwd] = @Pwd
 WHERE [ID]=@ID";
        /// <summary>
        /// 修改密码
        /// </summary>
        public const string SQL_UPDATE = @"UPDATE [HtmlManagement].[dbo].[AdminInfo]
      SET   
      [Email] = @Email
 WHERE [ID]=@ID";
        #endregion
        #region  方法
        /// <summary>
        /// 返回单个用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AdminInfoModel GetOne(int id)
        {
            AdminInfoModel model = new AdminInfoModel();
            SqlParameter[] paras = { 
                                       new SqlParameter("@ID",SqlDbType.BigInt)   
                                   };
            paras[0].Value = id;
            DataSet ds = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteDataset(CommandType.Text, SQL_SELECT_one, paras);
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

        public AdminInfoModel GetloginInfo(string username, string pwd)
        {
            AdminInfoModel model = new AdminInfoModel();
            SqlParameter[] paras = { 
                                       new SqlParameter("@AdminName",SqlDbType.NVarChar)   ,
                                          new SqlParameter("@Pwd",SqlDbType.NVarChar)
                                   };
            paras[0].Value = username;
            paras[1].Value = pwd;

            DataSet ds = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteDataset(CommandType.Text, SQL_SELECT_user, paras);
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


        public AdminInfoModel Getusername(string username)
        {
            AdminInfoModel model = new AdminInfoModel();
            SqlParameter[] paras = { 
                                       new SqlParameter("@AdminName",SqlDbType.NVarChar)
                                        
                                   };
            paras[0].Value = username;
            DataSet ds = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteDataset(CommandType.Text, SQL_SELECT_Name, paras);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    model = RowToModel(dr);
                }
            }


            return model;
        }

    
        public int Insert(AdminInfoModel model)
        {
            SqlParameter[] paras = { 
                                       new SqlParameter("@AdminName",SqlDbType.NVarChar),   
                                       new SqlParameter("@Pwd",SqlDbType.NVarChar) , 
                                         new SqlParameter("@Email",SqlDbType.NVarChar) , 
                                       new SqlParameter("@IsDel",SqlDbType.Bit) 
                                  
                                   };
            paras[0].Value = model.AdminName; 
            paras[1].Value = model.Pwd;
            paras[2].Value = model.Email;  
            paras[3].Value = model.IsDel; 
            object oo = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteScalar(CommandType.Text, SQL_INSERT, paras);
            return oo == null ? 0 : int.Parse(oo.ToString());
        }

        public int Delete(int id)
        {
            SqlParameter[] paras = { 
                                       new SqlParameter("@ID",SqlDbType.BigInt)   
                                   };
            paras[0].Value = id;
            int oo = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteNonQuery(CommandType.Text, SQL_DELETE, paras);
            return oo;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
  

        public int update_PWD(string pwd, int Id)
        {
            SqlParameter[] paras = {  
                                       new SqlParameter("@Pwd",SqlDbType.NVarChar) , 
                                        new SqlParameter("@ID",SqlDbType.Int)
                                   };
            paras[0].Value = pwd;
            paras[1].Value = Id;
            int result = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteNonQuery(CommandType.Text, SQL_UPDATE_pwd, paras);
            return result;
        }
        public int update(AdminInfoModel model)
        {
            SqlParameter[] paras = {   
                                         new SqlParameter("@Email",SqlDbType.NVarChar) , 
                                        new SqlParameter("@ID",SqlDbType.Int)
                                   };
            paras[0].Value = model.Email;
            paras[1].Value =model.ID; 
            int result = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteNonQuery(CommandType.Text, SQL_UPDATE, paras);
            return result;
        }
        public List<AdminInfoModel> getlist()
        {
            List<AdminInfoModel> list = new List<AdminInfoModel>();
            DataSet ds = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteDataset(CommandType.Text, SQL_SELECT);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                  list.Add(RowToModel(dr));
                }
            }
            else
            {
                list = null;
            }
            return list;
        }

        private static AdminInfoModel RowToModel(DataRow row)
        {
            var item = new AdminInfoModel();
            item.ID = row.IsNull("ID") ? 0 : row.Field<int>("Id"); 
            item.AdminName = row.IsNull("AdminName") ? string.Empty : row.Field<string>("AdminName");
            item.Pwd = row.IsNull("Pwd") ? string.Empty : row.Field<string>("Pwd");
            item.Email = row.IsNull("Email") ? string.Empty : row.Field<string>("Email");
            item.IsShow = row.IsNull("IsShow") ? false : row.Field<bool>("IsShow");
            item.IsDel = row.IsNull("IsDel") ? false : row.Field<bool>("IsDel");
            return item;
        }
        #endregion
    }
}
