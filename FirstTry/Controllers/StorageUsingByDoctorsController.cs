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
    public class StorageUsingByDoctorsController : Controller
    {
        private polyclinicEntities db = new polyclinicEntities();

        // GET: StorageUsing
        [Authorize(Roles = "director, manager")]
        public ActionResult Index(int? doctorId, string sortOrder, string[] SearchStrings)
        {
            var storage_using_by_doctors = db.storage_using_by_doctors.ToList(); 
            
            if(doctorId != null)
            {
                storage_using_by_doctors = storage_using_by_doctors.FindAll((it) => it.id == doctorId);
                ViewData["doctorId"] = doctorId;
            }

            ViewBag.FIOSortParm = sortOrder == "FIO" ? "FIO_desc" : "FIO";
            ViewBag.Item_nameSortParm = sortOrder == "Item_name" ? "Item_name_desc" : "Item_name";
            ViewBag.CountSortParm = sortOrder == "Count" ? "Count_desc" : "Count";
            ViewBag.PercentSortParm = sortOrder == "Percent" ? "Percent_desc" : "Percent";

            ViewData["sortOrder"] = sortOrder;

            if (SearchStrings != null && SearchStrings.Length == 1)
            {
                SearchStrings = SearchStrings[0].Split('%');
            }
            ViewBag.SearchStrings = SearchStrings;

            storage_using_by_doctors = SortData(sortOrder, storage_using_by_doctors);

            if (SearchStrings != null && SearchStrings.Length == 4)
                storage_using_by_doctors = SearchData(SearchStrings, storage_using_by_doctors);

            return View(storage_using_by_doctors);
        }

        public List<storage_using_by_doctors> SearchData(string[] SearchStrings, List<storage_using_by_doctors> storage_using_by_doctors)
        {
            if (!string.IsNullOrEmpty(SearchStrings[0]))
            {
                storage_using_by_doctors = storage_using_by_doctors.Where(s => s.FIO.ToString().Contains(SearchStrings[0])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[1]))
            {
                storage_using_by_doctors = storage_using_by_doctors.Where(s => s.Item_name.ToString().Contains(SearchStrings[1])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[2]))
            {
                storage_using_by_doctors = storage_using_by_doctors.Where(s => s.Count.ToString().Contains(SearchStrings[2])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[3]))
            {
                storage_using_by_doctors = storage_using_by_doctors.Where(s => s.Percent.ToString().Contains(SearchStrings[3])).ToList();
            }

            return storage_using_by_doctors;
        }

        public List<storage_using_by_doctors> SortData(string sortOrder, List<storage_using_by_doctors> storage_using_by_doctors)
        {
            switch (sortOrder)
            {
                case "FIO":
                    storage_using_by_doctors = storage_using_by_doctors.OrderBy(s => s.FIO).ToList();
                    break;
                case "FIO_desc":
                    storage_using_by_doctors = storage_using_by_doctors.OrderByDescending(s => s.FIO).ToList();
                    break;
                case "Item_name":
                    storage_using_by_doctors = storage_using_by_doctors.OrderBy(s => s.Item_name).ToList();
                    break;
                case "Item_name_desc":
                    storage_using_by_doctors = storage_using_by_doctors.OrderByDescending(s => s.Item_name).ToList();
                    break;
                case "Count":
                    storage_using_by_doctors = storage_using_by_doctors.OrderBy(s => s.Count).ToList();
                    break;
                case "Count_desc":
                    storage_using_by_doctors = storage_using_by_doctors.OrderByDescending(s => s.Count).ToList();
                    break;
                case "Percent":
                    storage_using_by_doctors = storage_using_by_doctors.OrderBy(s => s.Percent).ToList();
                    break;
                case "Percent_desc":
                    storage_using_by_doctors = storage_using_by_doctors.OrderByDescending(s => s.Percent).ToList();
                    break;
            }

            return storage_using_by_doctors;
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
