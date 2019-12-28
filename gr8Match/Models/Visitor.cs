using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace gr8Match.Models
{
    public class Visitor
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("User")]
        public int VisitorID { get; set; }
        [ForeignKey("User")]
        public int VisitedProfile { get; set; }
        public DateTime Date { get; set; }
      
    }
}