using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace gr8Match.Models
{
    public class Image
    {   [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string ImgPath { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
    }
}