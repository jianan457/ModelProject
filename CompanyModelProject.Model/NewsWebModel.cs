using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace CompanyModelProject.Model
{
    public class NewsWebModel
    {
        public int ID { get; set; }
        public int ColumnId { get; set; }
        public string HtmlUrl { get; set; }
        public string Title { get; set; } 
        public int orders { get; set; } 
        public DateTime CreateTime { get; set; }
        public string BriefMain { get; set; }
        /// <summary>
        /// 是否可用  0:否 1 :是
        /// </summary>
        public bool isDel { get; set; }
        /// <summary>
        /// 是否首页推荐
        /// </summary>
        public bool IsClomnrecommond { get; set; }
        public bool IsIndexRecommond { get; set; } 
    }
}
