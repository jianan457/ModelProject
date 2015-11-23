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
    public class baseInfoServices : IbaseInfoServices
    {
        public static object obj = new object();

        private static IbaseInfoServices instance = null;

        private baseInfoDataAccess access = new baseInfoDataAccess();

        public baseInfoServices() { }
        public static IbaseInfoServices getinstance()
        {
            if (instance == null)
            {
                lock (obj)
                {
                    instance = new baseInfoServices();
                }
            }

            return instance;
        }

        public int Insert(baseInfoModel model)
        {

            return access.Insert(model);
        }



        public baseInfoModel GetModel(int Id)
        {
            return access.GetOne(Id);
        }


        public List<baseInfoModel> Getlist()
        {
            return access.Getlist();
        }

        public int update(baseInfoModel model)
        {
            return access.update(model);
        }

    }
}
