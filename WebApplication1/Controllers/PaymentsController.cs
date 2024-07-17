using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;



namespace WebApplication1.Controllers
{
    public class PaymentsController : Controller
    {
        private OnlineHotelDBContext db = new OnlineHotelDBContext();



        // GET: Payments
        public ActionResult Index()
        {
            var payments = db.Payments.Include(p => p.Reservation);
            return View(payments.ToList());
        }



        // GET: Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }



        // GET: Payments/Create
        public ActionResult Create()
        {
            ViewBag.ReservationId = new SelectList(db.Reservations, "ReservationId", "ReservationId");
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaymentId,ReservationId,TotalAmount,PaymentMethod,TimeStamp")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Payments.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }



            ViewBag.ReservationId = new SelectList(db.Reservations, "ReservationId", "ReservationId", payment.ReservationId);
            return View(payment);
        }



        // GET: Payments/Edit/
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReservationId = new SelectList(db.Reservations, "ReservationId", "ReservationId", payment.ReservationId);
            return View(payment);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentId,ReservationId,TotalAmount,PaymentMethod,TimeStamp")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReservationId = new SelectList(db.Reservations, "ReservationId", "ReservationId", payment.ReservationId);
            return View(payment);
        }



        // GET: Payments/Delete/
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }



        // POST: Payments/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        // GET: Payments/CalculateTotalAmount
        [HttpPost]
        public ActionResult CalculateTotalPrice()
        {
            DateTime CheckInDate = db.Reservations.Select(t => t.CheckInDate).FirstOrDefault();
            DateTime CheckOutDate = db.Reservations.Select(a => a.CheckOutDate).FirstOrDefault();
            decimal pricepernight = db.RoomInfos.Select(p => p.Price).FirstOrDefault();
            int Noofnights = (int)Math.Floor((CheckOutDate - CheckInDate).TotalDays) + 1;
            decimal totalAmount = Noofnights * pricepernight;



            Session["TotalPrice"] = totalAmount;



            return RedirectToAction("PaymentDetails");
        }
        public ActionResult PaymentDetails(int? reservationId)
        {
            CalculateTotalPrice();
            decimal totalPrice = (decimal)Session["TotalPrice"];



            var payment = new Payment
            {
                TotalAmount = totalPrice
            };
            var paymentMethods = new List<SelectListItem>
    {
        new SelectListItem { Value = "GPay", Text = "GPay" },
        new SelectListItem { Value = "GPay", Text = "Paypal" },
        new SelectListItem { Value = "GPay", Text = "Paytm" },
        new SelectListItem { Value = "GPay", Text = "Visa card" },
        new SelectListItem { Value = "GPay", Text = "Master card" },
        new SelectListItem { Value = "CashOnDelivery", Text = "Cash on Delivery" }
    };



            ViewBag.PaymentMethods = paymentMethods;



            ViewBag.ReservationId = new SelectList(db.Reservations, "ReservationId", "ReservationId");
            return View(payment);




        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public ActionResult PaymentDetails([Bind(Include = "PaymentId,ReservationId,TotalAmount,PaymentMethod,TimeStamp")] Payment payment)
        {
            payment.TimeStamp = DateTime.Now;
            if (ModelState.IsValid)
            {
                //TempData["TotalPrice"] = booking.TotalPrice;
                db.Payments.Add(payment);
                //db.SaveChanges();
                TempData["SuccessMessage"] = "Payment succesful";
                return RedirectToAction("PaymentConfirmation");
            }
            ViewBag.PaymentMethods = new List<SelectListItem>
    {
        new SelectListItem { Value = "GPay", Text = "GPay" },
        new SelectListItem { Value = "GPay", Text = "Paypal" },
        new SelectListItem { Value = "GPay", Text = "Paytm" },
        new SelectListItem { Value = "GPay", Text = "Visa card" },
        new SelectListItem { Value = "GPay", Text = "Master card" },
        new SelectListItem { Value = "CashOnDelivery", Text = "Cash on Delivery" }
    };


            ViewBag.ReservationId = new SelectList(db.Reservations, "ReservationId", "ReservationId", payment.ReservationId);
            return View(payment);
        }
        public ActionResult PaymentConfirmation()
        {
            string successMessage = TempData["SuccessMessage"]?.ToString();
            ViewBag.SuccessMessage = successMessage;
            return View();
        }
        public ActionResult PaymentSucess()
        {
            return View();
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}