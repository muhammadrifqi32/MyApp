using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using API.Models;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MyApps.Controllers
{
    public class DepartmentsController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44357/api/")
        };

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult LoadDepartment()
        {
            IEnumerable<Department> department = null;
            var responseTask = client.GetAsync("Departments");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Department>>();
                readTask.Wait();
                department = readTask.Result;
            }
            else
            {
                department = Enumerable.Empty<Department>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(department);
        }

        public JsonResult GetById(int Id)
        {
            Department department = null;
            var responseTask = client.GetAsync("Departments/" + Id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                department = JsonConvert.DeserializeObject<Department>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            return Json(department);            
        }

        public JsonResult InsertOrUpdate(Department department)
        {
            var myContent = JsonConvert.SerializeObject(department);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (department.Id == 0)
            {
                var result = client.PostAsync("Departments", byteContent).Result;
                return Json(result);
            }
            else
            {
                var result = client.PutAsync("Departments/" + department.Id, byteContent).Result;
                return Json(result);
            }
        }

        public JsonResult Delete(int Id)
        {
            var result = client.DeleteAsync("Departments/" + Id).Result;
            return Json(result);
        }
    }
}
