using Controllers.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
namespace CompanyModelProject.Controllers
{
    public class HomeController : BaseController
    {
         [UserLoginFilter]
       public ActionResult Index()
       {
        
           return View();
       }
    }
}
