using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Repository.Interface;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivisionsController : ControllerBase
    {
        private IDivisionRepository _divisionRepository;
        public DivisionsController(IDivisionRepository divisionRepository)
        {
            _divisionRepository = divisionRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<DivisionVM>> Get()
        {
            return await _divisionRepository.Get();
        }
        //public IEnumerable<DivisionVM> Get()
        //{
        //    return _divisionRepository.Get();
        //}

        // GET api/<DepartmentsController>/5
        [HttpGet("{id}")]
        //public async Task<IEnumerable<DivisionVM>> GetById(int Id)
        //{
        //    return await _divisionRepository.Get(Id);
        //}
        public DivisionVM GetById(int Id)
        {
            return _divisionRepository.Get(Id);
        }

        // POST api/<DepartmentsController>
        [HttpPost]
        public IActionResult Post(DivisionVM divisionVM)
        {
            var post = _divisionRepository.Create(divisionVM);
            if (post > 0)
            {
                return Ok(post);
            }
            return BadRequest("Can't be created");
        }

        // PUT api/<DepartmentsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int Id, DivisionVM divisionVM)
        {
            var put = _divisionRepository.Update(Id, divisionVM);
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
            var delete = _divisionRepository.Delete(Id);
            if (delete > 0)
            {
                return Ok(delete);
            }
            return BadRequest("Can't be deleted");
        }
    }
}
