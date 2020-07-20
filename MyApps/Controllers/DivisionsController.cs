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
    public class DivisionsController : Controller
    {

        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44357/api/")
        };


        public IActionResult Index()
        {
            return View();
        }

        public JsonResult LoadDivision()
        {
            IEnumerable<DivisionVM> division = null;
            var responseTask = client.GetAsync("Divisions");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<DivisionVM>>();
                readTask.Wait();
                division = readTask.Result;
            }
            else
            {
                division = Enumerable.Empty<DivisionVM>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(division);
        }

        public JsonResult GetById(int Id)
        {
            DivisionVM divisionVM = null;
            var responseTask = client.GetAsync("Divisions/" + Id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                divisionVM = JsonConvert.DeserializeObject<DivisionVM>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(divisionVM);
        }

        public JsonResult InsertOrUpdate(DivisionVM divisionVM)
        {
            var myContent = JsonConvert.SerializeObject(divisionVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (divisionVM.Id == 0)
            {
                var result = client.PostAsync("Divisions", byteContent).Result;
                return Json(result);
            }
            else
            {
                var result = client.PutAsync("Divisions/" + divisionVM.Id, byteContent).Result;
                return Json(result);
            }
        }

        public JsonResult Delete(int Id)
        {
            var result = client.DeleteAsync("Divisions/" + Id).Result;
            return Json(result);
        }
    }
}
