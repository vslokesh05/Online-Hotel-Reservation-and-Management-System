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
    public class RoomInfosController : Controller
    {
        private OnlineHotelDBContext db = new OnlineHotelDBContext();

        // GET: RoomInfoes
        public ActionResult Index()
        {
            var roomInfos = db.RoomInfos.Include(r => r.Admin);
            return View(roomInfos.ToList());
        }

        // GET: RoomInfoes/Details/
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomInfo roomInfo = db.RoomInfos.Find(id);
            if (roomInfo == null)
            {
                return HttpNotFound();
            }
            return View(roomInfo);
        }

        // GET: RoomInfoes/Create
        public ActionResult Create()
        {
            ViewBag.AdminId = new SelectList(db.Admins, "AdminId", "AName");
            return View();
        }

        // POST: RoomInfoes/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoomId,RoomType,Size,Description,RoomNo,Availability,Price,AdminId")] RoomInfo roomInfo)
        {
            if (ModelState.IsValid)
            {
                db.RoomInfos.Add(roomInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdminId = new SelectList(db.Admins, "AdminId", "AName", roomInfo.AdminId);
            return View(roomInfo);
        }

        // GET: RoomInfoes/Edit/
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomInfo roomInfo = db.RoomInfos.Find(id);
            if (roomInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdminId = new SelectList(db.Admins, "AdminId", "AName", roomInfo.AdminId);
            return View(roomInfo);
        }

        // POST: RoomInfoes/Edit/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoomId,RoomType,Size,Description,RoomNo,Availability,Price,AdminId")] RoomInfo roomInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roomInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdminId = new SelectList(db.Admins, "AdminId", "AName", roomInfo.AdminId);
            return View(roomInfo);
        }

        // GET: RoomInfoes/Delete/
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomInfo roomInfo = db.RoomInfos.Find(id);
            if (roomInfo == null)
            {
                return HttpNotFound();
            }
            return View(roomInfo);
        }

        // POST: RoomInfoes/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoomInfo roomInfo = db.RoomInfos.Find(id);
            db.RoomInfos.Remove(roomInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult RMlist()
        {
            var rooms = db.RoomInfos.Take(10).ToList();
            return View(rooms);
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
