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
    public class AdminInfoServices : IAdminInfoServices
    {
        public static object obj = new object();

        private static IAdminInfoServices instance = null;

        private AdminInfoDataAccess access = new AdminInfoDataAccess();

        public AdminInfoServices() { }
        public static IAdminInfoServices getinstance()
        {
            if (instance == null)
            {
                lock (obj)
                {
                    instance = new AdminInfoServices();
                }
            }

            return instance;
        }

        public int Insert(AdminInfoModel model)
        {

            return access.Insert(model);
        }
        public AdminInfoModel getLoginInfo(string username, string pwd)
        {

            return access.GetloginInfo(username, pwd);
        }

        public int Getusername(string username)
        {
            if (access.Getusername(username) != null)
            {
                return access.Getusername(username).ID;
            }
            else
            {
                return 0;
            }
        }

        public AdminInfoModel GetModel(int Id)
        {
            return access.GetOne(Id);
        }

        public List<AdminInfoModel> GetList()
        {
            return access.getlist();
        }
        public int Delete(int Id)
        {
            return access.Delete(Id);
        }
        public int update_PWD(string pwd, int Id)
        {
            return access.update_PWD(pwd, Id);
        }
        public int update(AdminInfoModel model)
        {
            return access.update(model);
        }

    }
}
