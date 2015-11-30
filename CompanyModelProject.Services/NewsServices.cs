using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompanyModelProject.Model;
using CompanyModelProject.IServices;
using CompanyModelProject.Common;
using CompanyModelProject.DataAccess;
namespace CompanyModelProject.Services
{
    public class NewsServices : INewsServices
    {
        public static object obj = new object();

        private static INewsServices instance = null;

        private NewsDataAccess access = new NewsDataAccess();

        public NewsServices() { }
        public static INewsServices getinstance()
        {
            if (instance == null)
            {
                lock (obj)
                {
                    instance = new NewsServices();
                }
            }

            return instance;
        }

        public  int Insert(NewsModel model)
        {

            return access.Insert(model);
        }
        public int Delete(int id)
        {
            return access.Delete(id);
        }
        public int del_list(string where)
        {
            return access.del_list(where);
        }

        public NewsModel GetModel(int Id)
        {
            return access.GetOne(Id);
        }

        public List<NewsModel> getlist()
        {
            return access.getlist();
        }

        public List<NewsWebModel> getlistbycolId(int colID)
        {
            return access.getlistbycolId(colID);
        }
        /// <summary>
        /// 得到专题的前count条数据
        /// </summary>
        /// <param name="count"></param>
        /// <param name="columnid"></param>
        /// <returns></returns>
        public List<NewsWebModel> getTopList(int count, int columnid, string where)
        {
            return access.getTopList(count, columnid,where);
       }
         public List<NewsModel> getmodelbywhere(string where)
        {
            return access.getmodelbywhere(where);
         }
        public  int update_delete(NewsModel model)
        {
            return access.update_delete(model);
        }
        public int update(NewsModel model)
        {
            return access.update(model);
        }
        public List<pageModel> GetPageList(int pageIndex, int pageSize, ref int allcount, ref int pagecount, string strwhere)
        {
            return access.GetPageList(pageIndex, pageSize, ref allcount, ref pagecount, strwhere);
        }
        public List<NewsWebModel> GetwebPageList(int pageIndex, int pageSize, ref int allcount, ref int pagecount, string strwhere)
        {
            return access.GetwebPageList(pageIndex, pageSize, ref allcount, ref pagecount, strwhere);
        }
    }
}
