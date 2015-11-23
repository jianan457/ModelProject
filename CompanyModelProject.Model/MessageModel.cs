using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CompanyModelProject.Model
{
    public class MessageModel
    {
        public int ID { get; set; }

         [Required(ErrorMessage = "请填写姓名")]
        public string username { get; set; }

         [Required(ErrorMessage = "内容不能为空")]
        public string main { get; set; }
        public DateTime addtime { get; set; }
          [Required(ErrorMessage = "邮箱不能为空")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "请输入有效的邮箱地址！")]
        public string Email { get; set; }

        [Required(ErrorMessage = "手机号码不能为空")]
        [RegularExpression(@"^1[34587][0-9]{9}$", ErrorMessage = "手机号格式不正确")]
        public string phone { get; set; }

        public string QQ { get; set; }

        public int Orders { get; set; }
        /// <summary>
        /// 是否在首页显示
        /// </summary>
        public bool IsShow { get; set; }
        /// <summary>
        /// 是否可用  0:否 1 :是
        /// </summary>
        public bool isDel { get; set; }

    }
}
