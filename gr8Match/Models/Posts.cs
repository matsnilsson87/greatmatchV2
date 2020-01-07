using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace gr8Match.Models
{
    public class Posts
    {
       
        [Key]
        public int Id { get; set; }

        [Display(Name = "Meddelande")]
        public string Text { get; set; }
        public DateTime Datum { get; set; }
       

        [ForeignKey("User1")]
        public int WrittenBy { get; set; }
        public User User1 { get; set; }

        [ForeignKey("User2")]
        public int WrittenTo { get; set; }
        public User User2 { get; set; }

    }
}