using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FirstTry;
using FirstTry.Models;
using FirstTry.Static;
using Microsoft.AspNet.Identity.Owin;

namespace FirstTry.Controllers
{
    public class DoctorsController : Controller
    {
        private polyclinicEntities db = new polyclinicEntities();

         // GET: Doctors
        public ActionResult Index(string sortOrder, string[] SearchStrings)
        {
            ViewBag.FIOSortParm = sortOrder == "FIO" ? "FIO_desc" : "FIO";
            ViewBag.PhoneSortParm = sortOrder == "Phone" ? "Phone_desc" : "Phone";
            ViewBag.SalarySortParm = sortOrder == "Salary" ? "Salary_desc" : "Salary";
            ViewBag.DSWSortParm = sortOrder == "DSW" ? "DSW_desc" : "DSW";
            ViewBag.DEWSortParm = sortOrder == "DEW" ? "DEW_desc" : "DEW";
            ViewBag.PositionSortParm = sortOrder == "Position" ? "Position_desc" : "Position";
            ViewBag.QualificationSortParm = sortOrder == "Qualification" ? "Qualification_desc" : "Qualification";
            ViewBag.AddressSortParm = sortOrder == "Address" ? "Address_desc" : "Address";

            ViewData["sortOrder"] = sortOrder;

            if (SearchStrings != null && SearchStrings.Length == 1)
            {
                SearchStrings = SearchStrings[0].Split('%');
            }
            ViewBag.SearchStrings = SearchStrings;

            var doctors = db.doctors.ToList();            

            doctors = SortDoctors(sortOrder, doctors);           

            if(SearchStrings != null && SearchStrings.Length == 8)
                doctors = SearchDoctors(SearchStrings, doctors);

            return View(doctors);
        }

        public List<doctors> SearchDoctors(string[] SearchStrings, List<doctors> doctors)
        {
            if (!string.IsNullOrEmpty(SearchStrings[0]))
            {
                doctors = doctors.Where(s => s.FIO.Contains(SearchStrings[0])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[1]))
            {
                doctors = doctors.Where(s => s.phone.Contains(SearchStrings[1])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[2]))
            {
                doctors = doctors.Where(s => s.salary.ToString().Contains(SearchStrings[2])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[3]))
            {
                doctors = doctors.Where(s => s.date_start_work.ToString().Contains(SearchStrings[3])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[4]))
            {
                doctors = doctors.Where(s => s.date_end_work.ToString().Contains(SearchStrings[4])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[5]))
            {
                doctors = doctors.Where(s => s.position.Contains(SearchStrings[5])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[6]))
            {
                doctors = doctors.Where(s => s.qualification.ToString().Contains(SearchStrings[6])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[7]))
            {
                doctors = doctors.Where(s => s.address.Contains(SearchStrings[7])).ToList();
            }

            return doctors;
        }

            public List<doctors> SortDoctors(string sortOrder, List<doctors> doctors)
        {
            switch (sortOrder)
            {
               case "FIO":
                    doctors = doctors.OrderBy(s => s.FIO).ToList();
                    break;
                case "FIO_desc":
                    doctors = doctors.OrderByDescending(s => s.FIO).ToList();
                    break;
                case "Phone":
                    doctors = doctors.OrderBy(s => s.phone).ToList();
                    break;
                case "Phone_desc":
                    doctors = doctors.OrderByDescending(s => s.phone).ToList();
                    break;
                case "Salary":
                    doctors = doctors.OrderBy(s => s.salary).ToList();
                    break;
                case "Salary_desc":
                    doctors = doctors.OrderByDescending(s => s.salary).ToList();
                    break;
                case "DSW":
                    doctors = doctors.OrderBy(s => s.date_start_work).ToList();
                    break;
                case "DSW_desc":
                    doctors = doctors.OrderByDescending(s => s.date_start_work).ToList();
                    break;
                case "DEW":
                    doctors = doctors.OrderBy(s => s.date_end_work).ToList();
                    break;
                case "DEW_desc":
                    doctors = doctors.OrderByDescending(s => s.date_end_work).ToList();
                    break;
                case "Position":
                    doctors = doctors.OrderBy(s => s.position).ToList();
                    break;
                case "Position_desc":
                    doctors = doctors.OrderByDescending(s => s.position).ToList();
                    break;
                case "Qualification":
                    doctors = doctors.OrderBy(s => s.qualification).ToList();
                    break;
                case "Qualification_desc":
                    doctors = doctors.OrderByDescending(s => s.qualification).ToList();
                    break;
                case "Address":
                    doctors = doctors.OrderBy(s => s.address).ToList();
                    break;
                case "Address_desc":
                    doctors = doctors.OrderByDescending(s => s.address).ToList();
                    break;
            }

            return doctors;
        }

        // GET: Doctors/Details/5
        [Authorize(Roles = "director, manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            doctors doctors = db.doctors.Find(id);
            if (doctors == null)
            {
                return HttpNotFound();
            }
            return View(doctors);
        }

        // GET: Doctors/Create
        [Authorize(Roles = "director")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Doctors/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "director")]
        public ActionResult Create([Bind(Include = "id,FIO,phone,salary,date_start_work,date_end_work,position,qualification,address")] doctors doctors)
        {
            if (ModelState.IsValid)
            {
                db.doctors.Add(doctors);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(doctors);
        }

        // GET: Doctors/Edit/5
        [Authorize(Roles = "director, manager, doctor")]
        public ActionResult Edit(int? id)
        {
            if (User.IsInRole("manager") || User.IsInRole("director")) ;
            else if (User.IsInRole("doctor"))
            {
                var user = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().Users.FirstOrDefault(it => it.UserName == User.Identity.Name);
                int doctorId = DoctorsId.getDoctorId(user.Id);
                if(doctorId != id)
                {
                    return RedirectToAction("Index");
                }
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            doctors doctors = db.doctors.Find(id);
            if (doctors == null)
            {
                return HttpNotFound();
            }
            return View(doctors);
        }

        // POST: Doctors/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "director, manager, doctor")]
        public ActionResult Edit([Bind(Include = "id,FIO,phone,salary,date_start_work,date_end_work,position,qualification,address")] doctors doctors)
        {
            if (User.IsInRole("manager") || User.IsInRole("director")) ;
            else if (User.IsInRole("doctor"))
            {
                var user = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().Users.FirstOrDefault(it => it.UserName == User.Identity.Name);
                int doctorId = DoctorsId.getDoctorId(user.Id);
                if (doctorId != doctors.id)
                {
                    return RedirectToAction("Index");
                }
            }

            if (ModelState.IsValid)
            {
                db.Entry(doctors).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doctors);
        }

        // GET: Doctors/Delete/5
        [Authorize(Roles = "director")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            doctors doctors = db.doctors.Find(id);
            if (doctors == null)
            {
                return HttpNotFound();
            }
            return View(doctors);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "director")]
        public ActionResult DeleteConfirmed(int id)
        {
            doctors doctors = db.doctors.Find(id);
            db.doctors.Remove(doctors);
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
