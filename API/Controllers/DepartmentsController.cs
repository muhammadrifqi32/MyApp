using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Repository.Interface;
using API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private IDepartmentRepository _departmentRepository;
        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        //public async Task<IEnumerable<Department>> Get()
        //{
        //    return await _departmentRepository.Get();
        //}
        public async Task<IEnumerable<Department>> Get()
        {
            return await _departmentRepository.Get();
        }

        // GET api/<DepartmentsController>/5
        [HttpGet("{id}")]
        //public async Task<IEnumerable<Department>> GetById(int Id)
        //{
        //    return await _departmentRepository.Get(Id);
        //}
        public Department GetById(int Id)
        {
            return _departmentRepository.Get(Id);
        }

        // POST api/<DepartmentsController>
        [HttpPost]
        public IActionResult Post(Department department)
        {
            var post = _departmentRepository.Create(department);
            if (post > 0)
            {
                return Ok(post);
            }
            return BadRequest("Can't be created");
        }

        // PUT api/<DepartmentsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int Id, Department department)
        {
            var put = _departmentRepository.Update(Id, department);
            if (put > 0)
            {
                return Ok(put);
            }
            return BadRequest("Can't be updated");
        }

        // DELETE api/<DepartmentsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            var delete = _departmentRepository.Delete(Id);
            if (delete > 0)
            {
                return Ok(delete);
            }
            return BadRequest("Can't be deleted");
        }
    }
}
