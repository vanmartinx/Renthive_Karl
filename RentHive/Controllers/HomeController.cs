using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentHive.Models;
using System.Diagnostics;

namespace RentHive.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userData = HttpContext.Session.GetString("UserData");
            if (string.IsNullOrEmpty(userData))
            {
                // User is not logged in, redirect to login or handle as needed
                return RedirectToAction("Login", "UserManagement");
            }
            string url = "https://renthive.online/Admin_API/HomeViewer.php";
            try
            {
                var userObject = JsonConvert.DeserializeObject<UserDataGetter>(userData);
                using (var httpClient = new HttpClient())
                {
                    var data = new Dictionary<string, string>
                    {
                        {"userId", userObject.Acc_id.ToString()}
                    };

                    var content = new FormUrlEncodedContent(data);

                    var response = await httpClient.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();

                        if (responseData == "Something went wrong.")
                        {
                            ViewBag.ErrorMessage = string.Format("Something went wrong.");
                        }
                        else
                        {
                            var newuserObject = JsonConvert.DeserializeObject<UserDataGetter>(responseData);
                            return View(newuserObject);
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = string.Format("API request failed");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = string.Format("Handle exceptions error");
            }
            return View();
        }
        public IActionResult WelcomePage()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}