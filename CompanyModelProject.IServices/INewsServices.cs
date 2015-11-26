using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompanyModelProject.Model;
namespace CompanyModelProject.IServices
{
    public interface INewsServices
    {

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Insert(NewsModel model);
        int Delete(int id);
        int update(NewsModel model);
        int del_list(string where);

        List<NewsWebModel> getTopList(int count, int columnid,string where);
        int update_delete(NewsModel model);
        /// <summary>
        /// select one
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        NewsModel GetModel(int Id);

        List<NewsModel> getlist();
        List<NewsWebModel> getlistbycolId(int colID);
        List<pageModel> GetPageList(int pageIndex, int pageSize, ref int allcount, ref int pagecount, string strwhere);

        List<NewsWebModel> GetwebPageList(int pageIndex, int pageSize, ref int allcount, ref int pagecount, string strwhere);
         
    }
}
