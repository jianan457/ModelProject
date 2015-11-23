using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompanyModelProject.Model;
namespace CompanyModelProject.IServices
{
    public interface IMessageServices
    { 
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Insert(MessageModel model);

        /// <summary>
        /// select one
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        MessageModel GetModel(int Id);

        int update(MessageModel model);
        int del_list(string where);
        List<MessageModel> Getlist();
        List<MessageModel> GetPageList(int pageIndex, int pageSize, ref int allcount, ref int pagecount, string strwhere);
    }
}
