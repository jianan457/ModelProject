using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CompanyModelProject.Model
{
    public class ColumnModel
    {
        public int ID { get; set; }
        public int Upid { get; set; }


         [Required(ErrorMessage = "请填写栏目名")]
         //[Remote("CheckColName", "Manager", HttpMethod = "POST", ErrorMessage = "栏目名已经存在")]
         public string ColumnName { get; set; }
        public DateTime AddTime { get; set; }
        public string AddUser { get; set; }

           [Required(ErrorMessage = "请填写排序数字")]
           [RegularExpression(@"^[0-9]*$", ErrorMessage = "请输入有效数字！")]
        public int Orders { get; set; }
            [Required(ErrorMessage = "是否在首页显示？")]
        /// <summary>
        /// 是否在首页显示
        /// </summary>
        public bool IsShow { get; set; }
        /// <summary>
        /// 是否可用  0:否 1 :是
        /// </summary>
        public bool IsDel { get; set; }
    }
}
