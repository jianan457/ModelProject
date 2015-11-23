using CompanyModelProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CompanyModelProject.Common
{
   public  class UserManage
    {
        /// <summary>
        /// 获取当前登陆用户 cookie
        /// </summary>
        /// <returns></returns> 
       public AdminInfoModel GetLoginUserInfo()
        {
            AdminInfoModel model = new AdminInfoModel();

            CookieManager cm = new CookieManager();
            string userinfo = cm.getCookieValue("userinfo");   
            if (!string.IsNullOrEmpty(userinfo))
            {
                string[] arry = userinfo.Split('&');
                foreach (var item in arry)
                {
                    string[] arryItem = item.Split('=');
                    if (arryItem.Length == 2)
                    {
                        switch (arryItem[0].ToUpper())
                        {
                            case "NAME":
                                model.AdminName = arryItem[1].ToString();
                                break;
                           case "ID":
                                model.ID=int.Parse(arryItem[1].ToString());
                                break;
                           case "EMAIL":
                                model.Email = arryItem[1].ToString();
                                break;
                          
                          default:
                                break;
                        }
                    } 
                } 
            }
            else { return null; } 
            return model;
        }


     
    }
}
