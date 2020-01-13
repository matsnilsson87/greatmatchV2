using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace gr8Match.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Förnamn")]
        [RegularExpression(@"^[a-zåäöA-ZÅÄÖ\-]+$", ErrorMessage = "Du kan bara använda bokstäver")]
        [Required(ErrorMessage = "Du måste fylla i ett förnamn")]
        public string FirstName { get; set; }

        [Display(Name = "Efternamn")]
        [RegularExpression(@"^[a-zåäöA-ZÅÄÖ]+$", ErrorMessage = "Du kan bara använda bokstäver")]
        [Required(ErrorMessage = "Du måste fylla i ett efternamn")]
        public string LastName { get; set; }

        [Display(Name = "Födelsedatum")]
        [Required(ErrorMessage = "Du måste fylla i ett födelsedatum")]
        [Column(TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Profilbild")]
        public string ProfileImage { get; set; }

        public bool Active { get; set; }
        public string IdentityID { get; set; }

        [InverseProperty("User1")]
        [XmlIgnore]
        public ICollection<Visitor> Visitors1 { get; set; }

        [InverseProperty("User2")]
        [XmlIgnore]
        public ICollection<Visitor> Visitors2 { get; set; }

        [InverseProperty("User1")]
        [XmlIgnore]
        public ICollection<FriendRequest> Requests1 { get; set; }
        [InverseProperty("User2")]
        [XmlIgnore]
        public ICollection<FriendRequest> Requests2 { get; set; }

        [InverseProperty("User1")]
        [XmlIgnore]
        public ICollection<Posts> Posts1 { get; set; }
        [InverseProperty("User2")]
        [XmlIgnore]
        public ICollection<Posts> Posts2 { get; set; }

        [InverseProperty("User1")]
        [XmlIgnore]
        public ICollection<Image> Images { get; set; }

        [InverseProperty("User")]
        [XmlIgnore]
        public ICollection<FriendInCategory> FriendCategories { get; set; }
        [XmlIgnore]
        public ICollection<UserInterests> UserInterests { get; set; }
    }
}