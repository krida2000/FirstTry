using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FirstTry;
using FirstTry.Static;
using Microsoft.AspNet.Identity.Owin;

namespace FirstTry.Controllers
{
    public class PlacingInHospitalController : Controller
    {
        private polyclinicEntities db = new polyclinicEntities();

        // GET: PlacingInHospital
        public ActionResult Index(string sortOrder, string[] SearchStrings)
        {
            var placing_in_hospital = db.placing_in_hospital.Include(p => p.doctors).Include(p => p.patients).ToList();

            ViewBag.date_startSortParm = sortOrder == "date_start" ? "date_start_desc" : "date_start";
            ViewBag.date_endSortParm = sortOrder == "date_end" ? "date_end_desc" : "date_end";
            ViewBag.reasonSortParm = sortOrder == "reason" ? "reason_desc" : "reason";
            ViewBag.treatmentSortParm = sortOrder == "treatment" ? "treatment_desc" : "treatment";
            ViewBag.docFIOSortParm = sortOrder == "docFIO" ? "docFIO_desc" : "docFIO";
            ViewBag.patFIOSortParm = sortOrder == "patFIO" ? "patFIO_desc" : "patFIO";

            ViewData["sortOrder"] = sortOrder;

            if (SearchStrings != null && SearchStrings.Length == 1)
            {
                SearchStrings = SearchStrings[0].Split('%');
            }
            ViewBag.SearchStrings = SearchStrings;

            placing_in_hospital = SortData(sortOrder, placing_in_hospital);

            if (SearchStrings != null && SearchStrings.Length == 6)
                placing_in_hospital = SearchData(SearchStrings, placing_in_hospital);

            return View(placing_in_hospital);
        }

        public List<placing_in_hospital> SearchData(string[] SearchStrings, List<placing_in_hospital> placing_in_hospital)
        {
            if (!string.IsNullOrEmpty(SearchStrings[0]))
            {
                placing_in_hospital = placing_in_hospital.Where(s => s.date_start.ToString().Contains(SearchStrings[0])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[1]))
            {
                placing_in_hospital = placing_in_hospital.Where(s => s.date_end.ToString().Contains(SearchStrings[1])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[2]))
            {
                placing_in_hospital = placing_in_hospital.Where(s => s.reason.ToString().Contains(SearchStrings[2])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[3]))
            {
                placing_in_hospital = placing_in_hospital.Where(s => s.treatment.Contains(SearchStrings[3])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[3]))
            {
                placing_in_hospital = placing_in_hospital.Where(s => s.doctors.FIO.Contains(SearchStrings[3])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[3]))
            {
                placing_in_hospital = placing_in_hospital.Where(s => s.patients.FIO.Contains(SearchStrings[3])).ToList();
            }

            return placing_in_hospital;
        }

        public List<placing_in_hospital> SortData(string sortOrder, List<placing_in_hospital> placing_in_hospital)
        {
            switch (sortOrder)
            {
                case "date_start":
                    placing_in_hospital = placing_in_hospital.OrderBy(s => s.date_start).ToList();
                    break;
                case "date_start_desc":
                    placing_in_hospital = placing_in_hospital.OrderByDescending(s => s.date_start).ToList();
                    break;
                case "date_end":
                    placing_in_hospital = placing_in_hospital.OrderBy(s => s.date_end).ToList();
                    break;
                case "date_end_desc":
                    placing_in_hospital = placing_in_hospital.OrderByDescending(s => s.date_end).ToList();
                    break;
                case "reason":
                    placing_in_hospital = placing_in_hospital.OrderBy(s => s.reason).ToList();
                    break;
                case "reason_desc":
                    placing_in_hospital = placing_in_hospital.OrderByDescending(s => s.reason).ToList();
                    break;
                case "treatment":
                    placing_in_hospital = placing_in_hospital.OrderBy(s => s.treatment).ToList();
                    break;
                case "treatment_desc":
                    placing_in_hospital = placing_in_hospital.OrderByDescending(s => s.treatment).ToList();
                    break; 
                case "docFIO":
                    placing_in_hospital = placing_in_hospital.OrderBy(s => s.doctors.FIO).ToList();
                    break;
                case "docFIO_desc":
                    placing_in_hospital = placing_in_hospital.OrderByDescending(s => s.doctors.FIO).ToList();
                    break; 
                case "patFIO":
                    placing_in_hospital = placing_in_hospital.OrderBy(s => s.patients.FIO).ToList();
                    break;
                case "patFIO_desc":
                    placing_in_hospital = placing_in_hospital.OrderByDescending(s => s.patients.FIO).ToList();
                    break;
            }

            return placing_in_hospital;
        }

