using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompanyModelProject.Model;
namespace CompanyModelProject.IServices
{
    public interface IRightsServices
    {

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Insert(RightsModel model);

        /// <summary>
        /// select one
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        List<RightsModel> Getonelist(int upid);

        int update(RightsModel model); 

        List<RightsModel> GetList();
    }
}
