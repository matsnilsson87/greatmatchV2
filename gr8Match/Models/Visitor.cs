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
        public int Id { get; set; }

        [ForeignKey("User1")]
        public int VisitorId { get; set; }
        public User User1{ get; set; }

        [ForeignKey("User2")]
        public int VisitedProfile { get; set; }
        public User User2 { get; set; }

        public DateTime Date { get; set; }

        

        
        

    }
}