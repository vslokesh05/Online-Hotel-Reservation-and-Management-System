using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        public string AName { get; set; }
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username must not be empty"), MaxLength(255)]
        public string AUserName { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password must not be empty"), MaxLength(255)]
        public string APassword { get; set; }
        [Display(Name = "Contact number")]
        [Required(ErrorMessage = "Contact no must not be empty"), MaxLength(10)]
        [RegularExpression(@"[7-9]{1}[0-9]{9}")]
        public string APhoneNo { get; set; }
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Id must not be empty")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string AEmailId { get; set; }
        public ICollection<RoomInfo> RoomInfos { get; set; }
        public ICollection<AccountLogin> AccountLogins { get; set; }

    }
}