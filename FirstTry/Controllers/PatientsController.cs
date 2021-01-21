using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FirstTry;

namespace FirstTry.Controllers
{
    public class PatientsController : Controller
    {
        private polyclinicEntities db = new polyclinicEntities();

        // GET: Patients
        public ActionResult Index(string sortOrder, string[] SearchStrings)
        {
            ViewBag.FIOSortParm = sortOrder == "FIO" ? "FIO_desc" : "FIO";
            ViewBag.phoneSortParm = sortOrder == "phone" ? "phone_desc" : "phone";
            ViewBag.date_birthSortParm = sortOrder == "date_birth" ? "date_birth_desc" : "date_birth";
            ViewBag.addressSortParm = sortOrder == "address" ? "address_desc" : "address";

            ViewData["sortOrder"] = sortOrder;

            if (SearchStrings != null && SearchStrings.Length == 1)
            {
                SearchStrings = SearchStrings[0].Split('%');
            }
            ViewBag.SearchStrings = SearchStrings;

            var patients = db.patients.ToList();

            patients = SortData(sortOrder, patients);

            if (SearchStrings != null && SearchStrings.Length == 4)
                patients = SearchData(SearchStrings, patients);

            return View(patients);
        }

        public List<patients> SearchData(string[] SearchStrings, List<patients> patients)
        {
            if (!string.IsNullOrEmpty(SearchStrings[0]))
            {
                patients = patients.Where(s => s.FIO.ToString().Contains(SearchStrings[0])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[1]))
            {
                patients = patients.Where(s => s.phone.Contains(SearchStrings[1])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[2]))
            {
                patients = patients.Where(s => s.date_birth.ToString().Contains(SearchStrings[2])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[3]))
            {
                patients = patients.Where(s => s.address.Contains(SearchStrings[3])).ToList();
            }
            
            return patients;
        }

        public List<patients> SortData(string sortOrder, List<patients> patients)
        {
            switch (sortOrder)
            {
                case "FIO":
                    patients = patients.OrderBy(s => s.FIO).ToList();
                    break;
                case "FIO_desc":
                    patients = patients.OrderByDescending(s => s.FIO).ToList();
                    break;
                case "phone":
                    patients = patients.OrderBy(s => s.phone).ToList();
                    break;
                case "phone_desc":
                    patients = patients.OrderByDescending(s => s.phone).ToList();
                    break;
                case "date_birth":
                    patients = patients.OrderBy(s => s.date_birth).ToList();
                    break;
                case "date_birth_desc":
                    patients = patients.OrderByDescending(s => s.date_birth).ToList();
                    break;
                case "address":
                    patients = patients.OrderBy(s => s.address).ToList();
                    break;
                case "address_desc":
                    patients = patients.OrderByDescending(s => s.address).ToList();
                    break;                
            }

            return patients;
        }

        // GET: Patients/Details/5
        [Authorize(Roles = "manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            patients patients = db.patients.Find(id);
            if (patients == null)
            {
                return HttpNotFound();
            }
            return View(patients);
        }

        // GET: Patients/Create
        [Authorize(Roles = "manager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "manager")]
        public ActionResult Create([Bind(Include = "id,FIO,phone,date_birth,address")] patients patients)
        {
            if (ModelState.IsValid)
            {
                db.patients.Add(patients);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patients);
        }

        // GET: Patients/Edit/5
        [Authorize(Roles = "manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            patients patients = db.patients.Find(id);
            if (patients == null)
            {
                return HttpNotFound();
            }
            return View(patients);
        }

        // POST: Patients/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "manager")]
        public ActionResult Edit([Bind(Include = "id,FIO,phone,date_birth,address")] patients patients)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patients).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patients);
        }

        // GET: Patients/Delete/5
        [Authorize(Roles = "manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            patients patients = db.patients.Find(id);
            if (patients == null)
            {
                return HttpNotFound();
            }
            return View(patients);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "manager")]
        public ActionResult DeleteConfirmed(int id)
        {
            patients patients = db.patients.Find(id);
            db.patients.Remove(patients);
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
