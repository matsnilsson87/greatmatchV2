using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace gr8Match.Models
{
    public class Image
    {   [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        [DisplayName("Ladda upp profilbild")]
        public string ImgPath { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
        

        [ForeignKey("User1")]
        public int UserId { get; set; }
        public User User1 { get; set; }

        [InverseProperty("Image")]
        public ICollection<User> Users { get; set; }
    }
}