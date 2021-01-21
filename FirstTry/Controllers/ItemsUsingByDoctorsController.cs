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
    public class ItemsUsingByDoctorsController : Controller
    {
        private polyclinicEntities db = new polyclinicEntities();

        // GET: StorageUsing
        [Authorize(Roles = "director, manager")]
        public ActionResult Index(int? itemId, string sortOrder, string[] SearchStrings)
        {
            
            var items_using_by_doctors = db.items_using_by_doctors.ToList();

            if (itemId != null && itemId != -1)
            {
                items_using_by_doctors = items_using_by_doctors.FindAll((it) => it.item_id == itemId).ToList();
                ViewBag.Item = db.storage.Find(itemId).item_name;
                ViewData["itemId"] = itemId;                
            }

            ViewBag.FIOSortParm = sortOrder == "FIO" ? "FIO_desc" : "FIO";
            ViewBag.CountSortParm = sortOrder == "Count" ? "Count_desc" : "Count";
            ViewBag.PercentSortParm = sortOrder == "Percent" ? "Percent_desc" : "Percent";

            ViewData["sortOrder"] = sortOrder;

            if (SearchStrings != null && SearchStrings.Length == 1)
            {
                SearchStrings = SearchStrings[0].Split('%');
            }
            ViewBag.SearchStrings = SearchStrings;

            items_using_by_doctors = SortData(sortOrder, items_using_by_doctors);

            if (SearchStrings != null && SearchStrings.Length == 3)
                items_using_by_doctors = SearchData(SearchStrings, items_using_by_doctors);

            return View(items_using_by_doctors);
        }        

        public List<items_using_by_doctors> SearchData(string[] SearchStrings, List<items_using_by_doctors> items_using_by_doctors)
        {
            if (!string.IsNullOrEmpty(SearchStrings[0]))
            {
                items_using_by_doctors = items_using_by_doctors.Where(s => s.FIO.Contains(SearchStrings[0])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[1]))
            {
                items_using_by_doctors = items_using_by_doctors.Where(s => s.Count.ToString().Contains(SearchStrings[1])).ToList();
            }
            if (!string.IsNullOrEmpty(SearchStrings[2]))
            {
                items_using_by_doctors = items_using_by_doctors.Where(s => s.Percent.Contains(SearchStrings[2])).ToList();
            }            

            return items_using_by_doctors;
        }

        public List<items_using_by_doctors> SortData(string sortOrder, List<items_using_by_doctors> items_using_by_doctors)
        {
            switch (sortOrder)
            {
                case "FIO":
                    items_using_by_doctors = items_using_by_doctors.OrderBy(s => s.FIO).ToList();
                    break;
                case "FIO_desc":
                    items_using_by_doctors = items_using_by_doctors.OrderByDescending(s => s.FIO).ToList();
                    break;
                case "Count":
                    items_using_by_doctors = items_using_by_doctors.OrderBy(s => s.Count).ToList();
                    break;
                case "Count_desc":
                    items_using_by_doctors = items_using_by_doctors.OrderByDescending(s => s.Count).ToList();
                    break;
                case "Percent":
                    items_using_by_doctors = items_using_by_doctors.OrderBy(s => s.Percent).ToList();
                    break;
                case "Percent_desc":
                    items_using_by_doctors = items_using_by_doctors.OrderByDescending(s => s.Percent).ToList();
                    break;               
            }

            return items_using_by_doctors;
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
