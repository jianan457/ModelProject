using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CompanyModelProject.Model
{
    public class AdminInfoModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "请填写用户名")]

        public string AdminName { get; set; }

        [Required(ErrorMessage = "请填写密码")]
        public string Pwd { get; set; }

        [Required(ErrorMessage = "请输入邮箱地址")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "请输入有效的邮箱地址！")]
      
        public string Email { get; set; }

        public bool IsDel { get; set; }

        public bool IsShow { get; set; }
    }
}
