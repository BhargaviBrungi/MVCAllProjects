using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCAllProjects.Models
{
    public class Calendar
    {
        [Display(Name = "Enter Date")]
        public DateTime EnterDate { get; set; }
    }
}