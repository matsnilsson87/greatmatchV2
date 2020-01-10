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

        [Display(Name = "Intressen")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Du kan bara använda bokstäver")]
        [Required(ErrorMessage = "Du måste fylla i ett intresse")]
        public string Name { get; set; }

    }
}