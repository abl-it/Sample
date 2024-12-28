using Microsoft.AspNetCore.Mvc;
using Sample.Models;
using Sample.Services.IServices;

namespace Sample.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllAsync();

            return View(employees);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Detail(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        public async Task<IActionResult> Create()
        {
            var employee = new Employees();

            return View(employee);
        }
        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employees employee)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var newEmployee = new Employees();


                    // Add properties
                    newEmployee.FirstName = employee.FirstName;
                    newEmployee.LastName = employee.LastName;
                    newEmployee.Title = employee.Title;
                    newEmployee.TitleOfCourtesy = employee.TitleOfCourtesy;
                    newEmployee.Address = employee.Address;
                    newEmployee.PostalCode = employee.PostalCode;
                    newEmployee.HireDate = employee.HireDate;
                    newEmployee.BirthDate = employee.BirthDate;
                    newEmployee.City = employee.City;
                    newEmployee.Region = employee.Region;
                    newEmployee.Country = employee.Country;
                    newEmployee.HomePhone = employee.HomePhone;
                    newEmployee.Extension = employee.Extension;

                    await _employeeService.InsertAsync(newEmployee);
                    TempData["SuccessMessage"] = "Employee added successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while add employee.");
                    ModelState.AddModelError("", "An error occurred while add the employee.");
                }
            }

            //ViewBag.Jobs = await _jobRepository.GetAllAsync();
            //ViewBag.Publishers = await _publisherRepository.GetAllAsync();
            //return View(employee);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Employees employee)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    var existingEmployee = await _employeeService.GetByIdAsync(employee.EmployeeID);
                    if (existingEmployee == null)
                    {
                        return NotFound();
                    }

                    // Update properties
                    existingEmployee.FirstName = employee.FirstName;
                    existingEmployee.LastName = employee.LastName;
                    existingEmployee.Title = employee.Title;
                    existingEmployee.TitleOfCourtesy = employee.TitleOfCourtesy;
                    existingEmployee.Address = employee.Address;
                    existingEmployee.PostalCode = employee.PostalCode;
                    existingEmployee.HireDate = employee.HireDate;
                    existingEmployee.City = employee.City;
                    existingEmployee.Region = employee.Region;
                    existingEmployee.Country = employee.Country;
                    existingEmployee.HomePhone = employee.HomePhone;
                    existingEmployee.Extension = employee.Extension;

                    await _employeeService.UpdateAsync(existingEmployee);
                    TempData["SuccessMessage"] = "Employee updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while updating employee.");
                    ModelState.AddModelError("", "An error occurred while updating the employee.");
                }
            }

            //ViewBag.Jobs = await _jobRepository.GetAllAsync();
            //ViewBag.Publishers = await _publisherRepository.GetAllAsync();
            //return View(employee);
            return RedirectToAction("Index");
        }

        // GET: Employees/Delete/5
        // Implement Delete actions similarly...
    }
}
   
