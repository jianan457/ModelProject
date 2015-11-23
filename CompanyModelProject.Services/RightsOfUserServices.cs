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
    public class RightsOfUserServices : IRightsOfUserServices
    {
        public static object obj = new object();

        private static IRightsOfUserServices instance = null;

        private RightsOfUserDataAccess access = new RightsOfUserDataAccess();

        public RightsOfUserServices() { }
        public static IRightsOfUserServices getinstance()
        {
            if (instance == null)
            {
                lock (obj)
                {
                    instance = new RightsOfUserServices();
                }
            }

            return instance;
        }

        public int Insert(RightsOfUserModel model)
        {

            return access.Insert(model);
        }

        public RightsOfUserModel GetOne(int id)
        {
            return access.GetOne(id);
        }

        public int update(RightsOfUserModel model)
        {
            return access.update(model);
        }

        public List<RightsOfUserModel> GetList()
        {
            return access.Getlist();
        }

    }
}
