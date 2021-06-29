using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelsProject;
using DataBaseProject.DBOperations;

namespace MVCMainProject.Controllers
{
    public class HomeController : Controller
    {
        EmployeRepository repository = null;
        public HomeController()
        {
            repository = new EmployeRepository();
        }
        // GET: Home
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                int id = repository.AddEmploye(model);
                if (id >0)
                {
                    ModelState.Clear();
                    ViewBag.Issuccess = "Records Save Successfully";
                }

            }
            return View();
        }


        public ActionResult GetAllRecords()
        {
            var result = repository.GetAllEmployees();
            return View(result);
        }

        public ActionResult Details(int id)
        {
            var result = repository.GetEmploye(id);
            return View(result);
        }

        public ActionResult Edit(int id)
        {
            var result = repository.GetEmploye(id);
            return View(result);
        }
        [HttpPost]
        public ActionResult Edit(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateEmploye(model.Id,model);
                return RedirectToAction("GetAllRecords");
            }
          
            return View();
        }

     
        public ActionResult Delete(int id)
        {
           
                repository.DeleteEmployee(id);

            return RedirectToAction("GetAllRecords");

        }

    }
}