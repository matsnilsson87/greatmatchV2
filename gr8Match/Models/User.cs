using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace gr8Match.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateOfBirth { get; set; }

        [ForeignKey("Image")]
        public int? ProfileImage { get; set; }
        public Image Image { get; set; }

        public bool Active { get; set; }
        public string IdentityID { get; set; }

        [InverseProperty("User1")]
        public ICollection<Visitor> Visitors1 { get; set; }
        [InverseProperty("User2")]
        public ICollection<Visitor> Visitors2 { get; set; }

        [InverseProperty("User1")]
        public ICollection<FriendRequest> Requests1 { get; set; }
        [InverseProperty("User2")]
        public ICollection<FriendRequest> Requests2 { get; set; }

        [InverseProperty("User1")]
        public ICollection<Posts> Posts1 { get; set; }
        [InverseProperty("User2")]
        public ICollection<Posts> Posts2 { get; set; }

        [InverseProperty("User1")]
        public ICollection<Image> Images { get; set; }

        public ICollection<UserInterests> UserInterests { get; set; }
    }
}