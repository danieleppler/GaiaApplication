using System.Diagnostics;
using System.Text;
using System.Text.Json;
using GaiaApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NPOI.XWPF.UserModel;

namespace GaiaApplication.Controllers
{

    //here we get string command from the view, and send corresponding ICommand object to an api , getting result from api and send it back to the view 
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

 

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
        {
            
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetFromJsonAsync<List<string>>("https://localhost:7229/api/Commands/getCommands");
            var model = new IndexViewModel
            {
                Options = response
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(IndexViewModel model)
        {

            string json = "{\"input_a\":\"" + model.input_a + "\",\"input_b\":\"" + model.input_b + "\",\"command\":\"" + model.SelectedOption + "\"}";
                
            JsonRequestBody request = JsonConvert.DeserializeObject<JsonRequestBody>(json);

           
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("https://localhost:7229/api/Commands/execute", content);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Error calling Command API");
            }

            var result = await response.Content.ReadAsStringAsync();
            model.Answer = result;

            var commands_res = await _httpClient.GetFromJsonAsync<List<string>>("https://localhost:7229/api/Commands/getCommands");

            model.Options = commands_res;
            return View(model);
        }

   
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
