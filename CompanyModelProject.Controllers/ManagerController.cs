using Controllers.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompanyModelProject.Model;
using CompanyModelProject.Services;
using System.Web.UI;
using CompanyModelProject.Common;

namespace CompanyModelProject.Controllers
{
    public class ManagerController : BaseController
    {
        ColumnServices service = new ColumnServices();
        baseInfoServices bservice = new baseInfoServices();
        MessageServices mess = new MessageServices();

        //
        // GET: /Manager/
        [UserLoginFilter]
        public ActionResult Index()
        {
            //ViewBag.keys = KeyWords;
            //ViewBag.dec = Description;
            ViewBag.title = Title;
            ViewBag.RightsId = RightsId;
            ViewBag.Id = Id;
            ViewBag.admin = LoginName;
            return View();
        }
        [UserLoginFilter]
        public ActionResult ColumnFristAdd()
        {
            ViewBag.RightsId = RightsId;
            ViewBag.liId = "columnli1";
            //ViewBag.keys = KeyWords;
            //ViewBag.dec = Description;
            ViewBag.title = Title;
            ViewBag.admin = LoginName;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult ColumnFristAdd(ColumnModel model)
        {
            if (ModelState.IsValid)
            {
                model.Upid = 0;
                model.AddUser = LoginName;
                int result = service.Insert(model);
                if (result != 0)
                {
                    return RedirectToAction("ColumnList");
                }
            }
            return View();
        }
        [UserLoginFilter]
        public ActionResult ColumnList()
        {
            ViewBag.RightsId = RightsId;
            ViewBag.liId = "columnli2";
            ViewBag.admin = LoginName;
            //ViewBag.keys = KeyWords;
            //ViewBag.dec = Description;
            ViewBag.title = Title;
            List<AllColumnModel> list = service.getAllList("");
            if (list != null)
            {
                ViewBag.data = list;
            }
            else
            {
                ViewBag.data = null;
                ViewBag.msg = "暂无数据";
            }
            return View();
        }

        [HttpPost]
        public JsonResult CheckColName(string ColumnName)
        {
            bool result = true;
            if (service.checkname(ColumnName) != 0)
            {
                result = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [UserLoginFilter]
        public ActionResult ColumnModtify(int id)
        {
            ViewBag.RightsId = RightsId;
            ViewBag.liId = "columnli2";
            //ViewBag.keys = KeyWords;
            //ViewBag.dec = Description;
            ViewBag.title = Title;
            ColumnModel model = service.GetModel(id);
            if (model != null)
            {
                ColumnModel upmodel = service.GetModel(model.Upid);//父级MODEL
                if (upmodel != null)
                {
                    List<ColumnModel> list = service.getSonList(upmodel.Upid);
                    if (list != null && list.Count > 0)
                    {
                        ViewData["uplist"] = new SelectList(list, "ID", "ColumnName");
                    }
                    else
                    {
                        ViewData["uplist"] = null;
                    }
                }
                return View(model);
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult ColumnModtify(ColumnModel model)
        {
            int result = service.update(model);
            return RedirectToAction("ColumnList");
        } 
        public ActionResult DelColHandler()
        {
            string tID = RequestQueryString.GetQueryString("v");
            ColumnModel model = service.GetModel(int.Parse(tID));
            if (model != null)
            {
                List<ColumnModel> list = service.getSonList(model.ID);//该栏目是否有子栏目
                if (list != null && list.Count > 0)//存在子栏目
                {
                    return Json(new { code = 1, message = "请先删除该栏目下的子栏目，再删除此栏目！" }, JsonRequestBehavior.AllowGet);
                }
                else
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

            }
            else
            {
                return Json(new { code = 1, message = "数据异常！" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DelCollistHandler()
        {
            string tID = RequestQueryString.GetQueryString("v");
            if (tID != "" && tID != null)
            {
                string[] colid = tID.Split(',');
                string a = string.Empty;
                foreach (var item in colid)
                {
                    if (item != "")
                    {
                        int r = service.Delete(int.Parse(item)); 
                    } 
                }
                return Json(new { code = 0, message = "操作成功,已删除" }, JsonRequestBehavior.AllowGet);

            }
            return View();
        }
        [UserLoginFilter]
        public ActionResult ColumnSonAdd(int id)
        {
            ViewBag.RightsId = RightsId;
            ViewBag.liId = "columnli2";
            //ViewBag.keys = KeyWords;
            //ViewBag.dec = Description;
            ViewBag.title = Title;
            ViewBag.admin = LoginName;
            ColumnModel model = service.GetModel(id);
            if (model != null)
            {
                ViewBag.upid = id;
                ViewBag.upname = model.ColumnName;
            }
            return View();
        }
        [HttpPost]
        public ActionResult ColumnSonAdd(ColumnModel model)
        {
            model.AddUser = LoginName;
            int re = service.Insert(model);
            if (re != 0)
            {
                return RedirectToAction("ColumnList");
            }
            return View();
        }

        [UserLoginFilter]
        public ActionResult WebBaseInfo()
        {
            ViewBag.RightsId = RightsId;
            ViewBag.liId = "columnli5";
            ViewBag.admin = LoginName;
            //ViewBag.keys = KeyWords;
            //ViewBag.dec = Description;
            ViewBag.title = Title;
            List<baseInfoModel> list = bservice.Getlist();
            baseInfoModel model = new baseInfoModel();
            if (list != null && list.Count > 0)
            {
                model = list[0];
            }
            else
            {
                model = null;
            }
            return View(model);
        }
        [HttpPost]

        public ActionResult WebBaseInfo(baseInfoModel model)
        {

            int r = 0;
            if (model.ID != 0)
            {
                r = bservice.update(model);
            }
            else
            {
                r = bservice.Insert(model);
            }

            return RedirectToAction("WebBaseInfo");
        }
           [UserLoginFilter]
        public ActionResult MessageList()
        {
            ViewBag.RightsId = RightsId;
            ViewBag.liId = "columnli7";
            //ViewBag.keys = KeyWords;
            //ViewBag.dec = Description;
            ViewBag.admin = LoginName;
            ViewBag.title = Title;
            int page = RequestQueryString.GetQueryInt("page", 1);
            int size = 20;
            int total = 0;
            int count = 0;
            string strwhere = null;
            List<string> urllist = new List<string>();
            string url = "/Manager/MessageList?page={0}";
            List<MessageModel> List = mess.GetPageList(page, size, ref total, ref count, strwhere);
            for (int i = 0; i < count; i++)
            {
                urllist.Add(string.Format(url, (i + 1)));
            }
            ViewBag.Url = urllist;
            ViewBag.PageIndex = page;
            ViewBag.webdata = List;
            ViewBag.count = total;
            ViewBag.admin = LoginName;
            if (List != null)
            {
                ViewBag.data = List;
            }
            else
            {
                ViewBag.data = null;
                ViewBag.meg = "暂无数据";
            }
            return View();
        }

        [UserLoginFilter]
        public ActionResult MessageAdd()
        {
            ViewBag.RightsId = RightsId;
            ViewBag.liId = "columnli7";
            //ViewBag.keys = KeyWords;
            //ViewBag.dec = Description;
            ViewBag.title = Title;
            ViewBag.admin =LoginName;
            return View();
        }
        [HttpPost]
        public ActionResult MessageAdd(MessageModel model)
        {

            int res = mess.Insert(model);
            if (res != 0)
            {
                return RedirectToAction("MessageList");
            }
            return View();
        }

        public ActionResult DelMesslistHandler()
        {
            string tID = RequestQueryString.GetQueryString("v");
            if (!string.IsNullOrEmpty(tID))
            {
                tID = tID.Substring(0, tID.Length - 1);
            }
            int r = mess.del_list(tID);
            if (r != 0)
            {
                return Json(new { code = 0, message = "操作成功，成功删除" + r + "条数据！" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { code = 2, message = "删除失败！" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DelmessHandler()
        {
            string tID = RequestQueryString.GetQueryString("v");
            MessageModel model = mess.GetModel(int.Parse(tID));
            if (model != null)
            {
                model.isDel = true;
                model.IsShow = false;
                if (mess.update(model) != 0)
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
                return Json(new { code = 2, message = "操作失败！原因：数据不存在！" }, JsonRequestBehavior.AllowGet);
            }


        }
    }
}
