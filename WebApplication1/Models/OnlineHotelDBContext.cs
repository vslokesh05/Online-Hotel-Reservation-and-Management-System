using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class OnlineHotelDBContext : DbContext
    {
        
        public OnlineHotelDBContext() : base("sqlconl"){}
       public virtual DbSet<AccountLogin> AccountLogins { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<RoomInfo> RoomInfos { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }

        }

    }
