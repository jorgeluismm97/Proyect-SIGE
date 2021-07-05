using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SiGe
{
    public class ReportViewModel
    {
        [Display(Name ="Fecha de inicio")]
        public DateTime startingDate { get; set; }
        [Display(Name = "Fecha fin")]
        public DateTime endingDate { get; set; }
    }
}
