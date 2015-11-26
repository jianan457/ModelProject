﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CompanyModelProject.Model;
using CompanyModelProject.Services;
using CompanyModelProject.Common;
using System.Web;
using System.IO;
namespace CompanyModelProject.Controllers
{
    public class NewsController : BaseController
    {

        ColumnServices service = new ColumnServices();
        NewsServices newsService = new NewsServices();
        public ActionResult NewsAdd()
        {
            ViewBag.RightsId = RightsId; 
            ViewBag.liId = "columnli3";
            ViewBag.keys = KeyWords;
            ViewBag.dec = Description;
            ViewBag.admin = LoginName;
            ViewBag.title = Title;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult NewsAdd(NewsModel model)
        {
            if (ModelState.IsValid)
            {
                model.Creater = LoginName; 
                 string htmlurl= FileToHtml.WriteFile(model.Title, model.Main, model.CreateTime.ToString());//生成静态文件
                 if (htmlurl!=null)
                    {
                        model.HtmlUrl = htmlurl;
                        int r = newsService.Insert(model);//加入数据库
                        if (r!=0)
                        {
                            return RedirectToAction("NewsList");
                        }
                        else
                        {
                            return RedirectToAction("AddNews");
                        }
                    }
                    
            }
            return View();
        }

        public ActionResult NewsList()
        {
            ViewBag.RightsId = RightsId; 
            ViewBag.liId = "columnli4";
            ViewBag.admin = LoginName;
            ViewBag.title = Title;
            List<ColumnModel> fristlist = service.getFristColumnlist_Index();//一级 
            ViewData["fristlist"] = new SelectList(fristlist, "ID", "ColumnName");
            int page = RequestQueryString.GetQueryInt("page", 1);
            string colid = RequestQueryString.GetQueryString("upid", "");
            string levl = RequestQueryString.GetQueryString("levl", "");
            string key = RequestQueryString.GetQueryString("key", "");
            int size = 20;
            int total = 0;
            int count = 0;
            string strwhere = null;
            List<string> urllist = new List<string>();
            string url = "/News/NewsList?page={0}";
            if (levl != "" && colid!="")
            {
                if (int.Parse(levl) == 0)///一级
                {
                    string sb="sort="+int.Parse(colid);
                    List<AllColumnModel> list= service.getAllList(sb.ToString());
                    StringBuilder sbcolId = new StringBuilder();
                    string colids = string.Empty;
                    if (list!=null&&list.Count>0)
                    { 
                        foreach (var item in list)
                        {
                            sbcolId.Append(item.ID+",");  
                        }
                      colids=  sbcolId.ToString().Substring(0, sbcolId.Length - 1);
                      strwhere += " and ColumnId in (" + colids + ")"; 
                    } 
                }
                if (int.Parse(levl) == 1)///二级
                {
                    string scontion = "a.ID=" + int.Parse(colid) + " or upid=" + int.Parse(colid);
                    List<AllColumnModel> list = service.getAllList(scontion.ToString());
                    StringBuilder sbcolId = new StringBuilder();
                    string colids = string.Empty;
                    if (list != null && list.Count > 0)
                    {
                        foreach (var item in list)
                        {
                            sbcolId.Append(item.ID + ",");
                        }
                        colids = sbcolId.ToString().Substring(0, sbcolId.Length - 1);
                        strwhere += " and ColumnId in (" + colids + ")";
                    } 
                }
                if (int.Parse(levl) == 2)///三级
                {
                    strwhere += " and ColumnId =" + int.Parse(colid);
                    url += "&upid=" + int.Parse(colid);
                }
                url += "&upid=" + int.Parse(colid) + "&levl="+int.Parse(levl);
            }
            if (key != "")
            {
                strwhere += " and Title like '%" + key + "%' ";
                url += "&key=" + key + "";
            } 
            List<pageModel> List = newsService.GetPageList(page, size, ref total, ref count, strwhere);
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

        public ActionResult fristHandler()
        {
            List<ColumnModel> fristlist = service.getFristColumnlist_Index();//一级 
            StringBuilder sbf = new StringBuilder();
            if (fristlist != null && fristlist.Count > 0)
            {
                sbf.Append("[");
                foreach (var item in fristlist)
                {
                    sbf.Append("{");
                    sbf.Append("'Id':'" + item.ID + "','columnName':'" + item.ColumnName + "'");
                    sbf.Append("},");
                }
                sbf.Append("]");
                return Json(new { code = 0, data = sbf.ToString() }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { code = 1 }, JsonRequestBehavior.AllowGet);
            }
           
        }
        public ActionResult secColHandler()
        {
            string cID = RequestQueryString.GetQueryString("val");
            if (cID != "" && cID != null)
            {
                List<ColumnModel> sonlist = service.getSonList(int.Parse(cID));//一级 
                StringBuilder sb = new StringBuilder();
                if (sonlist != null && sonlist.Count > 0)
                {
                    sb.Append("[");
                    foreach (var item in sonlist)
                    {
                        sb.Append("{");
                        sb.Append("'Id':'" + item.ID + "','columnName':'" + item.ColumnName + "'");
                        sb.Append("},");
                    }
                    sb.Append("]");
                    return Json(new { code = 0, data = sb.ToString() }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json(new { code = 1 }, JsonRequestBehavior.AllowGet);
                } 
            }
            return View();
        }

        public ActionResult UploadHandler()
        {
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            string imgPath = "";
            string filepath = "";
            string filename = "";//文件名字
            if (hfc.Count > 0)
            {
                string ex = System.IO.Path.GetExtension(hfc[0].FileName).ToLower();//文件扩展名 
                ///缩略图
                if (hfc.Keys[0] == "newspic")
                {
                    filepath = "/Upload/News/";
                    filename = System.DateTime.Now.ToString("yyyyMMddHHmmss") + ex;
                } 
              
                imgPath = Server.MapPath(filepath + filename);
                hfc[0].SaveAs(imgPath);

                return Content(filepath + filename);
            }
            return Json(new { code = 1, message = "fail" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UploadEditorImgHandler()
        {
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            string imgPath = "";
            string filepath = "";
            string filename = "";//文件名字
            if (hfc.Count > 0)
            {
                string ex = System.IO.Path.GetExtension(hfc[0].FileName).ToLower();//文件扩展名 
                filepath = "/Upload/EditorNewsImg/";
                    filename = System.DateTime.Now.ToString("yyyyMMddHHmmss") + ex;
                imgPath = Server.MapPath(filepath + filename);
                hfc[0].SaveAs(imgPath);
                return Json(new { error = 0, message = "上传成功", url = filepath + filename });
            }
            return Json(new { code = "1", message = "上传失败", url = "" }); 
        }

        public ActionResult NewsModtify(int id)
        {
            ViewBag.RightsId = RightsId; 
            ViewBag.admin = LoginName;
            ViewBag.title = Title;
            ViewBag.liId = "columnli4";
            NewsModel model = newsService.GetModel(id);
            if (model != null)
            {
                ViewBag.img = model.picUrl;
                ColumnModel upmodel = service.GetModel(model.ColumnId);
                if (upmodel != null)
                {
                    List<AllColumnModel> list = service.getAllList(" a.ID=" + model.ColumnId);//得到栏目的等级 一级ID信息
                    if (list != null && list.Count > 0)
                    {
                        if (list[0].level==0)//1级
                        {
                            ViewBag.level = 0;
                            ViewBag.FcolId = model.ColumnId;
                        }
                        if (list[0].level == 1)//2级
                        {
                            ViewBag.level = 1;
                            ViewBag.FcolId = list[0].Upid;
                            ViewBag.ScolId = list[0].ID;
                        }
                        if (list[0].level == 2)//3级
                        {
                            ViewBag.level = 2;
                            ViewBag.FcolId = list[0].sort;
                            ViewBag.ScolId = list[0].Upid;
                            ViewBag.ThridId = list[0].ID; 
                        } 
                    }
                  
                }
                return View(model);
            }
            return View();
             
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult NewsModtify(NewsModel model)
        {
            model.Creater = LoginName;
           string htmlurl= FileToHtml.WriteFile(model.Title, model.Main, model.CreateTime.ToString());//生成静态文件
           if (htmlurl != null)
           {
               model.HtmlUrl = htmlurl;
               int result = newsService.update(model);//修改如数据库
               if (result != 0)
               {
                   ///修改成功之后  
                   return RedirectToAction("NewsList");//返回到列表
               }
               else
               {
                   return RedirectToAction("NewsModtify");

               }
           }
            return View();
        }

        public ActionResult DelNewsHandler()
        {
            string tID = RequestQueryString.GetQueryString("v");
            NewsModel model = newsService.GetModel(int.Parse(tID));
            if (model != null)
            {
                model.isDel = true;
               int r= newsService.update_delete(model);
               if (r!=0)
               {
                     return Json(new { code = 0, message = "删除成功！" }, JsonRequestBehavior.AllowGet);
               }
               else
               {
                   return Json(new { code =2, message = "删除失败！" }, JsonRequestBehavior.AllowGet);
               }
            }
            else
            {
                return Json(new { code = 1, message = "没有要删除的数据！" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DelNewslistHandler()
        {
            string tID = RequestQueryString.GetQueryString("v");
            if (!string.IsNullOrEmpty(tID))
            {
                tID = tID.Substring(0, tID.Length - 1);
            } 
                int r = newsService.del_list(tID);
                if (r != 0)
                {
                    return Json(new { code = 0, message = "操作成功，成功删除"+r+"条数据！" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { code = 2, message = "删除失败！" }, JsonRequestBehavior.AllowGet);
                }
        }

        /// <summary>
        /// 首页生成静态页
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexToHtml()
        {
            ViewBag.RightsId = RightsId;
            ViewBag.admin = LoginName;
            ViewBag.title = Title;
            ViewBag.liId = "columnli11";
            return View();
        }
        public ActionResult CreatHtmlHandler()
        {
            ///新闻动态
            List<NewsWebModel> list1 = newsService.getTopList(5, 18, " and IsIndexRecommond=1");
            StringBuilder sb1 = new StringBuilder();
            if (list1!=null&&list1.Count>0)
            {
                for (int i = 0; i < list1.Count; i++)
                {
                    ///拼接html
                }
            }
            //教务公告
            List<NewsWebModel> list2 = newsService.getTopList(8, 34, " and IsIndexRecommond=1");
            StringBuilder sb2 = new StringBuilder();
            if (list2 != null && list2.Count > 0)
            {
                for (int i = 0; i < list2.Count; i++)
                {
                    ///拼接html
                }
            }
            //简介
            List<NewsWebModel> list3 = newsService.getTopList(1, 20, "");
            StringBuilder sb3 = new StringBuilder();
            if (list3 != null && list3.Count > 0)
            {
                for (int i = 0; i < list3.Count; i++)
                {
                    ///拼接html
                }
            }
            //学员风采
            List<NewsWebModel> list4 = newsService.getTopList(6, 36, "");
            StringBuilder sb4 = new StringBuilder();
            if (list4 != null && list4.Count > 0)
            {
                for (int i = 0; i < list4.Count; i++)
                {
                    ///拼接html
                }
            }
            //学员企业
            List<NewsWebModel> list5 = newsService.getTopList(6, 37, "");
            StringBuilder sb5 = new StringBuilder();
            if (list5 != null && list5.Count > 0)
            {
                for (int i = 0; i < list5.Count; i++)
                {
                    ///拼接html
                }
            }
            //管理资讯
            List<NewsWebModel> list6 = newsService.getTopList(6, 38, "");
            StringBuilder sb6 = new StringBuilder();
            if (list6 != null && list6.Count > 0)
            {
                for (int i = 0; i < list6.Count; i++)
                {
                    ///拼接html
                }
            }
            //名师介绍(图片)
            List<NewsWebModel> list7 = newsService.getTopList(6, 35, " and IsIndexRecommond=1");
            StringBuilder sb7 = new StringBuilder();
            if (list7 != null && list7.Count > 0)
            {
                for (int i = 0; i < list7.Count; i++)
                {
                    ///拼接html
                }
                
            }
            //新闻图片(图片)
            List<NewsWebModel> list8 = newsService.getTopList(6, 41, "");
            StringBuilder sb8 = new StringBuilder();
            if (list8 != null && list8.Count > 0)
            {
                for (int i = 0; i < list8.Count; i++)
                {
                    ///拼接html
                }
            }
            bool issucess = indextofile(sb1.ToString(), sb2.ToString(), sb3.ToString(), sb4.ToString(), sb5.ToString(), sb6.ToString(), sb7.ToString(), sb8.ToString());
            if (issucess)
            {
                  return Json(new { code = 0, message = "首页已生成！" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { code = 1, message = "首页生成失败！" }, JsonRequestBehavior.AllowGet);
            }
          
        }
        private bool indextofile(string sb1, string sb2, string sb3, string sb4, string sb5, string sb6, string sb7, string sb8)
        {
            string path = Server.MapPath("/Index.html");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);///创建文件夹
            }
            Encoding code = Encoding.GetEncoding("gb2312");
            // 读取模板文件
            string temp = Server.MapPath("/Temple/Index.html");
            StreamReader sr = null;
            StreamWriter sw = null;
            string str = "";
            try
            {
                sr = new StreamReader(temp, code);
                str = sr.ReadToEnd(); // 读取文件
            }
            catch (Exception exp)
            {
                Response.Write(exp.Message);
                Response.End();
                sr.Close();
            }
           
            str = str.Replace("newslist1", sb1);
            str = str.Replace("newslist2", sb2);
            str = str.Replace("newslist3", sb3);
            str = str.Replace("newslist4", sb4);
            str = str.Replace("newslist5", sb5);
            str = str.Replace("newslist6", sb6);
            str = str.Replace("newslist7", sb7);
            str = str.Replace("newslist8", sb8);
            try
            {
                sw = new StreamWriter(path , false, code);
                sw.Write(str);
                sw.Flush();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                Response.End();
            }
            finally
            {
                sw.Close();
            }
            return true;
        }
    }
}