        // GET: PlacingInHospital/Details/5
        [Authorize(Roles = "manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            placing_in_hospital placing_in_hospital = db.placing_in_hospital.Find(id);
            if (placing_in_hospital == null)
            {
                return HttpNotFound();
            }
            return View(placing_in_hospital);
        }

        // GET: PlacingInHospital/Create
        [Authorize(Roles = "manager, doctor")]
        public ActionResult Create()
        {
            ViewBag.attending_doctor_id = new SelectList(db.doctors.Where((it) => it.date_end_work == null), "id", "FIO");
            ViewBag.patient_id = new SelectList(db.patients, "id", "FIO");

            placing_in_hospital placing_in_hospital = new placing_in_hospital();
            placing_in_hospital.date_start = DateTime.Now;

            return View(placing_in_hospital);
        }

        // POST: PlacingInHospital/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "manager, doctor")]
        public ActionResult Create([Bind(Include = "id,attending_doctor_id,patient_id,date_start,date_end,reason,treatment")] placing_in_hospital placing_in_hospital)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.placing_in_hospital.Add(placing_in_hospital);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null)
                    {
                        var mess = ex.InnerException.InnerException.Message;
                        ViewData["error"] = mess.Substring(0, mess.Length > 65 ? mess.Length - 65 : mess.Length);
                    }
                }
            }

            ViewBag.attending_doctor_id = new SelectList(db.doctors, "id", "FIO", placing_in_hospital.attending_doctor_id);
            ViewBag.patient_id = new SelectList(db.patients, "id", "FIO", placing_in_hospital.patient_id);
            return View(placing_in_hospital);
        }

        // GET: PlacingInHospital/Edit/5
        [Authorize(Roles = "manager, doctor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            placing_in_hospital placing_in_hospital = db.placing_in_hospital.Find(id);

            if (User.IsInRole("manager") || User.IsInRole("director")) ;
            else if (User.IsInRole("doctor"))
            {
                var user = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().Users.FirstOrDefault(it => it.UserName == User.Identity.Name);
                int doctorId = DoctorsId.getDoctorId(user.Id);
                if (doctorId != placing_in_hospital.attending_doctor_id)
                {
                    return RedirectToAction("Index");
                }
            }

            if (placing_in_hospital == null)
            {
                return HttpNotFound();
            }
            ViewBag.attending_doctor_id = new SelectList(db.doctors, "id", "FIO", placing_in_hospital.attending_doctor_id);
            ViewBag.patient_id = new SelectList(db.patients, "id", "FIO", placing_in_hospital.patient_id);
            return View(placing_in_hospital);
        }

        // POST: PlacingInHospital/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "manager, doctor")]
        public ActionResult Edit([Bind(Include = "id,attending_doctor_id,patient_id,date_start,date_end,reason,treatment")] placing_in_hospital placing_in_hospital)
        {
            if (User.IsInRole("manager") || User.IsInRole("director")) ;
            else if (User.IsInRole("doctor"))
            {
                var user = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().Users.FirstOrDefault(it => it.UserName == User.Identity.Name);
                int doctorId = DoctorsId.getDoctorId(user.Id);
                if (doctorId != placing_in_hospital.attending_doctor_id)
                {
                    return RedirectToAction("Index");
                }
            }

            if (ModelState.IsValid)
            {
                db.Entry(placing_in_hospital).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.attending_doctor_id = new SelectList(db.doctors, "id", "FIO", placing_in_hospital.attending_doctor_id);
            ViewBag.patient_id = new SelectList(db.patients, "id", "FIO", placing_in_hospital.patient_id);
            return View(placing_in_hospital);
        }

        // GET: PlacingInHospital/Delete/5
        [Authorize(Roles = "manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            placing_in_hospital placing_in_hospital = db.placing_in_hospital.Find(id);
            if (placing_in_hospital == null)
            {
                return HttpNotFound();
            }
            return View(placing_in_hospital);
        }

        // POST: PlacingInHospital/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "manager")]
        public ActionResult DeleteConfirmed(int id)
        {
            placing_in_hospital placing_in_hospital = db.placing_in_hospital.Find(id);
            db.placing_in_hospital.Remove(placing_in_hospital);
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
