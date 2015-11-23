using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CompanyModelProject.Model
{
    public class RightsModel
    {
        public int ID { get; set; }
        public int upid { get; set; }
        
        public string RightsName { get; set; } 

        public int Orders { get; set; }
    }
}
