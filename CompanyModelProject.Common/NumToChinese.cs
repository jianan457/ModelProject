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
    }
}
