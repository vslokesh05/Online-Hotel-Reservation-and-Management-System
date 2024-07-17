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
    public class ReservationsController : Controller
    {
        private OnlineHotelDBContext db = new OnlineHotelDBContext();

        // GET: Reservations
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.RoomInfo).Include(r => r.User);
            return View(reservations.ToList());
        }

        // GET: Reservations/Details/
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            ViewBag.RoomId = new SelectList(db.RoomInfos, "RoomId", "RoomType");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name");
            return View();
        }

        // POST: Reservations/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservationId,RoomId,NoOfPerson,CheckInDate,CheckOutDate,UserId")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Reservations.Add(reservation);
                //db.SaveChanges();
                return RedirectToAction("ReservationDetails", new { id = reservation.ReservationId });
            }

            ViewBag.RoomId = new SelectList(db.RoomInfos, "RoomId", "RoomType", reservation.RoomId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name", reservation.UserId);
            return View(reservation);
        }


        // GET: Reservations/Edit/
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoomId = new SelectList(db.RoomInfos, "RoomId", "RoomType", reservation.RoomId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name", reservation.UserId);
            return View(reservation);
        }

        // POST: Reservations/Edit/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationId,RoomId,NoOfPerson,CheckInDate,CheckOutDate,UserId")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoomId = new SelectList(db.RoomInfos, "RoomId", "RoomType", reservation.RoomId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name", reservation.UserId);
            return View(reservation);
        }

        // GET: Reservations/Delete/
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ReservationDetails([Bind(Include = "ReservationId,CheckInDate,CheckOutDate,NoOfPerson,RoomId,UserId")] Reservation reservation)
        {
            //reservation.CheckInDate = DateTime.Now;
            if (ModelState.IsValid)
            {

                db.Reservations.Add(reservation);
                //db.SaveChanges();
                TempData["SuccessMessage"] = "ROOM BOOKED SUCESSFULY!!!";
                return RedirectToAction("ReservationConfirmation");
               


            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name", reservation.UserId);
            ViewBag.RoomID = new SelectList(db.RoomInfos, "RoomId", "RoomType", reservation.RoomId);
            return View(reservation);
        }
        public ActionResult ReservationConfirmation()
        {
            string successMessage = TempData["SuccessMessage"]?.ToString();
            ViewBag.SuccessMessage = successMessage;
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