using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        [ForeignKey("RoomInfo")]
        public int RoomId { get; set; }
        public int NoOfPerson { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public User User { get; set; }
        public RoomInfo RoomInfo { get; set; }





    }
}