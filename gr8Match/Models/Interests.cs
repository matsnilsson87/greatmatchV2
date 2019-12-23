using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace gr8Match.Models
{
    public class Interests
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}