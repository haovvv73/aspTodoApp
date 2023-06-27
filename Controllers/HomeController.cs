
using ASPLab05.Dbcontext;
using ASPLab05.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;

namespace ASPLab05.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _contextAccessor;
        CompanyDbContext companyDBContext;

        public HomeController(ILogger<HomeController> logger,IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _contextAccessor = httpContextAccessor;
            companyDBContext = new CompanyDbContext();
        }

        // view action
        public IActionResult Index()
        {
            // get data
            List<Models.Employee> newdata = DBGet();

            // save session
            string newdataString = JsonConvert.SerializeObject(newdata);
            _contextAccessor.HttpContext.Session.SetString("data",newdataString);

            // view
            return View(newdata);
        }

        public IActionResult Search()
        {
            // get list data
            string dataString = _contextAccessor.HttpContext.Session.GetString("data");
            List<Models.Employee> dataList = JsonConvert.DeserializeObject<List<Models.Employee>>(dataString);

            // result []
            List<Models.Employee> result = new List<Models.Employee>();

            // get search key
            string newkey = Request.Query["text"].ToString();

            // find
            foreach (Models.Employee item in dataList)
            {
                if (item.name.Contains(newkey)) result.Add(item);
            }

            // view
            return View("Index", result);
        }
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult CreateAction(Models.Employee employee)
        {
            // DB create and add
            DBAdd(new Models.Employee
            {
                city= employee.city,
                salary= employee.salary,
                name= employee.name,
            });

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int custID)
        {
            // get list employee from session
            string dataString = _contextAccessor.HttpContext.Session.GetString("data");
            List<Models.Employee> dataList = JsonConvert.DeserializeObject<List<Models.Employee>>(dataString);

            // get employee
            Models.Employee em = dataList.Where(item => item.Id == custID).First();

            // save old employee to Tempdata
            TempData["oldEmployee"] = JsonConvert.SerializeObject(em);

            return View(em);
        }

        public IActionResult Delete(int custID)
        {
            // DB delete
            DBDelete(custID);

            // view index
            return RedirectToAction("Index");
        }

        public IActionResult Update(Models.Employee employee)
        {
            // db update
            DBUpdate(employee);

            // new employee
            ViewBag.employee = employee;

            // old employee from Tempdata
            string o = TempData["oldEmployee"] as string;
            Models.Employee em = JsonConvert.DeserializeObject<Models.Employee>(o);
            ViewBag.oldEmployee = em;

            return View();
        }

        // DB method action
        public List<Models.Employee> DBGet()
        {
            var list = companyDBContext.Employees.ToList();

            return list.Select(u => new Models.Employee
            {
                Id = u.Id,
                name = u.Name,
                city = u.City,
                salary = u.Salary,
            }).ToList();
        }

        public void DBUpdate(Models.Employee employee) 
        {
            Dbcontext.Employee em = companyDBContext.Employees.SingleOrDefault(u => u.Id.Equals(employee.Id));
            em.Name = employee.name;
            em.City = employee.city;
            em.Salary = employee.salary;

            companyDBContext.SaveChanges();
        }
        
        public void DBDelete(int custID)
        {
            Dbcontext.Employee em = companyDBContext.Employees.SingleOrDefault(u => u.Id.Equals(custID));
            companyDBContext.Employees.Remove(em);

            companyDBContext.SaveChanges();
        }

        public void DBAdd(Models.Employee employee)
        {
            companyDBContext.Employees.Add(new Dbcontext.Employee
            {
                Name = employee.name,
                City = employee.city,
                Salary = employee.salary,
            });
            companyDBContext.SaveChanges();
        }
    }
}