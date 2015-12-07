using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompanyModelProject.Common
{
     public  class NumToChinese
    {
         public NumToChinese() { }
       public static string getStatic(int tId)
       {
           string typename = string.Empty;
           switch (tId)
           {
               case 0: typename = "一级栏目";
                   break;
               case 1: typename = "二级栏目";
                   break;
               case 2: typename = "三级栏目";
                   break; 
               default:
                   break;
           }
           return typename;
       }


       public static string getColumn(string cId)
       {
           string Columnname = string.Empty;
           switch (cId)
           {
               case "学院简介": Columnname = "Summary";
                   break;
               case "招生简章": Columnname = "Enrollment";
                   break;
               case "新闻动态": Columnname = "NewsList";
                   break;
               case "教务公告": Columnname = "StudyList";
                   break;
               case "名师介绍": Columnname = "TeacherList";
                   break;
               case "常见问题": Columnname = "ProblemList";
                   break;
               case "联系我们": Columnname = "ContactUs";
                   break;
               case "企业内训": Columnname = "Training";
                   break;
               case "学员企业": Columnname = "Stuenterprise";
                   break;
               case "学员风采": Columnname = "StustyleList";
                   break;
               case "管理资讯": Columnname = "ManagementInfo";
                   break;
               default:
                   break;
           }
           return Columnname;
       } 
       
    }
}
