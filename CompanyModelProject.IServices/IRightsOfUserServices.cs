using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompanyModelProject.Model;
namespace CompanyModelProject.IServices
{
    public interface IRightsOfUserServices
    {

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Insert(RightsOfUserModel model);

        /// <summary>
        /// select one
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        RightsOfUserModel GetOne(int id);

        int update(RightsOfUserModel model);

        List<RightsOfUserModel> GetList();
    }
}
