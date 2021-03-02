using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrudNoEF.Models;
using System.Data.Entity;

namespace CrudNoEF.Controllers
{
    public class EmployeeController : Controller
    {
        private DBContextEntities _db = new DBContextEntities();
        public ActionResult Index()
        {
            var data = _db.Employees.ToList();
            ViewBag.Employees = data;

            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmployeeViewModel empVM)
        {
            if (ModelState.IsValid)
            {
                Employee empList = new Employee();
                empList.EmpName = empVM.EmpName;
                empList.EmpLastName = empVM.EmpLastName;
                empList.EmpMidName = empVM.EmpMidName;
                empList.DateHired = empVM.DateHired;
                empList.DateCreated = empVM.DateCreated;

                _db.Employees.Add(empList);
                _db.SaveChanges();

                return RedirectToAction("Index");

            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {   

            var data = _db.Employees.Where(x => x.EmployeeID == id).FirstOrDefault();
            EmployeeViewModel empList = new EmployeeViewModel();
            empList.EmployeeID = data.EmployeeID;
            empList.EmpName = data.EmpName;
            empList.EmpMidName = data.EmpMidName;
            empList.EmpLastName = data.EmpLastName;
            empList.DateCreated = data.DateCreated;
            empList.DateHired = data.DateHired;

            return View(empList);

        }

        [HttpPost]
        public ActionResult Edit(EmployeeViewModel empVM)
        {
            if (ModelState.IsValid)
            {
                Employee empList = new Employee();
                empList.EmployeeID = empVM.EmployeeID;
                empList.EmpName = empVM.EmpName;
                empList.EmpMidName = empVM.EmpMidName;
                empList.EmpLastName = empVM.EmpLastName;
                empList.DateHired = empVM.DateHired;
                empList.DateCreated = empVM.DateCreated;

                _db.Entry(empList).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
            
        }

        public ActionResult Details(int? id)
        {
            var data = _db.Employees.Where(x => x.EmployeeID == id).FirstOrDefault();
            EmployeeViewModel empList = new EmployeeViewModel();
            empList.EmployeeID = data.EmployeeID;
            empList.EmpName = data.EmpName;
            empList.EmpMidName = data.EmpMidName;
            empList.EmpLastName = data.EmpLastName;
            empList.DateCreated = data.DateCreated;
            empList.DateHired = data.DateHired;

            return View(empList);
        }

        public ActionResult Delete(int id)
        {
            var data =  _db.Employees.Where(x=>x.EmployeeID == id).FirstOrDefault();

            _db.Employees.Remove(data);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}