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
    public class RightsServices : IRightsServices
    {
        public static object obj = new object();

        private static IRightsServices instance = null;

        private RightsDataAccess access = new RightsDataAccess();

        public RightsServices() { }
        public static IRightsServices getinstance()
        {
            if (instance == null)
            {
                lock (obj)
                {
                    instance = new RightsServices();
                }
            }

            return instance;
        }

        public int Insert(RightsModel model)
        {

            return access.Insert(model);
        }
         
        public List<RightsModel> Getonelist(int upid)
        {
            return access.Getonelist(upid);
        } 

        public int update(RightsModel model)
        {
            return access.update(model);
        }

        public List<RightsModel> GetList()
        {
            return access.Getlist();
        }

    }
}
