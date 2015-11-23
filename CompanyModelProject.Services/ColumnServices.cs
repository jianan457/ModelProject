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
 public  class ColumnServices:IColumnServices
    {
      public static object obj = new object();

        private static IColumnServices instance = null;

        private ColumnDataAccess access = new ColumnDataAccess();

        public ColumnServices() { }
        public static IColumnServices getinstance()
        {
            if (instance == null)
            {
                lock (obj)
                {
                    instance = new ColumnServices();
                }
            }

            return instance;
        }

        public int Insert(ColumnModel model)
        {

            return access.Insert(model);
        }

        public int checkname(string name)
        {

            if (access.checkname(name) != null)
            {
                return access.checkname(name).ID;
            }
            else
            {
                return 0;
            }
            
        }

        public ColumnModel GetModel(int Id)
        {
            return access.GetOne(Id);
        }

        public List<ColumnModel> getFristColumnlist(int upid)
        {
            return access.getFristColumnlist(upid);
        }
        public List<ColumnModel> getFristColumnlist_Index()
        {
            return access.getFristColumnlist_Index();
        }

        public   List<ColumnModel> getSonList(int CId)
        {
            return access.getSonList(CId);
        }
        public List<AllColumnModel> getAllList(string where)
        {
            return access.getAllList(where);
        }
        public int Delete(int Id)
        {
            return access.Delete(Id);
        }
        public int Delete_list(string Ids)
        {
            return access.Delete_list(Ids);
        }
        public int update(ColumnModel model)
        {
            return access.update(model);
        }

    }
}
