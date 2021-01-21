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
    public class DoctorVisitsController : Controller
    {
        private polyclinicEntities db = new polyclinicEntities();

        // GET: DoctorVisits
        public ActionResult Index(string sortOrder, string[] SearchStrings)
        {
            ViewBag.dateSortParm = sortOrder == "date" ? "date_desc" : "date";
            ViewBag.reasonSortParm = sortOrder == "reason" ? "reason_desc" : "reason";
            ViewBag.resultSortParm = sortOrder == "result" ? "result_desc" : "result";
            ViewBag.treatmentSortParm = sortOrder == "treatment" ? "treatment_desc" : "treatment";
            ViewBag.docFIOSortParm = sortOrder == "docFIO" ? "docFIO_desc" : "docFIO";
            ViewBag.patFIOSortParm = sortOrder == "patFIO" ? "patFIO_desc" : "patFIO";

            ViewData["sortOrder"] = sortOrder;

            if (SearchStrings != null && SearchStrings.Length == 1)
            {
                SearchStrings = SearchStrings[0].Split('%');
            }
            ViewBag.SearchStrings = SearchStrings;

            var doctor_visits = db.doctor_visits.Include(d => d.doctors).Include(d => d.patients).ToList();

            doctor_visits = SortDoctorVisits(sortOrder, doctor_visits);

            if (SearchStrings != null && SearchStrings.Length == 6)
                doctor_visits = SearchDoctorVisits(SearchStrings, doctor_visits);

            return View(doctor_visits);
        }

        public List<doctor_visits> SearchDoctorVisits(string[] SearchStrings, List<doctor_visits> doctor_visits)
        {
            if (!string.IsNullOrEmpty(SearchStrings[0]))
            {
                doctor_visits = doctor_visits.Where(s => s.date.ToString().Contains(SearchStrings[0])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[1]))
            {
                doctor_visits = doctor_visits.Where(s => s.reason.Contains(SearchStrings[1])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[2]))
            {
                doctor_visits = doctor_visits.Where(s => s.result.Contains(SearchStrings[2])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[3]))
            {
                doctor_visits = doctor_visits.Where(s => s.treatment.Contains(SearchStrings[3])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[4]))
            {
                doctor_visits = doctor_visits.Where(s => s.doctors.FIO.Contains(SearchStrings[4])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[5]))
            {
                doctor_visits = doctor_visits.Where(s => s.patients.FIO.Contains(SearchStrings[5])).ToList();
            }            

            return doctor_visits;
        }

        public List<doctor_visits> SortDoctorVisits(string sortOrder, List<doctor_visits> doctor_visits)
        {
            switch (sortOrder)
            {
                case "date":
                    doctor_visits = doctor_visits.OrderBy(s => s.date).ToList();
                    break;
                case "date_desc":
                    doctor_visits = doctor_visits.OrderByDescending(s => s.date).ToList();
                    break;
                case "reason":
                    doctor_visits = doctor_visits.OrderBy(s => s.reason).ToList();
                    break;
                case "reason_desc":
                    doctor_visits = doctor_visits.OrderByDescending(s => s.reason).ToList();
                    break;
                case "result":
                    doctor_visits = doctor_visits.OrderBy(s => s.result).ToList();
                    break;
                case "result_desc":
                    doctor_visits = doctor_visits.OrderByDescending(s => s.result).ToList();
                    break;
                case "treatment":
                    doctor_visits = doctor_visits.OrderBy(s => s.treatment).ToList();
                    break;
                case "treatment_desc":
                    doctor_visits = doctor_visits.OrderByDescending(s => s.treatment).ToList();
                    break;
                case "docFIO":
                    doctor_visits = doctor_visits.OrderBy(s => s.doctors.FIO).ToList();
                    break;
                case "docFIO_desc":
                    doctor_visits = doctor_visits.OrderByDescending(s => s.doctors.FIO).ToList();
                    break;
                case "patFIO":
                    doctor_visits = doctor_visits.OrderBy(s => s.patients.FIO).ToList();
                    break;
                case "patFIO_desc":
                    doctor_visits = doctor_visits.OrderByDescending(s => s.patients.FIO).ToList();
                    break;                
            }

            return doctor_visits;
        }

        // GET: DoctorVisits/Details/5
        [Authorize(Roles = "manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            doctor_visits doctor_visits = db.doctor_visits.Find(id);
            if (doctor_visits == null)
            {
                return HttpNotFound();
            }
            return View(doctor_visits);
        }

        // GET: DoctorVisits/Create
        [Authorize(Roles = "manager")]
        public ActionResult Create()
        {
            ViewBag.doctor_id = new SelectList(db.doctors.Where((it) => it.date_end_work == null), "id", "FIO");
            ViewBag.patient_id = new SelectList(db.patients, "id", "FIO");

            doctor_visits doctor_visits = new doctor_visits();
            doctor_visits.date = DateTime.Now;

            return View();
        }

        // POST: DoctorVisits/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "manager")]
        public ActionResult Create([Bind(Include = "id,doctor_id,patient_id,date,reason,result,treatment")] doctor_visits doctor_visits)
        {
            if (ModelState.IsValid)
            {
                db.doctor_visits.Add(doctor_visits);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.doctor_id = new SelectList(db.doctors, "id", "FIO", doctor_visits.doctor_id);
            ViewBag.patient_id = new SelectList(db.patients, "id", "FIO", doctor_visits.patient_id);
            return View(doctor_visits);
        }

        // GET: DoctorVisits/Edit/5
        [Authorize(Roles = "manager, doctor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            doctor_visits doctor_visits = db.doctor_visits.Find(id);

            if (User.IsInRole("manager") || User.IsInRole("director")) ;
            else if (User.IsInRole("doctor"))
            {
                var user = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().Users.FirstOrDefault(it => it.UserName == User.Identity.Name);
                int doctorId = DoctorsId.getDoctorId(user.Id);
                if (doctorId != doctor_visits.doctor_id)
                {
                    return RedirectToAction("Index");
                }
            }
            
            if (doctor_visits == null)
            {
                return HttpNotFound();
            }
            ViewBag.doctor_id = new SelectList(db.doctors, "id", "FIO", doctor_visits.doctor_id);
            ViewBag.patient_id = new SelectList(db.patients, "id", "FIO", doctor_visits.patient_id);
            return View(doctor_visits);
        }

        // POST: DoctorVisits/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "manager, doctor")]
        public ActionResult Edit([Bind(Include = "id,doctor_id,patient_id,date,reason,result,treatment")] doctor_visits doctor_visits)
        {
            if (User.IsInRole("manager") || User.IsInRole("director")) ;
            else if (User.IsInRole("doctor"))
            {
                var user = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().Users.FirstOrDefault(it => it.UserName == User.Identity.Name);
                int doctorId = DoctorsId.getDoctorId(user.Id);
                if (doctorId != doctor_visits.doctor_id)
                {
                    return RedirectToAction("Index");
                }
            }

            if (ModelState.IsValid)
            {
                db.Entry(doctor_visits).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.doctor_id = new SelectList(db.doctors, "id", "FIO", doctor_visits.doctor_id);
            ViewBag.patient_id = new SelectList(db.patients, "id", "FIO", doctor_visits.patient_id);
            return View(doctor_visits);
        }

        // GET: DoctorVisits/Delete/5
        [Authorize(Roles = "manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            doctor_visits doctor_visits = db.doctor_visits.Find(id);
            if (doctor_visits == null)
            {
                return HttpNotFound();
            }
            return View(doctor_visits);
        }

        // POST: DoctorVisits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "manager")]
        public ActionResult DeleteConfirmed(int id)
        {
            doctor_visits doctor_visits = db.doctor_visits.Find(id);
            db.doctor_visits.Remove(doctor_visits);
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
