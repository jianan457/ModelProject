using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CompanyModelProject.Model
{
  public  class RegisterModel
    {

      public int  Id { get; set; }
      [Required(ErrorMessage = "用户名必须填写")]
      [StringLength(18, MinimumLength = 6, ErrorMessage = "至少包含6个字符")]
      [RegularExpression(@"^[a-zA-Z]\w{5,17}$", ErrorMessage = "以字母开头，只能包含字符、数字和下划线")]
      [Remote("CheckUserName", "Account", HttpMethod = "POST", ErrorMessage = "用户名已经存在")]
        public string AdminName { get; set; }

        [Required(ErrorMessage = "请输入密码")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "请输入6-12位密码")]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "用户名为英文与数字组合！")]
      
        public string Pwd { get; set; }

        [Required(ErrorMessage = "请再次输入密码")]
        [Compare("Pwd", ErrorMessage = "两次密码输入不一致")]
        public string comfirmPwd { get; set; }

        [Required(ErrorMessage = "请输入邮箱地址")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "请输入有效的邮箱地址！")]
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
    }
}
