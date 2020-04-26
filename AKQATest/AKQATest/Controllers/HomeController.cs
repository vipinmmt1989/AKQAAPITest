using AKQATest.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AKQATest.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Index(GetWord getWord)
        {
            string  Name = getWord.Name;

            using (var client = new HttpClient())
            {
                //Passing service base url  
             client.BaseAddress = new Uri("http://localhost:53332/");

            client.DefaultRequestHeaders.Clear();
            //Define request data format  
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
            HttpResponseMessage Res = await client.GetAsync("api/Values/Get?number=" + getWord.Word);

            //Checking the response is successful or not which is sent using HttpClient  
            if (Res.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var apiResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list

                    getWord = JsonConvert.DeserializeObject<GetWord>(apiResponse);
                    getWord.Name = Name;
            }
            if (ModelState.IsValid)
            {
                return RedirectToAction("Details", new {name= getWord.Name,word=getWord.Word });
            }
            //returning the employee list to view  
            return View();
        }
        }

        public ActionResult Details(string name,string word)
        {
            var model = new GetWord();
            model.Name = name;
            model.Word = word.ToUpper();
            return View(model);
        }

    }
}