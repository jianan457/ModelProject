using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CompanyModelProject.Model
{
    public class RightsOfUserModel
    {
        public int ID { get; set; } 
        public int UserId { get; set; }
        public string RightIds { get; set; }
        public bool IsDel { get; set; }

    }
}
