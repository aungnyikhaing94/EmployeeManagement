using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDBContext _db;

        public EmployeeController(ApplicationDBContext db)
        {
            _db = db;
        }

        // GET - Index
        public IActionResult Index()
        {
            IEnumerable<Employee> objList = _db.Employee.Include(u => u.Position).Include(u => u.Department);
            return View(objList);
        }

        //POST - Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(string searchStr)
        {
            int searchEmpNo;
            IEnumerable<Employee> objList;

            bool success = int.TryParse(searchStr, out searchEmpNo);

            if (success)
            {
                objList = _db.Employee.Include(e => e.Position).Include(e => e.Department).Where(e => e.Position.Name == searchStr || e.Department.Name == searchStr || e.empName == searchStr || e.empNo == searchEmpNo);
            }
            else
            {
                objList = _db.Employee.Include(e => e.Position).Include(e => e.Department).Where(e => e.Position.Name == searchStr || e.Department.Name == searchStr || e.empName == searchStr);
            }

            return View(objList);
        }

        //GET - CREATE
        public IActionResult Create()
        {
            EmployeeVM employeeVM = new EmployeeVM()
            {
                Employee = new Employee(),
                PositionSelectList = _db.Position.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                DepartmentSelectList = _db.Department.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            return View(employeeVM);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeVM employeeVM)
        {
            if (ModelState.IsValid)
            {
                _db.Employee.Add(employeeVM.Employee);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employeeVM);
        }

        //GET - EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Employee.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            EmployeeVM employeeVM = new EmployeeVM()
            {
                Employee = obj,
                PositionSelectList = _db.Position.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                DepartmentSelectList = _db.Department.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            return View(employeeVM);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeVM employeeVM)
        {
            if (ModelState.IsValid)
            {
                _db.Employee.Update(employeeVM.Employee);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employeeVM);
        }

        //GET - DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Employee.Include(e => e.Position).Include(e => e.Department).FirstOrDefault(e => e.empNo == id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST - DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? empNo)
        {
            var obj = _db.Employee.Find(empNo);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Employee.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET - GetPositionByDepartmentId
        public IActionResult GetPositionsByDepartmentId(int departmentId)
        {
            var Positions = _db.Position.Where(p => p.DepartmentId == departmentId)
                                        .Select(i => new SelectListItem {
                                            Text = i.Name,
                                            Value = i.Id.ToString()
                                        }).ToList();

            return Json(Positions);
        }
    }
}
