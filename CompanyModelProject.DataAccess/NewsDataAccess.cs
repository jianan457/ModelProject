using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompanyModelProject.Common;
using CompanyModelProject.Model;
using CompanyModelProject;
using System.Data.SqlClient;
using System.Data;
namespace CompanyModelProject.DataAccess
{
  public  class NewsDataAccess
    {
        #region  SQL
        /// <summary>
        /// 查询单个信息
        /// </summary>
      public const string SQL_SELECT_one = @"SELECT [ID]
      ,[ColumnId]
      ,[Title]
      ,[picUrl]
      ,[Main]
      ,[BriefMain]
      ,[MainWithout]
      ,[fromUrl]
      ,[HtmlUrl]
      ,[orders]
      ,[Creater]
      ,[CreateTime]
      ,[IsClomnrecommond]
      ,[IsIndexRecommond]
      ,[IsDel] 
      FROM [HtmlManagement ].[dbo].[News]
     with(nolock) where ID=@Id and isDel=0";
        /// <summary>
        /// 查询所有信息
        /// </summary>
        public const string SQL_SELECT_ALL = @"SELECT [ID]
      ,[ColumnId]
      ,[Title]
      ,[picUrl]
      ,[Main]
      ,[BriefMain]
      ,[MainWithout]
      ,[fromUrl]
      ,[HtmlUrl]
      ,[orders]
      ,[Creater]
      ,[CreateTime]
      ,[IsClomnrecommond]
      ,[IsIndexRecommond]
      ,[IsDel]
       FROM [HtmlManagement].[dbo].[News] with(nolock) where isDel=0  order by orders desc ";
        /// <summary>
        /// 根据栏目查询内容信息
        /// </summary>
        public const string SQL_SELECT_Col = @"SELECT  [ID]
      ,[ColumnId]
      ,[Title]
      ,[picUrl]
      ,[Main]
      ,[BriefMain]
      ,[MainWithout]
      ,[fromUrl]
      ,[HtmlUrl]
      ,[orders]
      ,[Creater]
      ,[CreateTime]
      ,[IsClomnrecommond]
      ,[IsIndexRecommond]
      ,[IsDel]
       FROM [HtmlManagement].[dbo].[News] with(nolock) where ColumnId=@ColumnId and isDel=0";


        public const string SQL_UPDATE = @"UPDATE [dbo].[News] 
       SET [ColumnId] = @ColumnId 
      ,[Title] = @Title 
      ,[picUrl] = @picUrl 
      ,[Main] = @Main 
      ,[BriefMain] =@BriefMain 
      ,[MainWithout] = @MainWithout 
      ,[fromUrl] = @fromUrl 
      ,[HtmlUrl] = @HtmlUrl 
      ,[orders] = @orders
      ,[Creater] = @Creater 
      
      ,[IsClomnrecommond] = @IsClomnrecommond 
      ,[IsIndexRecommond] = @IsIndexRecommond 
      ,[IsDel] = @IsDel 
      WHERE [ID]=@ID
";
        /// <summary>
        /// 添加内容
        /// </summary> 
        public const string SQL_INSERT = @"INSERT INTO [HtmlManagement].[dbo].[News]
           ([ColumnId]
           ,[Title]
           ,[picUrl]
           ,[Main]
           ,[BriefMain]
           ,[MainWithout]
           ,[fromUrl]
           ,[HtmlUrl]
           ,[orders]
           ,[Creater] 
           ,[IsClomnrecommond]
           ,[IsIndexRecommond]
           ,[IsDel])
        VALUES
           (@ColumnId
           ,@Title
           ,@picUrl
           ,@Main
           ,@BriefMain
           ,@MainWithout
           ,@fromUrl
           ,@HtmlUrl
           ,@orders
           ,@Creater 
           ,@IsClomnrecommond
           ,@IsIndexRecommond
           ,@IsDel)   SELECT @@identity";
       
