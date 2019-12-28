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
        public int ID { get; set; }
        public string Text { get; set; }
        public DateTime Datum { get; set; }
        [ForeignKey("User")]
        public int WrittenBy { get; set; }
        [ForeignKey("User")]
        public int WrittenTo { get; set; }
    }
}