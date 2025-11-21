using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using TestProject.Context;
using TestProject.Model;

namespace TestProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly App_context _context;

        public EmployeeController(App_context Context )
        {
            _context = Context;
        }



        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult GetAll()
        {

            var result=_context.Employees.ToList();

            return Ok(result);
        }


        [HttpGet]
        
        [Route("{id:int}")]
        [Authorize(Roles = "user")]
        public IActionResult GetById(int id)
        {

            var result=_context.Employees.FirstOrDefault(e => e.Id == id);
            if (result == null)
                return NotFound($"the id {id} not exsistes");
            return Ok(result);
        }


        [HttpPost]

        [Authorize(Roles = "admin")]
        public IActionResult CreateNewEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var add=_context.Employees.Add(employee);
                _context.SaveChanges();
                return Ok($"The employee was successfully added{employee}  ");
            }
            return BadRequest();
           
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateEmployee(int id)
        {
            if (ModelState.IsValid)
            {
                var update= _context.Employees.Find(id);
                if (update == null)
                    return BadRequest("Employee not found");
                
                _context.Entry(update).State=EntityState.Modified;
                _context.SaveChanges();
                return Ok($"The employee was successfully updated{update}  ");
            }
            return BadRequest();

        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteEmployee(int id)
        {

                var employee = _context.Employees.Find(id);
                if (employee == null)
                 return BadRequest("Employee not found");

                 _context.Remove(employee);
                 _context.SaveChanges();

                return Ok($"The employee was successfully deleted{employee}  ");
            
            

        }





    }
}
