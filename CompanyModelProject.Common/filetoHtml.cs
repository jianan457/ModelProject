using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace CompanyModelProject.Common
{
    public class FileToHtml
    {
        public static string WriteFile(string strTitle, string strContent, DateTime datetime,string Column,string brief,string pic)//文章详情
        { 
            string path = HttpContext.Current.Server.MapPath("/WebHtml/" + DateTime.Now.ToString("yyyyMMdd")+"/");
            string filepath = string.Empty;
            string temp = string.Empty;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);///创建文件夹
            }
            filepath = "/WebHtml/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            Encoding code = Encoding.GetEncoding("gb2312");
            // 读取模板文件
            if (Column == "名师介绍")
            {
                 temp = HttpContext.Current.Server.MapPath("/Temple/TeacherTemple.html");
            }
            else
            {
                 temp = HttpContext.Current.Server.MapPath("/Temple/NewsTemple.html");
            }
          
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
                HttpContext.Current.Response.Write(exp.Message);
                HttpContext.Current.Response.End();
                sr.Close();
            }
            string htmlfilename =datetime.ToString("yyyyMMddHHmmss") + ".html";
            // 替换内容
            // 这时,模板文件已经读入到名称为str的变量中了
            //str = str.Replace("Title", strTitle); //模板页中的ShowArticle
            str = str.Replace("$Title$", strTitle);
            str = str.Replace("$Main$", strContent);
            str = str.Replace("$AddTime$", datetime.ToString());
            str = str.Replace("$ColumnId$", Column);
            str = str.Replace("$ColumnName$", NumToChinese.getColumn(Column));
            if (Column == "名师介绍")
            {
                str = str.Replace("$MainBrief$", brief);
                str = str.Replace("$TitlePic$", pic);
                
            }
            // 写文件
            try
            {
                sw = new StreamWriter(path + htmlfilename, false, code);
                sw.Write(str);
                sw.Flush();
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.Message);
                HttpContext.Current.Response.End();
            }
            finally
            {
                sw.Close();
            }
            return filepath + htmlfilename;
        } 
    }
}
