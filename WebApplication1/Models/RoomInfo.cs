using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class RoomInfo
    {
        [Key]
        public int RoomId { get; set; }
        public string RoomType { get; set; }
        public int Size { get; set; }
        public string Description { get; set; }
        public int RoomNo { get; set; }
        public Boolean Availability { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("Admin")]
        public int AdminId { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public Admin Admin { get; set; }
    }
}