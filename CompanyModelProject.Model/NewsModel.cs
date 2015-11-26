using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
namespace CompanyModelProject.Model
{
    public class NewsModel
    {
        public int ID { get; set; }
        public int ColumnId { get; set; }
        [Required(ErrorMessage = "请填写标题")]
        public string Title { get; set; }
        public string picUrl { get; set; }
        public string BriefMain { get; set; }
        public string Main { get; set; }
        public string MainWithout { get; set; }
        public string fromUrl { get; set; }
        public string HtmlUrl { get; set; }
         [Required(ErrorMessage = "请填写排序数字")]
        public int orders { get; set; }
        public string Creater { get; set; }
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 是否可用  0:否 1 :是
        /// </summary>
        public bool isDel { get; set; }
        /// <summary>
        /// 是否首页推荐
        /// </summary>
        public bool IsClomnrecommond { get; set; }
        public bool IsIndexRecommond { get; set; }

        public int Clicks { get; set; }
    }
}