        /// <summary>
        /// 删除
        /// </summary>
        public const string SQL_DELETE = @"DELETE FROM  [HtmlManagement].[dbo].[News] where ID=@Id";
      
        /// <summary>
        /// 逻辑删除
        /// </summary>
        public const string SQL_UPDATE_del = @"UPDATE [HtmlManagement].[dbo].[News]
      SET   
      [IsDel] = @IsDel
 WHERE [ID]=@ID";

        
        #endregion

        public int Insert(NewsModel model)
        {
            SqlParameter[] paras = { 
                                       new SqlParameter("@Title",SqlDbType.NVarChar),   
                                       new SqlParameter("@picUrl",SqlDbType.NVarChar) , 
                                       new SqlParameter("@orders",SqlDbType.Int) , 
                                         new SqlParameter("@MainWithout",SqlDbType.NVarChar) , 
                                         new SqlParameter("@Main",SqlDbType.NVarChar) , 
                                         new SqlParameter("@IsIndexRecommond",SqlDbType.Bit) , 
                                         new SqlParameter("@isDel",SqlDbType.Bit) , 
                                         new SqlParameter("@IsClomnrecommond",SqlDbType.Bit) , 
                                         new SqlParameter("@HtmlUrl",SqlDbType.NVarChar) , 
                                         new SqlParameter("@fromUrl",SqlDbType.NVarChar) ,  
                                         new SqlParameter("@Creater",SqlDbType.NVarChar) , 
                                         new SqlParameter("@ColumnId",SqlDbType.Int) , 
                                         new SqlParameter("@BriefMain",SqlDbType.NVarChar) , 
                                         new SqlParameter("@ID",SqlDbType.Int) , 
                                  
                                   };
            paras[0].Value = model.Title;
            paras[1].Value = model.picUrl;
            paras[2].Value = model.orders;
            paras[3].Value = model.MainWithout;
            paras[4].Value = model.Main;
            paras[5].Value = model.IsIndexRecommond;
            paras[6].Value = model.isDel;
            paras[7].Value = model.IsClomnrecommond;
            paras[8].Value = model.HtmlUrl;
            paras[9].Value = model.fromUrl;
            paras[10].Value = model.Creater;
            paras[11].Value = model.ColumnId;
            paras[12].Value = model.BriefMain;
            paras[13].Value = model.ID;


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

        public int del_list(string where)
        {
         
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE [HtmlManagement].[dbo].[News]  SET  [IsDel] = 1 WHERE [ID] in ");
            if (!string.IsNullOrEmpty(where))
            {
                sb.Append("( "+ where +")");

            }
            else
            {
                sb.Append("( )");
            }
            int result = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteNonQuery(CommandType.Text,sb.ToString());

            return result;
        }

        public int update(NewsModel model)
        {
            SqlParameter[] paras = {   
                                         new SqlParameter("@Title",SqlDbType.NVarChar),   
                                       new SqlParameter("@picUrl",SqlDbType.NVarChar) , 
                                       new SqlParameter("@orders",SqlDbType.Int) , 
                                         new SqlParameter("@MainWithout",SqlDbType.NVarChar) , 
                                         new SqlParameter("@Main",SqlDbType.NVarChar) , 
                                         new SqlParameter("@IsIndexRecommond",SqlDbType.Bit) , 
                                         new SqlParameter("@isDel",SqlDbType.Bit) , 
                                         new SqlParameter("@IsClomnrecommond",SqlDbType.Bit) , 
                                         new SqlParameter("@HtmlUrl",SqlDbType.NVarChar) , 
                                         new SqlParameter("@fromUrl",SqlDbType.NVarChar) ,  
                                         new SqlParameter("@Creater",SqlDbType.NVarChar) , 
                                         new SqlParameter("@ColumnId",SqlDbType.Int) , 
                                         new SqlParameter("@BriefMain",SqlDbType.NVarChar) , 
                                         new SqlParameter("@ID",SqlDbType.Int) , 
                                   };
            paras[0].Value = model.Title;
            paras[1].Value = model.picUrl;
            paras[2].Value = model.orders;
            paras[3].Value = model.MainWithout;
            paras[4].Value = model.Main;
            paras[5].Value = model.IsIndexRecommond;
            paras[6].Value = model.isDel;
            paras[7].Value = model.IsClomnrecommond;
            paras[8].Value = model.HtmlUrl;
            paras[9].Value = model.fromUrl; 
            paras[10].Value = model.Creater;
            paras[11].Value = model.ColumnId;
            paras[12].Value = model.BriefMain;
            paras[13].Value = model.ID;
            int result = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteNonQuery(CommandType.Text, SQL_UPDATE, paras);
            return result;
        }

        public int update_delete(NewsModel model)
        {
            SqlParameter[] paras = {   
                                         new SqlParameter("@isDel",SqlDbType.Bit) , 
                                         new SqlParameter("@ID",SqlDbType.Int)  
                                   }; 
            paras[0].Value = model.isDel;
            paras[1].Value = model.ID;
            int result = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteNonQuery(CommandType.Text, SQL_UPDATE_del, paras);
            return result;
        }
        /// <summary>
        /// 返回单条信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NewsModel GetOne(int id)
        {
            NewsModel model = new NewsModel();
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

        public List<NewsModel> getlist()
        {
            List<NewsModel> list = new List<NewsModel>();
            DataSet ds = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteDataset(CommandType.Text, SQL_SELECT_ALL);
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

        public List<NewsModel> getlistbycolId(int colID)
        {
            List<NewsModel> list = new List<NewsModel>();
            SqlParameter[] paras = { 
                                       new SqlParameter("@ColumnId",SqlDbType.NVarChar)
                                        
                                   };
            paras[0].Value = colID;
            DataSet ds = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteDataset(CommandType.Text, SQL_SELECT_Col, paras);
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
        /// 获取分页数据
        /// </summary>

        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="allcount"></param>
        /// <param name="pagecount"></param>
        /// <returns></returns>
        public List<pageModel> GetPageList(int pageIndex, int pageSize, ref int allcount, ref int pagecount, string strwhere)
        {
            DateTime date = DateTime.Now;
            List<pageModel> pagelist = new List<pageModel>();
            string strProc = "[PKG_PageData]";//存储过程名
            string sTable = " [dbo].[News] t1 inner join [dbo].[ColName] t2 on t1.ColumnId=t2.ID "; 
            string sPkey = " t1.ID";
            string sField = "t1.ID,[ColumnId] ,t2.ColumnName as Name,[Title],t1.[orders] ,HtmlUrl,[Creater] ,[CreateTime] ,[IsClomnrecommond] ,[IsIndexRecommond] ,t1.[IsDel]";//
            StringBuilder sb = new StringBuilder();
            sb.Append(" t1.isDel=0");
            sb.Append(" and t2.isDel=0"); 
            if (strwhere != null && strwhere != "")
            {
                sb.Append(strwhere);
            }
            string sCondition = sb.ToString();
            string sOrder = " t1.orders  ";
            SqlParameter[] paras = new[]{ new SqlParameter(){ParameterName="@sTable",SqlDbType=SqlDbType.VarChar,Value=sTable}, 
                                          new SqlParameter(){ParameterName="@sPkey", SqlDbType=SqlDbType.VarChar,Value=sPkey},
                                          new SqlParameter( ){ParameterName="@sField", SqlDbType=SqlDbType.VarChar,Value=sField}, 
                                          new SqlParameter(){ ParameterName="@iPageCurr",SqlDbType = SqlDbType.Int,Value=pageIndex},
                                          new SqlParameter(){ParameterName="@iPageSize", SqlDbType=SqlDbType.Int,Value=pageSize} ,
                                          new SqlParameter(){ParameterName="@sCondition", SqlDbType=SqlDbType.VarChar,Value=sCondition},  
                                          new SqlParameter(){ParameterName="@sOrder", SqlDbType=SqlDbType.VarChar,Value=sOrder}, 
                                          new SqlParameter(){ParameterName="@Counts", SqlDbType=SqlDbType.Int,Direction = ParameterDirection.Output}, 
                                          new SqlParameter(){ParameterName="@pageCount", SqlDbType=SqlDbType.Int,Direction = ParameterDirection.Output},
                                          new SqlParameter(){ParameterName="@nowDate", SqlDbType=SqlDbType.DateTime,Value=date}
                                   };
            DataSet ds = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteDataset(CommandType.StoredProcedure, strProc, paras);//执行存储过程 
            if (ds == null || ds.Tables[0].Rows.Count == 0) return null;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                pagelist.Add(pageRowToModel(dr));
            }
            allcount = int.Parse(paras[7].Value.ToString());
            pagecount = (int)Math.Ceiling(double.Parse(allcount.ToString()) / pageSize);
            return pagelist;

        }
        private static NewsModel RowToModel(DataRow row)
        {
            var item = new NewsModel();
            item.ID = row.IsNull("ID") ? 0 : row.Field<int>("Id");
            item.Title = row.IsNull("Title") ? string.Empty : row.Field<string>("Title");
            item.Main = row.IsNull("Main") ? string.Empty : row.Field<string>("Main");
            item.MainWithout = row.IsNull("MainWithout") ? string.Empty : row.Field<string>("MainWithout");
            item.BriefMain = row.IsNull("BriefMain") ? string.Empty : row.Field<string>("BriefMain");
            item.ColumnId = row.IsNull("ColumnId") ? 0 : row.Field<int>("ColumnId");
            item.Creater = row.IsNull("Creater") ? string.Empty : row.Field<string>("Creater");
            item.HtmlUrl = row.IsNull("HtmlUrl") ? string.Empty : row.Field<string>("HtmlUrl");
            item.picUrl = row.IsNull("picUrl") ? string.Empty : row.Field<string>("picUrl");
            item.fromUrl = row.IsNull("fromUrl") ? string.Empty : row.Field<string>("fromUrl");
            item.CreateTime = row.IsNull("CreateTime") ? DateTime.Now : row.Field<DateTime>("CreateTime");
            item.orders = row.IsNull("orders") ? 0 : row.Field<int>("orders");
            item.IsClomnrecommond = row.IsNull("IsClomnrecommond") ? false : true;
            item.IsIndexRecommond = row.IsNull("IsIndexRecommond") ? false : true; 
            item.isDel = row.IsNull("isDel") ? false : true;

            return item;
        }
        private static pageModel pageRowToModel(DataRow row)
        {
            var item = new pageModel();
            item.ID = row.IsNull("ID") ? 0 : row.Field<int>("Id");
            item.Title = row.IsNull("Title") ? string.Empty : row.Field<string>("Title"); 
            item.ColumnId = row.IsNull("ColumnId") ? 0 : row.Field<int>("ColumnId");
            item.Creater = row.IsNull("Creater") ? string.Empty : row.Field<string>("Creater");
            item.Name = row.IsNull("Name") ? string.Empty : row.Field<string>("Name");
            item.HtmlUrl = row.IsNull("HtmlUrl") ? string.Empty : row.Field<string>("HtmlUrl");  
            item.CreateTime = row.IsNull("CreateTime") ? DateTime.Now : row.Field<DateTime>("CreateTime");
            item.orders = row.IsNull("orders") ? 0 : row.Field<int>("orders");
            item.IsClomnrecommond = row.IsNull("IsClomnrecommond") ? false : true;
            item.IsIndexRecommond = row.IsNull("IsIndexRecommond") ? false : true;
            item.isDel = row.IsNull("isDel") ? false : true; 
            return item;
        }

    }
}
