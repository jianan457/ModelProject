using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompanyModelProject.Model;
namespace CompanyModelProject.IServices
{
    public interface IbaseInfoServices
    {

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Insert(baseInfoModel model);

        /// <summary>
        /// select one
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        baseInfoModel GetModel(int Id);

        int update(baseInfoModel model);

        List<baseInfoModel> Getlist();
    }
}
