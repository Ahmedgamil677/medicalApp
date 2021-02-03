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
   public class RxesController : Controller
    {
        private medicaldatabaseEntities db = new medicaldatabaseEntities();

      
        public ActionResult Index(string sort, string SelectedDoctorName)
        {
            ViewBag.DoctorName = string.IsNullOrEmpty(sort) ? "DoctorName_sort" : "";
            ViewBag.PatientName = sort == "PatientName" ? "patientName_sort" : "PatientName";
          var data = (from s in db.Rxes
                        select s).ToList(); 
            var temp = from s in data
                       select s;
            if(!string.IsNullOrEmpty(SelectedDoctorName))
            {
                temp = temp.Where(s => s.DoctorName.Trim().Equals(SelectedDoctorName.Trim()));
            }
            var uniqueDoctorNames = from s in temp
                                   group s by s.DoctorName into newGroup
                                   where newGroup.Key != null   
                                   orderby newGroup.Key
                                   select new { DoctorName = newGroup.Key };
            ViewBag.uniqueDoctorNames = uniqueDoctorNames.Select(m => new SelectListItem { Value = m.DoctorName, Text = m.DoctorName }).ToList();
            ViewBag.SelectedDoctorName = SelectedDoctorName;


            switch (sort)
            {
                case "DoctorName_sort" :
                    temp = temp.OrderByDescending(s => s.DoctorName);
                    break;
                case "patientName":
                    temp = temp.OrderBy(s => s.patientName);
                    break;
                case "PatientName_sort":
                    temp = temp.OrderByDescending(s => s.patientName);
                    break;
                default:
                    temp = temp.OrderBy(s => s.DoctorName);
                    break;
            }

            return View(temp.ToList());
        }


        public ActionResult ShowRx(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rx rx = db.Rxes.Find(id);
            if (rx == null)
            {
                return HttpNotFound();
            }
            var doctor = db.Doctors.Find(rx.DoctorId);
            var temp = doctor.specialty.ToString();
            ViewBag.specialty = temp;

            var clinic = db.Clinics.Find(rx.ClinicId);
            var tem = clinic.clinicAddress.ToString();
            ViewBag.clinicAddress = tem;
            var tem2 = clinic.clinicMobile;
            ViewBag.clinicMobile = tem2;


            return View(rx);
            
            
        }
      
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rx rx = db.Rxes.Find(id);
            if (rx == null)
            {
                return HttpNotFound();
            }
            return View(rx);
        }

       
        public ActionResult Create()
        {
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PatientId,patientName,ClinicId,ClinicName,PatientMobile,Date,DoctorName,DoctorId,medicineName")] Rx rx)
        {
            if (ModelState.IsValid)
            {
                db.Rxes.Add(rx);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rx);
        }

     
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rx rx = db.Rxes.Find(id);
            if (rx == null)
            {
                return HttpNotFound();
            }
            return View(rx);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PatientId,patientName,ClinicId,ClinicName,PatientMobile,Date,DoctorName,DoctorId,medicineName,dose")] Rx rx)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rx).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rx);
        }

      
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rx rx = db.Rxes.Find(id);
            if (rx == null)
            {
                return HttpNotFound();
            }
            return View(rx);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rx rx = db.Rxes.Find(id);
            db.Rxes.Remove(rx);
            db.SaveChanges();
            return RedirectToAction("Index");
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
