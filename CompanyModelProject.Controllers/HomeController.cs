using Controllers.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CompanyModelProject.Model;
using CompanyModelProject.Services;
using CompanyModelProject.Common;
namespace CompanyModelProject.Controllers
{
    public class HomeController : BaseController
    {
        NewsServices news = new NewsServices();
        
        public ActionResult Index()
        {
             ViewBag.keys = KeyWords;
             ViewBag.dec = Description;
            ViewBag.title = Title;
            //新闻动态（1+4）
            List<NewsWebModel> list1 = news.getTopList(5, 18, " and IsClomnrecommond=1");
            if (list1!=null&&list1.Count>0)
            {
                ViewBag.news = list1;
            }

            //教务公告
            if (getStudy() != null && getStudy().Count > 0)
            {
                ViewBag.study = getStudy();
            }
            else
            {
                ViewBag.studymsg = "暂无数据";
            }
            //简介
            List<NewsWebModel> list3 = news.getTopList(1, 20, "");
            if (list3!=null&&list3.Count>0)
            {
                ViewBag.dection = list3[0].BriefMain;
                ViewBag.dectionimg = list3[0].picUrl;
            }
            else
            {
                ViewBag.dectionmsg = "暂无数据";
            }
            //学员风采
            List<NewsWebModel> list4 = news.getTopList(6, 36, "");
            if (list4!=null&&list4.Count>0)
            {
                 ViewBag.student =list4;
            }
            else
            {
                ViewBag.studentmsg = "暂无数据";
            }
            //学员企业
            List<NewsWebModel> list5 = news.getTopList(6, 37, "");
            if (list5 != null && list5.Count > 0)
            {
                ViewBag.stucom = list5;
            }
            else
            {
                ViewBag.stucommsg = "暂无数据";
            }
            //管理资讯
            List<NewsWebModel> list6 = news.getTopList(6, 38, "");
            if (list6 != null && list6.Count > 0)
            {
                ViewBag.managenews = list6;
            }
            else
            {
                ViewBag.managenewsmsg = "暂无数据";
            }

            //名师介绍(图片)
            if (getTeacher() != null && getTeacher().Count > 0)
            {
                ViewBag.teacher = getTeacher();
            }
            else
            {
                ViewBag.teachermsg = "暂无数据";
            }
            //新闻图片(图片)
            List<NewsWebModel> list8 = news.getTopList(6, 41, "");
            if (list8 != null)
            {
                ViewBag.newPic = list8;
            }
            else
            {
                ViewBag.newPicmsg = "暂无数据";
            }
            return View();
        }


