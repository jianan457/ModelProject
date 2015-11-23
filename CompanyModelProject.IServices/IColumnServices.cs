using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompanyModelProject.Model;
namespace CompanyModelProject.IServices
{
  public  interface IColumnServices
    {
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Insert(ColumnModel model);

        /// <summary>
        /// select one
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        ColumnModel GetModel(int Id);
        int Delete(int id);
        int Delete_list(string ids);
        int update(ColumnModel model);
        List<ColumnModel> getFristColumnlist_Index();
        int checkname(string name);
        List<AllColumnModel> getAllList(string where);

        List<ColumnModel> getSonList(int CId);
        List<ColumnModel> getFristColumnlist(int upid);
    }
}
