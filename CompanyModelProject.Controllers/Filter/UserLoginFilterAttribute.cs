using CompanyModelProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CompanyModelProject.Common;
namespace Controllers.Filter
{
   public  class UserLoginFilterAttribute:ActionFilterAttribute
    { 
       public override void OnActionExecuting(ActionExecutingContext filterContext)
       {
           AdminInfoModel model = new UserManage().GetLoginUserInfo();
           if (model==null)
           {
               filterContext.Result = new RedirectResult("/Account/Login");
           }
           base.OnActionExecuting(filterContext);
       }
    }
}
