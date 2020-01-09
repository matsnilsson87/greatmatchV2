using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace gr8Match.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
       
        public string CategoryName { get; set; }

        [InverseProperty("Category")]
        public ICollection<FriendInCategory> Categories { get; set; }
    }
}