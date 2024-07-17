using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        [ForeignKey("Reservation")]
        public int ReservationId { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime TimeStamp { get; set; }
        public Reservation Reservation { get; set; }

    }
}