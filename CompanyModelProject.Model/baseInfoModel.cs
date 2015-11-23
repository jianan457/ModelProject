using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CompanyModelProject.Model
{
    public class baseInfoModel
    {
        public int ID { get; set; }
         [Required(ErrorMessage = "请填写网站名称")]
        public string webName { get; set; }
         [Required(ErrorMessage = "请填写网站域名")]

        public string domain { get; set; }
         [Required(ErrorMessage = "请填写网站关键字")]

        public string keywords { get; set; }
         [Required(ErrorMessage = "请填写网站描述")]

        public string description { get; set; }
    }
}
