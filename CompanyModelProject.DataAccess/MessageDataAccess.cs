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
    public class MessageDataAccess
    {
        #region SQL
        /// <summary>
        /// 查询详情
        /// </summary>
        public const string SQL_SELECT_ONE = @"SELECT [ID],[username] ,[main] ,[phone],[QQ] ,[Email] ,[Orders],[addtime],[IsShow],[IsDel] FROM [HtmlManagement].[dbo].[Message] with(nolock) where ID=@Id  ";
        public const string SQL_INSERT = "INSERT INTO [HtmlManagement].[dbo].[Message]([username] ,[main] ,[phone],[QQ] ,[Email] ,[Orders]) VALUES ( @username, @main,@phone,@QQ,@Email,@Orders) SELECT @@identity";
        public const string SQL_UPDATE = @"UPDATE [HtmlManagement].[dbo].[Message]
      SET   
      IsShow=@IsShow ,
      IsDel=@IsDel ,
      Orders=@Orders
      WHERE [ID]=@ID";
        public const string SQL_SELECT = @"SELECT [ID],[username] ,[main] ,[phone],[QQ] ,[Email] ,[Orders],[addtime],[IsShow],[IsDel] FROM [HtmlManagement].[dbo].[Message] with(nolock) order by ID desc   ";
        /// <summary>
        /// 逻辑删除
        /// </summary>
        public const string SQL_UPDATE_del = @"UPDATE [HtmlManagement].[dbo].[Message]
      SET   
      [IsDel] = @IsDel
 WHERE [ID]=@ID";

        #endregion
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(MessageModel model)
        {
            SqlParameter[] paras = { 
                                       new SqlParameter("@username",SqlDbType.NVarChar),   
                                       new SqlParameter("@main",SqlDbType.NVarChar) , 
                                         new SqlParameter("@phone",SqlDbType.NVarChar) , 
                                       new SqlParameter("@QQ",SqlDbType.NVarChar)  ,
                                       new SqlParameter("@Email",SqlDbType.NVarChar)  ,
                                       new SqlParameter("@Orders",SqlDbType.NVarChar)  

                                   };
            paras[0].Value = model.username;
            paras[1].Value = model.main;
            paras[2].Value = model.phone;
            paras[3].Value = model.QQ;
            paras[4].Value = model.Email;
            paras[5].Value = model.Orders; 
            object oo = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteScalar(CommandType.Text, SQL_INSERT, paras);
            return oo == null ? 0 : int.Parse(oo.ToString());
        }

        public int update(MessageModel model)
        {
            SqlParameter[] paras = {   
                                         new SqlParameter("@Orders",SqlDbType.Int) ,
                                         new SqlParameter("@IsDel",SqlDbType.Int) ,
                                         new SqlParameter("@IsShow",SqlDbType.Int) , 
                                         new SqlParameter("@ID",SqlDbType.BigInt)
                                   };
            paras[0].Value = model.Orders;
            paras[1].Value = model.isDel;
            paras[2].Value = model.IsShow; 
            paras[3].Value = model.ID;
            int result = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteNonQuery(CommandType.Text, SQL_UPDATE, paras);
            return result;
        }

        public MessageModel GetOne(int id)
        {
            MessageModel model = new MessageModel();
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

        public List<MessageModel> Getlist()
        {
            List<MessageModel> model = new List<MessageModel>();
           
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

        public int del_list(string where)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE [HtmlManagement].[dbo].[Message]  SET  [IsDel] = 1 WHERE [ID] in ");
            if (!string.IsNullOrEmpty(where))
            {
                sb.Append("( " + where + ")");

            }
            else
            {
                sb.Append("( )");
            }
            int result = DbHelper.GetInstance(ConnectionStringUtility.ConnectionString).ExecuteNonQuery(CommandType.Text, sb.ToString());

            return result;
        }

        public List<MessageModel> GetPageList(int pageIndex, int pageSize, ref int allcount, ref int pagecount, string strwhere)
        {
            DateTime date = DateTime.Now;
            List<MessageModel> pagelist = new List<MessageModel>();
            string strProc = "[PKG_PageData]";//存储过程名
            string sTable = " [dbo].[Message] ";
            string sPkey = " ID";
            string sField = "[ID],[username] ,[main] ,[phone],[QQ] ,[Email] ,[Orders],[addtime],[IsShow],[IsDel] ";//
            StringBuilder sb = new StringBuilder();
            sb.Append(" IsDel=0"); 
            if (strwhere != null && strwhere != "")
            {
                sb.Append(strwhere);
            }
            string sCondition = sb.ToString();
            string sOrder = " ID desc ";
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
                pagelist.Add(RowToModel(dr));
            }
            allcount = int.Parse(paras[7].Value.ToString());
            pagecount = (int)Math.Ceiling(double.Parse(allcount.ToString()) / pageSize);
            return pagelist;

        }
        private static MessageModel RowToModel(DataRow row)
        {
            var item = new MessageModel();
            item.ID = row.IsNull("ID") ? 0 : row.Field<int>("Id");
            item.username = row.IsNull("username") ? string.Empty : row.Field<string>("username");
            item.main = row.IsNull("main") ? string.Empty : row.Field<string>("main");
            item.phone = row.IsNull("phone") ? string.Empty : row.Field<string>("phone");
            item.QQ = row.IsNull("QQ") ? string.Empty : row.Field<string>("QQ");
            item.Email = row.IsNull("Email") ? string.Empty : row.Field<string>("Email");
            item.addtime = row.IsNull("addtime") ? DateTime.Now: row.Field<DateTime>("addtime"); 
            item.IsShow = row.IsNull("IsShow") ? false :  row.IsNull("IsShow");
            item.isDel = row.IsNull("isDel") ? false : row.IsNull("isDel");
            item.Orders = row.IsNull("Orders") ? 0 : row.Field<int>("Orders");

            return item;
        } 
    }
}
