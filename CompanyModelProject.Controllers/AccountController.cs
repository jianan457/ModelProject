using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CompanyModelProject.Model;
using CompanyModelProject.Services;
using System.Web.UI;
using Controllers.Filter;
using CompanyModelProject.Common;
namespace CompanyModelProject.Controllers
{
    public class AccountController : BaseController
    {
        AdminInfoServices service = new AdminInfoServices();
        RightsOfUserServices rou = new RightsOfUserServices();
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Regisiter()
        {
            ViewBag.RightsId = RightsId;
            ViewBag.liId = "columnli6";
            ViewBag.admin = LoginName;
            //ViewBag.keys = KeyWords;
            //ViewBag.dec = Description;
            ViewBag.title = Title;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Regisiter(RegisterModel model)
        {

            if (ModelState.IsValid)
            {
                AdminInfoModel m = new AdminInfoModel();
                if (model != null)
                {
                    m.AdminName = model.AdminName;
                    m.Email = model.Email;
                    m.Pwd = model.Pwd;
                    m.IsDel = false;
                }
                int result = service.Insert(m);
                if (result != 0)
                {
                    return RedirectToAction("UserList");//返回到列表
                }
                return RedirectToAction("Regisiter");
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(AdminInfoModel model)
        {
            AdminInfoModel m = service.getLoginInfo(model.AdminName, model.Pwd);
            if (m != null)
            {
               
                Common.CookieManager cm = new Common.CookieManager();
                cm.setCookie("userinfo", "name=" + m.AdminName + "&Id=" + m.ID + "&email=" + m.Email, DateTime.Now.AddHours(2), "www.model.com", "/");

                return RedirectToAction("Index", "Manager");//返回到列表
            }
            return RedirectToAction("Login");

        }

        public ActionResult loginoff()
        {
            string domain = "www.model.com";
            Common.CookieManager cm = new Common.CookieManager();
            cm.deleteCookie("userinfo", domain);
            return RedirectToAction("Login", "Account");

        }

        /// <summary>
        /// 验证用户是否可用
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]   //清除缓存
        public JsonResult CheckUserName(string UserName)
        {
            bool result = true;
            if (service.Getusername(UserName) != 0)
            {
                result = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [UserLoginFilter]
        public ActionResult UserList()
        {
            ViewBag.RightsId = RightsId;
            ViewBag.liId = "columnli6";
            ViewBag.admin = LoginName;
            ViewBag.title = Title;
            List<AdminInfoModel> list = service.GetList();
            if (list != null)
            {
                ViewBag.data = list;
            }
            else
            {
                ViewBag.data = null;
                ViewBag.mes = "暂无数据";
            }
            return View();
        }
        [UserLoginFilter]
        public ActionResult pwdModtify(int Id)
        {
            ViewBag.RightsId = RightsId;
            ViewBag.liId = "columnli6";
            ViewBag.admin = LoginName;
            ViewBag.title = Title;
            if (Id != 0)
            {
                ViewBag.UId = Id;
            }
            else
            {
                return RedirectToAction("UserList");//返回到列表
            }
            return View();
        }
        [UserLoginFilter]
        public ActionResult UserModtify(int id)
        {
            ViewBag.RightsId = RightsId; 
            ViewBag.liId = "columnli6";
            ViewBag.admin = LoginName;
            ViewBag.title = Title;
            AdminInfoModel model = service.GetModel(id);
            if (model != null)
            {
                return View(model);
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult UserModtify(AdminInfoModel model)
        {
            if (ModelState.IsValid)
            {
                int result = service.update(model);
                return RedirectToAction("UserList");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult EditPasswordHandler()
        {
            string pwd = RequestQueryString.GetFormString("pwd1");
            string uid = RequestQueryString.GetFormString("v");
            int result = service.update_PWD(pwd, int.Parse(uid));
            if (result != 0)
            {
                return Json(new { code = 0, message = "密码修改成功" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { code = 1, message = "密码修改失败!" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DelUserHandler()
        {
            string tID = RequestQueryString.GetQueryString("v");
            AdminInfoModel model = service.GetModel(int.Parse(tID));
            if (model != null)
            {

                if (service.Delete(model.ID) != 0)
                {
                    return Json(new { code = 0, message = "操作成功,已删除" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { code = 1, message = "操作失败！" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { code = 1, message = "操作失败！" }, JsonRequestBehavior.AllowGet);
            }
        }

        [UserLoginFilter]
        public ActionResult RightsAdd(int Id)
        {
            ViewBag.RightsId = RightsId; 
            RightsServices rights = new RightsServices(); 
            if (Id != 0)
            {
                ViewBag.liId = "columnli6";
                ViewBag.admin = LoginName;
                ViewBag.UserId = Id;
                ViewBag.title = Title;
                List<RightsModel> list1 = rights.Getonelist(8);
                List<RightsModel> list2 = rights.Getonelist(9);
                List<RightsModel> list3 = rights.Getonelist(10);
                if (list1 != null)
                {
                    ViewBag.data1 = list1;
                }
                if (list2 != null)
                {
                    ViewBag.data2 = list2;
                }
                if (list3 != null)
                {
                    ViewBag.data3 = list3;
                }
                return View();
            }
            return RedirectToAction("UserList");

        }

        public ActionResult AddRightsHandler()
        {
         
            string userid = RequestQueryString.GetQueryString("userid");
            string rightids = RequestQueryString.GetQueryString("v");
            int r=0;
            if (rightids != "" && rightids != null && userid!=null)
            {
                rightids = rightids.Substring(0, rightids.Length - 1);
                RightsOfUserModel model = new RightsOfUserModel();
                model.UserId = int.Parse(userid);
                model.RightIds = rightids;
             List<RightsOfUserModel> list = rou.GetList();
             if (list != null && list.Count > 0)
             {
                 List<RightsOfUserModel> newlist = list.Where(o => o.UserId == int.Parse(userid)).ToList();
                 if (newlist != null && newlist.Count > 0)
                 {
                     model.ID = newlist[0].ID;
                     r = rou.update(model);
                 }
                 else
                 {
                     r = rou.Insert(model);
                 }
             }
             else
             {
                 r = rou.Insert(model);
             }
             if (r != 0)
             {
                 return Json(new { code = 0, message = "操作成功" }, JsonRequestBehavior.AllowGet);
             }
             else
             {
                 return Json(new { code = 1, message = "操作失败" }, JsonRequestBehavior.AllowGet);
             }
            }
            return Json(new { code = 2, message = "数据异常" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getURList()
        {

            string userid = RequestQueryString.GetQueryString("v");
         
            List<RightsOfUserModel> list = rou.GetList();
            if (list!=null&&list.Count>0)
            {
                List<RightsOfUserModel>  model = list.Where(o => o.UserId == int.Parse(userid)).ToList();
                if (model!=null&&model.Count>0)
                {
                    string re = model[0].RightIds;
                    return Json(new { code = 0, data = re }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { code = 1, message = "没有权限" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { code = 2, message = "没有数据" }, JsonRequestBehavior.AllowGet);
            }
        }
    }

}
