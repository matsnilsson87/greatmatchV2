using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace gr8Match.Models
{
    public class FriendRequest
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("User")]
        public int User1 { get; set; }
        [ForeignKey("User")]
        public int User2 { get; set; }
        public int Accepted   { get; set; }
        
    }
}