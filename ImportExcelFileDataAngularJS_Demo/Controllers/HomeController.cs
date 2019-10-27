using ImportExcelFileDataAngularJS_Demo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ImportExcelFileDataAngularJS_Demo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetEmployees()
        {
            using (EmployeeContext db = new EmployeeContext())
            {
                List<Employee> employees = db.Employees.ToList();
                return Json(new { data = employees }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ImportData()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportData(List<Employee> employees)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (EmployeeContext db = new EmployeeContext())
                {
                    foreach (var i in employees)
                    {
                        db.Employees.Add(i);
                    }
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}