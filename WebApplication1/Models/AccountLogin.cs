﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class AccountLogin
    {
        [Key]
        public int LoginId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public User User { get; set; }
        public Admin Admin { get; set; }


    }
}