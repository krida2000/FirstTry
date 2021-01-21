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
    public class StoragesController : Controller
    {
        private polyclinicEntities db = new polyclinicEntities();

        // GET: Storages
        public ActionResult Index(string sortOrder, string[] SearchStrings)
        {
            var storage = db.storage.ToList();

            ViewBag.item_nameSortParm = sortOrder == "item_name" ? "item_name_desc" : "item_name";
            ViewBag.countSortParm = sortOrder == "count" ? "count_desc" : "count";
            ViewBag.priceSortParm = sortOrder == "price" ? "price_desc" : "price";
            ViewBag.expiration_dateSortParm = sortOrder == "expiration_date" ? "expiration_date_desc" : "expiration_date";
            ViewBag.is_written_offSortParm = sortOrder == "is_written_off" ? "is_written_off_desc" : "is_written_off";

            ViewData["sortOrder"] = sortOrder;

            if (SearchStrings != null && SearchStrings.Length == 1)
            {
                SearchStrings = SearchStrings[0].Split('%');
            }
            ViewBag.SearchStrings = SearchStrings;

            storage = SortData(sortOrder, storage);

            if (SearchStrings != null && SearchStrings.Length == 5)
                storage = SearchData(SearchStrings, storage);

            return View(storage);
        }

        public List<storage> SearchData(string[] SearchStrings, List<storage> storage)
        {
            if (!string.IsNullOrEmpty(SearchStrings[0]))
            {
                storage = storage.Where(s => s.item_name.ToString().Contains(SearchStrings[0])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[1]))
            {
                storage = storage.Where(s => s.count.ToString().Contains(SearchStrings[1])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[2]))
            {
                storage = storage.Where(s => s.price.ToString().Contains(SearchStrings[2])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[3]))
            {
                storage = storage.Where(s => s.expiration_date.ToString().Contains(SearchStrings[3])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[3]))
            {
                storage = storage.Where(s => s.is_written_off.ToString().Contains(SearchStrings[3])).ToList();
            }            

            return storage;
        }

        public List<storage> SortData(string sortOrder, List<storage> storage)
        {
            switch (sortOrder)
            {
                case "item_name":
                    storage = storage.OrderBy(s => s.item_name).ToList();
                    break;
                case "item_name_desc":
                    storage = storage.OrderByDescending(s => s.item_name).ToList();
                    break;
                case "count":
                    storage = storage.OrderBy(s => s.count).ToList();
                    break;
                case "count_desc":
                    storage = storage.OrderByDescending(s => s.count).ToList();
                    break;
                case "price":
                    storage = storage.OrderBy(s => s.price).ToList();
                    break;
                case "price_desc":
                    storage = storage.OrderByDescending(s => s.price).ToList();
                    break;
                case "expiration_date":
                    storage = storage.OrderBy(s => s.expiration_date).ToList();
                    break;
                case "expiration_date_desc":
                    storage = storage.OrderByDescending(s => s.expiration_date).ToList();
                    break;
                case "is_written_off":
                    storage = storage.OrderBy(s => s.is_written_off).ToList();
                    break;
                case "is_written_off_desc":
                    storage = storage.OrderByDescending(s => s.is_written_off).ToList();
                    break;
            }

            return storage;
        }

        // GET: Storages/Details/5
        [Authorize(Roles = "director, manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            storage storage = db.storage.Find(id);
            if (storage == null)
            {
                return HttpNotFound();
            }
            return View(storage);
        }

        // GET: Storages/Create
        [Authorize(Roles = "director, manager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Storages/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "director, manager")]
        public ActionResult Create([Bind(Include = "item_id,item_name,count,price,expiration_date,is_written_off")] storage storage)
        {
            if (ModelState.IsValid)
            {
                try
                {                
                    db.storage.Add(storage);
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

            return View(storage);
        }

        // GET: Storages/Edit/5
        [Authorize(Roles = "director, manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            storage storage = db.storage.Find(id);
            if (storage == null)
            {
                return HttpNotFound();
            }
            return View(storage);
        }

        // POST: Storages/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "director, manager")]
        public ActionResult Edit([Bind(Include = "item_id,item_name,count,price,expiration_date,is_written_off")] storage storage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(storage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(storage);
        }

        // GET: Storages/Delete/5
        [Authorize(Roles = "director, manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            storage storage = db.storage.Find(id);
            if (storage == null)
            {
                return HttpNotFound();
            }
            return View(storage);
        }

        // POST: Storages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "director, manager")]
        public ActionResult DeleteConfirmed(int id)
        {
            storage storage = db.storage.Find(id);
            db.storage.Remove(storage);
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
