using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace gr8Match.Models
{
    public class UserInterests
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("User1")]
        public int UserId { get; set; }
        public User User1 { get; set; }

        [ForeignKey("Interests")]
        public int Interest { get; set; }
        public Interests Interests { get; set; }
    }
}