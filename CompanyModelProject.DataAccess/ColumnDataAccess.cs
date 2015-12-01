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
    public class ColumnDataAccess
    {
        #region SQL
        /// <summary>
        /// 查询单个栏目详情
        /// </summary>
        public const string SQL_SELECT_ONE = @"SELECT [ID],[Upid] ,[ColumnName] ,[AddTime],[AddUser] ,[Orders] ,[IsShow],[IsDel]
     FROM [HtmlManagement].[dbo].[ColName] with(nolock) where ID=@Id and isDel=0 ";

        public const string SQL_SELECT_Col = @"SELECT *
     FROM [HtmlManagement].[dbo].[ColName] with(nolock) where ColumnName=@ColumnName and isDel=0 ";
        /// <summary>
        /// 根据查询某父一级栏目
        /// </summary>
        public const string SQL_SELECT_FRIST = "SELECT [ID],[Upid] ,[ColumnName] ,[AddTime],[AddUser] ,[Orders] ,[IsShow],[IsDel] FROM [HtmlManagement].[dbo].[ColName] WHERE ID=@Upid and IsDel=0  order by orders";
      
        /// <summary>
        /// 查询一级首页推荐栏目
        /// </summary>
        public const string SQL_SELECT_FRIST_Index = "SELECT [ID],[Upid] ,[ColumnName] ,[AddTime],[AddUser] ,[Orders] ,[IsShow],[IsDel] FROM [HtmlManagement].[dbo].[ColName] WHERE Upid=0 and IsDel=0 order by orders";

        /// <summary>
        /// 查询所有栏目
        /// </summary>
        public const string SQL_SELECT_ALL = "select a.ID,a.Upid,REPLICATE('  ',b.level) +'  ┝  '+a.ColumnName AS Name ,b.level,b.sort from [HtmlManagement].[dbo].[ColName]  a,[HtmlManagement].[dbo].f_getC(0) b where a.ID=b.id  and @where order by case when b.level<3 then 0 else 1 end,b.sort,b.level";
        /// <summary>
        ///    /// 根据查询某子一级栏目
        /// </summary>
        public const string SQL_SELECT_SON = @"SELECT [ID] ,Upid,[ColumnName] ,[AddTime],[AddUser] ,[Orders] ,[IsShow]
      ,[isDel] 
  FROM [HtmlManagement].[dbo].[ColName]  with(nolock) where Upid=@Id and isDel=0 ";


        public const string SQL_INSERT = "INSERT INTO [HtmlManagement].[dbo].[ColName]([Upid] ,[ColumnName] ,[AddUser],[Orders] ,[IsShow] ,[IsDel]) VALUES ( @Upid, @ColumnName,@AddUser,@Orders,@IsShow,@isDel) SELECT @@identity";
        public const string SQL_UPDATE = @"UPDATE [HtmlManagement].[dbo].[ColName]
      SET  
      ColumnName = @ColumnName,
      Upid=@Upid,
      AddUser=@AddUser,
      Orders=@Orders,
      IsShow=@IsShow
      WHERE [ID]=@ID";

        /// <summary>
        /// 删除
        /// </summary>
        public const string SQL_DELETE = @"DELETE FROM  [HtmlManagement].[dbo].[ColName] where ID=@Id ";

        /// <summary>
        /// 批量删除
        /// </summary>
        public const string SQL_DELETE_list = @"DELETE FROM  [HtmlManagement].[dbo].[ColName] where ID in (@ids) ";
        #endregion
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(ColumnModel model)
        {
            SqlParameter[] paras = { 
                                       new SqlParameter("@Upid",SqlDbType.NVarChar),   
                                       new SqlParameter("@ColumnName",SqlDbType.NVarChar) , 
                                         new SqlParameter("@AddUser",SqlDbType.NVarChar) , 
                                       new SqlParameter("@IsDel",SqlDbType.Bit) ,
                                       new SqlParameter("@IsShow",SqlDbType.Bit) ,
                                       new SqlParameter("@Orders",SqlDbType.Int)  
                                   };
            paras[0].Value = model.Upid;
            paras[1].Value = model.ColumnName;
            paras[2].Value = model.AddUser;
            paras[3].Value = model.IsDel;
            paras[4].Value = model.IsShow;
            paras[5].Value = model.Orders; 
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
        public int Delete_list(string ids)
        {
            SqlParameter[] paras = { 
                                       new SqlParameter("@ids",SqlDbType.NVarChar)   
                                   };
            paras[0].Value = ids;
            int oo = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteNonQuery(CommandType.Text, SQL_DELETE_list, paras);
            return oo;
        }

        public int update(ColumnModel model)
        {
            SqlParameter[] paras = {   
                                         new SqlParameter("@ColumnName",SqlDbType.NVarChar) , 
                                         new SqlParameter("@Upid",SqlDbType.Int) , 
                                         new SqlParameter("@AddUser",SqlDbType.NVarChar) , 
                                         new SqlParameter("@Orders",SqlDbType.Int) , 
                                         new SqlParameter("@IsShow",SqlDbType.Int) ,  
                                        new SqlParameter("@ID",SqlDbType.Int)
                                   };
            paras[0].Value = model.ColumnName;
            paras[1].Value = model.Upid;
            paras[2].Value = model.AddUser;
            paras[3].Value = model.Orders;
            paras[4].Value = model.IsShow;
            paras[5].Value = model.ID;
            int result = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteNonQuery(CommandType.Text, SQL_UPDATE, paras);
            return result;
        }
        /// <summary>
        /// 返回栏目详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ColumnModel GetOne(int id)
        {
            ColumnModel model = new ColumnModel();
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

        public ColumnModel checkname(string  name)
        {
            ColumnModel model = new ColumnModel();
            SqlParameter[] paras = { 
                                       new SqlParameter("@ColumnName",SqlDbType.NVarChar)   
                                   };
            paras[0].Value = name;
            DataSet ds = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteDataset(CommandType.Text, SQL_SELECT_Col, paras);
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

        /// <summary>
        /// 查询栏目子级
        /// </summary>
        /// <param name="CId">栏目ID</param>
        /// <returns></returns>
        public List<ColumnModel> getSonList(int CId)
        {
            List<ColumnModel> list = new List<ColumnModel>();
            SqlParameter[] paras = { 
                                       new SqlParameter("@Id",SqlDbType.NVarChar)
                                        
                                   };
            paras[0].Value = CId;
            DataSet ds = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteDataset(CommandType.Text, SQL_SELECT_SON, paras);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    list.Add(RowToModel(dr));
                }
            }
            return list;
        }

        /// <summary>
        /// 查询所有栏目
        /// </summary>
        /// <param name="CId"></param>
        /// <returns></returns>
        public List<AllColumnModel> getAllList(string where)
        { 
            List<AllColumnModel> list = new List<AllColumnModel>(); 
            StringBuilder sb = new StringBuilder(); 
            sb.Append("select a.ID,a.Upid,REPLICATE('  ',b.level) +'  ┝  '+a.ColumnName AS Name ,b.level,b.sort from [HtmlManagement].[dbo].[ColName]  a,[HtmlManagement].[dbo].f_getC(0) b where a.ID=b.id ");
            if (!string.IsNullOrEmpty(where))
            {
                sb.Append(" and " + where + " order by case when b.level<3 then 0 else 1 end,b.sort,b.level");

            }
            else {
                sb.Append(" order by case when b.level<3 then 0 else 1 end,b.sort,b.level");
            } 
            DataSet ds = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteDataset(CommandType.Text, sb.ToString());
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    list.Add(AllColRowToModel(dr));
                }
            }
            return list;
        }
        /// <summary>
        /// 查询一级栏目列表
        /// </summary>
        /// <returns></returns>
        public List<ColumnModel> getFristColumnlist(int upid)
        {
            List<ColumnModel> list = new List<ColumnModel>();
            SqlParameter[] paras = { 
                                       new SqlParameter("@Upid",SqlDbType.NVarChar)
                                        
                                   };
            paras[0].Value = upid;
            DataSet ds = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteDataset(CommandType.Text, SQL_SELECT_FRIST,paras);
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

        /// <summary>
        /// 查询首页显示一级栏目
        /// </summary>
        /// <returns></returns>
        public List<ColumnModel> getFristColumnlist_Index()
        {
            List<ColumnModel> list = new List<ColumnModel>();
            DataSet ds = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteDataset(CommandType.Text, SQL_SELECT_FRIST_Index);
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

        private static ColumnModel RowToModel(DataRow row)
        {
            var item = new ColumnModel();
            item.ID = row.IsNull("ID") ? 0 : row.Field<int>("Id");
            item.ColumnName = row.IsNull("ColumnName") ? string.Empty : row.Field<string>("ColumnName");
            item.Upid = row.IsNull("Upid") ? 0 : row.Field<int>("Upid");
            item.AddUser = row.IsNull("AddUser") ? string.Empty : row.Field<string>("AddUser");
            item.AddTime = row.IsNull("AddTime") ? DateTime.Now : row.Field<DateTime>("AddTime");
            item.IsShow = row.IsNull("IsShow") ? false : row.Field<bool>("IsShow");
            item.IsDel = row.IsNull("IsDel") ? false : row.Field<bool>("IsDel");
            item.Orders = row.IsNull("Orders") ? 0 : row.Field<int>("Orders");
            return item;
        }

        private static AllColumnModel AllColRowToModel(DataRow row)
        {
            var item = new AllColumnModel();
            item.ID = row.IsNull("ID") ? 0 : row.Field<int>("Id");
            item.Name = row.IsNull("Name") ? string.Empty : row.Field<string>("Name");
            item.Upid = row.IsNull("Upid") ? 0 : row.Field<int>("Upid");
            item.level = row.IsNull("level") ? 0 : row.Field<int>("level");
            item.sort = row.IsNull("sort") ? string.Empty : row.Field<string>("sort");
            return item;
        }
    }
}
