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
        public static string WriteFile(string strTitle, string strContent, string datetime)//文章详情
        {
            string path = HttpContext.Current.Server.MapPath("/WebHtml/" + DateTime.Now.ToString("yyyyMMdd"));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);///创建文件夹
            } 
            Encoding code = Encoding.GetEncoding("gb2312");
            // 读取模板文件
            string temp = HttpContext.Current.Server.MapPath("/Temple/NewsTemple.html");
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
            string htmlfilename = DateTime.Now.ToString("yyyyMMddHHmmss") + ".html";
            // 替换内容
            // 这时,模板文件已经读入到名称为str的变量中了
            //str = str.Replace("Title", strTitle); //模板页中的ShowArticle
            str = str.Replace("biaoti", strTitle);
            str = str.Replace("content", strContent);
            str = str.Replace("author", datetime);
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
            return path + htmlfilename;
        }


       
    }
}
