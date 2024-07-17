using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Name must not be empty")]
        public string Name { get; set; }
        [Display(Name = "Contact number")]
        [Required(ErrorMessage = "Contact no must not be empty"), MaxLength(10)]
        [RegularExpression(@"[7-9]{1}[0-9]{9}", ErrorMessage = "Invalid Contact number")]
        public string PhoneNo { get; set; }
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Id must not be empty")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailId { get; set; }
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username must not be empty"), MaxLength(255)]
        public string Username { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password must not be empty"), MaxLength(255)]
        public string Password { get; set; }
        [Display(Name = "Aadhar No")]
        [Required(ErrorMessage = "Aadhar No must not be empty"), MaxLength(255)]
        public string AadharNo { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<AccountLogin> AccountLogins { get; set; }


    }
}