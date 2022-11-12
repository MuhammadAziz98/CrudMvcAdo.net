using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ado.netMVC.Models;

namespace Ado.netMVC.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            EmployeeDbContext db = new EmployeeDbContext();

            List<EmployeeModel> employees = db.GetEmployeesList();

            return View(employees);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmployeeModel e)
        {
            if (ModelState.IsValid)
            {
                EmployeeDbContext context = new EmployeeDbContext();
                //check if data is inserted successfully
                bool check=context.addEmploy(e);

                if (check)
                {
                    TempData["MsgInsert"] = "Data Inserted Successfully!";
                    ModelState.Clear();
                    //redirect to index Action page
                    return RedirectToAction("index");

                }
            }

            return View();
        }

        public ActionResult EditEmploy()
        {
            return View();
        }

        public ActionResult DelEmploy()
        {
            return View();
        }

    }
}