        public ActionResult NewsList()//新闻动态
        {

            //名师介绍(图片)
            if (getTeacher() != null && getTeacher().Count > 0)
            {
                ViewBag.teacher = getTeacher();
            }
            else
            {
                ViewBag.teachermsg = "暂无数据";
            }
            //教务公告
            if (getStudy() != null && getStudy().Count > 0)
            {
                ViewBag.study = getStudy();
            }
            else
            {
                ViewBag.studymsg = "暂无数据";
            }
            ViewBag.keys = KeyWords;
            ViewBag.dec = Description;
            ViewBag.title = Title;
            int page = RequestQueryString.GetQueryInt("page", 1);
            int size = 20;
            int total = 0;
            int count = 0;
            string strwhere = null;
            List<string> urllist = new List<string>();
            string url = "/Home/NewsList?page={0}";
            strwhere = " and ColumnId=18";
            List<NewsWebModel> List = news.GetwebPageList(page, size, ref total, ref count, strwhere);
            for (int i = 0; i < count; i++)
            {
                urllist.Add(string.Format(url, (i + 1)));
            }
            ViewBag.Url = urllist;
            ViewBag.PageIndex = page;
            ViewBag.webdata = List;
            ViewBag.count = total;
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
        public ActionResult StudyList()//教务公告
        {

            //名师介绍(图片)
            if (getTeacher() != null && getTeacher().Count > 0)
            {
                ViewBag.teacher = getTeacher();
            }
            else
            {
                ViewBag.teachermsg = "暂无数据";
            }
            //教务公告
            if (getStudy() != null && getStudy().Count > 0)
            {
                ViewBag.study = getStudy();
            }
            else
            {
                ViewBag.studymsg = "暂无数据";
            }
            ViewBag.keys = KeyWords;
            ViewBag.dec = Description;
            ViewBag.title = Title;
            int page = RequestQueryString.GetQueryInt("page", 1);
            int size = 20;
            int total = 0;
            int count = 0;
            string strwhere = null;
            List<string> urllist = new List<string>();
            string url = "/Home/StudyList?page={0}";
            strwhere = " and ColumnId=34";
            List<NewsWebModel> List = news.GetwebPageList(page, size, ref total, ref count, strwhere);
            for (int i = 0; i < count; i++)
            {
                urllist.Add(string.Format(url, (i + 1)));
            }
            ViewBag.Url = urllist;
            ViewBag.PageIndex = page;
            ViewBag.webdata = List;
            ViewBag.count = total;
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
        public ActionResult TeacherList()//名师
        {
            //名师介绍(图片)
            if (getTeacher() != null && getTeacher().Count > 0)
            {
                ViewBag.teacher = getTeacher();
            }
            else
            {
                ViewBag.teachermsg = "暂无数据";
            }
            //教务公告
            if (getStudy() != null && getStudy().Count > 0)
            {
                ViewBag.study = getStudy();
            }
            else
            {
                ViewBag.studymsg = "暂无数据";
            }
        
            ViewBag.keys = KeyWords;
            ViewBag.dec = Description;
            ViewBag.title = Title;
            int page = RequestQueryString.GetQueryInt("page", 1);
            int size = 20;
            int total = 0;
            int count = 0;
            string strwhere = null;
            List<string> urllist = new List<string>();
            string url = "/Home/TeacherList?page={0}";
            strwhere = " and ColumnId=35";
            List<NewsWebModel> List = news.GetwebPageList(page, size, ref total, ref count, strwhere);
            for (int i = 0; i < count; i++)
            {
                urllist.Add(string.Format(url, (i + 1)));
            }
            ViewBag.Url = urllist;
            ViewBag.PageIndex = page;
            ViewBag.webdata = List;
            ViewBag.count = total;
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

        public ActionResult Enrollment()//招生简章
        {
            //名师介绍(图片)
            if (getTeacher() != null && getTeacher().Count > 0)
            {
                ViewBag.teacher = getTeacher();
            }
            else
            {
                ViewBag.teachermsg = "暂无数据";
            }
            //教务公告
            if (getStudy() != null && getStudy().Count > 0)
            {
                ViewBag.study = getStudy();
            }
            else
            {
                ViewBag.studymsg = "暂无数据";
            }
        
            ViewBag.keys = KeyWords;
            ViewBag.dec = Description;
            ViewBag.title = Title;
            int page = RequestQueryString.GetQueryInt("page", 1);
            int size = 20;
            int total = 0;
            int count = 0;
            string strwhere = null;
            List<string> urllist = new List<string>();
            string url = "/Home/Enrollment?page={0}";
            strwhere = " and ColumnId=19";
            List<NewsWebModel> List = news.GetwebPageList(page, size, ref total, ref count, strwhere);
            for (int i = 0; i < count; i++)
            {
                urllist.Add(string.Format(url, (i + 1)));
            }
            ViewBag.Url = urllist;
            ViewBag.PageIndex = page;
            ViewBag.webdata = List;
            ViewBag.count = total;
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
      
        public ActionResult ProblemList()//常见问题
        {
            //名师介绍(图片)
            if (getTeacher() != null && getTeacher().Count > 0)
            {
                ViewBag.teacher = getTeacher();
            }
            else
            {
                ViewBag.teachermsg = "暂无数据";
            }
            //教务公告
            if (getStudy() != null && getStudy().Count > 0)
            {
                ViewBag.study = getStudy();
            }
            else
            {
                ViewBag.studymsg = "暂无数据";
            }
            ViewBag.keys = KeyWords;
            ViewBag.dec = Description;
            ViewBag.title = Title;
            int page = RequestQueryString.GetQueryInt("page", 1);
            int size = 20;
            int total = 0;
            int count = 0;
            string strwhere = null;
            List<string> urllist = new List<string>();
            string url = "/Home/Enrollment?page={0}";
            strwhere = " and ColumnId=39";
            List<NewsWebModel> List = news.GetwebPageList(page, size, ref total, ref count, strwhere);
            for (int i = 0; i < count; i++)
            {
                urllist.Add(string.Format(url, (i + 1)));
            }
            ViewBag.Url = urllist;
            ViewBag.PageIndex = page;
            ViewBag.webdata = List;
            ViewBag.count = total;
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
      
        public ActionResult ContactUs()//联系我们
        {
            ViewBag.keys = KeyWords;
            ViewBag.dec = Description;
            ViewBag.title = Title;
            if (getTeacher() != null && getTeacher().Count > 0)
            {
                ViewBag.teacher = getTeacher();
            }
            else
            {
                ViewBag.teachermsg = "暂无数据";
            }
            //教务公告
            if (getStudy() != null && getStudy().Count > 0)
            {
                ViewBag.study = getStudy();
            }
            else
            {
                ViewBag.studymsg = "暂无数据";
            }
            List<NewsModel> list = news.getmodelbywhere(" and ColumnId=40 ");
            if (list != null && list.Count > 0)
            {
                ViewBag.data = list[0];
            }
            else
            {
                ViewBag.data = null;
            }
            return View(ViewBag.data); 

        }
        
        public ActionResult Summary()//学院简介
        {
             ViewBag.keys = KeyWords;
             ViewBag.dec = Description;
            ViewBag.title = Title;
            if (getTeacher() != null && getTeacher().Count > 0)
            {
                ViewBag.teacher = getTeacher();
            }
            else
            {
                ViewBag.teachermsg = "暂无数据";
            }
            //教务公告
            if (getStudy() != null && getStudy().Count > 0)
            {
                ViewBag.study = getStudy();
            }
            else
            {
                ViewBag.studymsg = "暂无数据";
            }
            List<NewsModel> list = news.getmodelbywhere(" and ColumnId=20 ");
            if (list != null && list.Count > 0)
            {
                ViewBag.data = list[0];
            }
            else
            {
                ViewBag.data =null;
            }
            return View(ViewBag.data); 
        }

        public ActionResult Training()//企业内训 一篇文章
        {
            if (getTeacher() != null && getTeacher().Count > 0)
            {
                ViewBag.teacher = getTeacher();
            }
            else
            {
                ViewBag.teachermsg = "暂无数据";
            }
            //教务公告
            if (getStudy() != null && getStudy().Count > 0)
            {
                ViewBag.study = getStudy();
            }
            else
            {
                ViewBag.studymsg = "暂无数据";
            }
             ViewBag.keys = KeyWords;
             ViewBag.dec = Description;
            ViewBag.title = Title;
            List<NewsModel> list = news.getmodelbywhere(" and ColumnId=42 ");
            if (list != null && list.Count > 0)
            {
                ViewBag.d = list[0];
                return View(ViewBag.d); 
            }
            else
            {
                ViewBag.d = null;
                return RedirectToAction("Index");
            }
          

        }


        public ActionResult Message()
        {
             ViewBag.keys = KeyWords;
             ViewBag.dec = Description;
            ViewBag.title = Title;
            return View();
        }
       /// <summary>
       /// 在线报名
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       /// 
       
        public ActionResult AddMessage()
        {
            string main = RequestQueryString.GetQueryString("main");
            string name = RequestQueryString.GetQueryString("name");
            string plone = RequestQueryString.GetQueryString("plone");
            MessageServices mess = new MessageServices();
            MessageModel model = new MessageModel();
            model.phone = plone;
            model.username = name;
            model.main = main;
            int res = mess.Insert(model);
            if (res != 0)
            {
                return Json(new { code = 0, message = "在线报名成功，请等待系统确认！" }, JsonRequestBehavior.AllowGet); 
            }
            else
            {
                return Json(new { code = 1, message = "请稍后重试！" }, JsonRequestBehavior.AllowGet); 
            }
            
        } 
        /// <summary>
        /// 左侧名师
        /// </summary>
        /// <returns></returns>
        private List<NewsWebModel> getTeacher()
        {
            List<NewsWebModel> list = news.getTopList(6, 35, " and IsIndexRecommond=1");
            return list;
        }
        /// <summary>
        /// 教务公告
        /// </summary>
        /// <returns></returns>
        private List<NewsWebModel> getStudy()
        {
            List<NewsWebModel> list = news.getTopList(8, 34, " and IsIndexRecommond=1");
            return list;
        }

        public ActionResult getTeacherHandler()
        {

            List<NewsWebModel> list = getTeacher();
                StringBuilder sb = new StringBuilder();
                if (list != null && list.Count > 0)
                {
                    sb.Append("[");
                    foreach (var item in list)
                    {
                        sb.Append("{");
                        sb.Append("'Id':'" + item.ID + "','Name':'" + item.Title + "','pic':'"+item.picUrl +"','HtmlUrl':'"+item.HtmlUrl +"'");
                        sb.Append("},");
                    }
                    sb.Append("]");
                    return Json(new { code = 0, data = sb.ToString() }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json(new { code = 1 }, JsonRequestBehavior.AllowGet);
                }
            
            return View();
        }
        public ActionResult getstudyHandler()
        {

            List<NewsWebModel> list = getStudy();
            StringBuilder sb = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                sb.Append("[");
                foreach (var item in list)
                {
                    sb.Append("{");
                    sb.Append("'Id':'" + item.ID + "','Name':'" + item.Title + "','HtmlUrl':'" + item.HtmlUrl + "'"); 
                    sb.Append("},");
                }
                sb.Append("]");
                return Json(new { code = 0, data = sb.ToString() }, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(new { code = 1 }, JsonRequestBehavior.AllowGet);
            }

            return View();
        }
    }
}
