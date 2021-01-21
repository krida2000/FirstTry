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
    public class StorageUsingController : Controller
    {
        private polyclinicEntities db = new polyclinicEntities();

        // GET: StorageUsing
        [Authorize(Roles = "director, manager")]
        public ActionResult Index(string sortOrder, string[] SearchStrings)
        {
            var storage_using = db.storage_using.Include(s => s.doctors).Include(s => s.storage).ToList();

            ViewBag.countSortParm = sortOrder == "count" ? "count_desc" : "count";
            ViewBag.dateSortParm = sortOrder == "date" ? "date_desc" : "date";
            ViewBag.docFIOSortParm = sortOrder == "docFIO" ? "docFIO_desc" : "docFIO";
            ViewBag.stItem_nameSortParm = sortOrder == "stItem_name" ? "stItem_name_desc" : "stItem_name";

            ViewData["sortOrder"] = sortOrder;

            if (SearchStrings != null && SearchStrings.Length == 1)
            {
                SearchStrings = SearchStrings[0].Split('%');
            }
            ViewBag.SearchStrings = SearchStrings;

            storage_using = SortData(sortOrder, storage_using);

            if (SearchStrings != null && SearchStrings.Length == 4)
                storage_using = SearchData(SearchStrings, storage_using);

            return View(storage_using);
        }

        public List<storage_using> SearchData(string[] SearchStrings, List<storage_using> storage_using)
        {
            if (!string.IsNullOrEmpty(SearchStrings[0]))
            {
                storage_using = storage_using.Where(s => s.count.ToString().Contains(SearchStrings[0])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[1]))
            {
                storage_using = storage_using.Where(s => s.date.ToString().Contains(SearchStrings[1])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[2]))
            {
                storage_using = storage_using.Where(s => s.doctors.FIO.ToString().Contains(SearchStrings[2])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[3]))
            {
                storage_using = storage_using.Where(s => s.storage.item_name.Contains(SearchStrings[3])).ToList();
            }

            return storage_using;
        }

        public List<storage_using> SortData(string sortOrder, List<storage_using> storage_using)
        {
            switch (sortOrder)
            {
                case "count":
                    storage_using = storage_using.OrderBy(s => s.count).ToList();
                    break;
                case "count_desc":
                    storage_using = storage_using.OrderByDescending(s => s.count).ToList();
                    break;
                case "date":
                    storage_using = storage_using.OrderBy(s => s.date).ToList();
                    break;
                case "date_desc":
                    storage_using = storage_using.OrderByDescending(s => s.date).ToList();
                    break;
                case "docFIO":
                    storage_using = storage_using.OrderBy(s => s.doctors.FIO).ToList();
                    break;
                case "docFIO_desc":
                    storage_using = storage_using.OrderByDescending(s => s.doctors.FIO).ToList();
                    break;
                case "stItem_name":
                    storage_using = storage_using.OrderBy(s => s.storage.item_name).ToList();
                    break;
                case "stItem_name_desc":
                    storage_using = storage_using.OrderByDescending(s => s.storage.item_name).ToList();
                    break;
            }

            return storage_using;
        }

        // GET: StorageUsing/Details/5
        [Authorize(Roles = "manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            storage_using storage_using = db.storage_using.Find(id);
            if (storage_using == null)
            {
                return HttpNotFound();
            }
            return View(storage_using);
        }

        // GET: StorageUsing/Create
        [Authorize(Roles = "manager")]
        public ActionResult Create()
        {
            ViewBag.doctor_id = new SelectList(db.doctors.Where((it) => it.date_end_work == null), "id", "FIO");
            ViewBag.item_id = new SelectList(db.storage, "item_id", "item_name").Where((it) => db.storage.Find(int.Parse(it.Value)).count > 0 && db.storage.Find(int.Parse(it.Value)).is_written_off != true).ToList();

            

            storage_using storage_using = new storage_using();
            storage_using.date = DateTime.Now;

            return View(storage_using);
        }

        // POST: StorageUsing/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "manager")]
        public ActionResult Create([Bind(Include = "id,doctor_id,item_id,count,date")] storage_using storage_using)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.storage_using.Add(storage_using);
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

            ViewBag.doctor_id = new SelectList(db.doctors, "id", "FIO", storage_using.doctor_id);
            ViewBag.item_id = new SelectList(db.storage, "item_id", "item_name", storage_using.item_id);
            return View(storage_using);
        }

        // GET: StorageUsing/Edit/5
        [Authorize(Roles = "manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            storage_using storage_using = db.storage_using.Find(id);
            if (storage_using == null)
            {
                return HttpNotFound();
            }
            ViewBag.doctor_id = new SelectList(db.doctors, "id", "FIO", storage_using.doctor_id);
            ViewBag.item_id = new SelectList(db.storage, "item_id", "item_name", storage_using.item_id);
            return View(storage_using);
        }

        // POST: StorageUsing/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "manager")]
        public ActionResult Edit([Bind(Include = "id,doctor_id,item_id,count,date")] storage_using storage_using)
        {
            if (ModelState.IsValid)
            {
                db.Entry(storage_using).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.doctor_id = new SelectList(db.doctors, "id", "FIO", storage_using.doctor_id);
            ViewBag.item_id = new SelectList(db.storage, "item_id", "item_name", storage_using.item_id);
            return View(storage_using);
        }

        // GET: StorageUsing/Delete/5
        [Authorize(Roles = "manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            storage_using storage_using = db.storage_using.Find(id);
            if (storage_using == null)
            {
                return HttpNotFound();
            }
            return View(storage_using);
        }

        // POST: StorageUsing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "manager")]
        public ActionResult DeleteConfirmed(int id)
        {
            storage_using storage_using = db.storage_using.Find(id);
            db.storage_using.Remove(storage_using);
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
