using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompanyModelProject.Model;
namespace CompanyModelProject.IServices
{
    public interface IAdminInfoServices
    {

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Insert(AdminInfoModel model);

        /// <summary>
        /// select one
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        AdminInfoModel GetModel(int Id);
        AdminInfoModel getLoginInfo(string username, string pwd);
        int update(AdminInfoModel model);
        int Getusername(string username);
        int update_PWD(string pwd, int Id);
        /// <summary>
        /// update userinfo
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>



        List<AdminInfoModel> GetList();
    }
}
