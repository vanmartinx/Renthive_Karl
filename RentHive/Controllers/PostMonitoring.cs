using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentHive.Models;

namespace RentHive.Controllers
{
    public class PostMonitoring : Controller
    {
        public async Task<IActionResult> HiveUserPostList(UserDataGetter TempData)
        {
            var userData = HttpContext.Session.GetString("UserData");
            if (string.IsNullOrEmpty(userData))
            {
                // User is not logged in, redirect to login or handle as needed
                return RedirectToAction("Login", "UserManagement");
            }
            string url = "https://renthive.online/Admin_API/ViewUsersList.php";
                try
                {
                    using (var client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.GetAsync(url);

                        if (response.IsSuccessStatusCode)
                        {
                            // Parse the response content
                            var responseData = await response.Content.ReadAsStringAsync();

                            if (responseData == "No Users found")
                            {
                                ViewBag.ErrorMessage = string.Format("No users found.");
                            }
                            else
                            {
                                //Successfully retrieving data 
                                var userObject = JsonConvert.DeserializeObject<List<UserDataGetter>>(responseData);
                                ViewBag.Acc_Id = TempData.Acc_id;
                                ViewBag.Acc_FirstName = TempData.Acc_FirstName;
                                ViewBag.Acc_MiddleName = TempData.Acc_MiddleName;
                                ViewBag.Acc_LastName = TempData.Acc_LastName;
                                ViewBag.Acc_DisplayName = TempData.Acc_DisplayName;
                                ViewBag.Acc_UserType = TempData.Acc_UserType;
                                return View(userObject);
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
                    ViewBag.ErrorMessage = string.Format("No record found");
                    ViewBag.Acc_Id = TempData.Acc_id;
                    ViewBag.Acc_FirstName = TempData.Acc_FirstName;
                    ViewBag.Acc_MiddleName = TempData.Acc_MiddleName;
                    ViewBag.Acc_LastName = TempData.Acc_LastName;
                    ViewBag.Acc_DisplayName = TempData.Acc_DisplayName;
                    ViewBag.Acc_UserType = TempData.Acc_UserType;
                    return View(new List<UserDataGetter>());// Returns an emtpy list 
                }
                return View();
        }
        public async Task<IActionResult> HiveUserPostDetails(int userID, UserDataGetter TempData)
        {
            var userData = HttpContext.Session.GetString("UserData");
            if (string.IsNullOrEmpty(userData))
            {
                // User is not logged in, redirect to login or handle as needed
                return RedirectToAction("Login", "UserManagement");
            }
            string url = "https://renthive.online/Admin_API/ViewUsersDetails.php";
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        int userId = userID;
                        var data = new Dictionary<string, string>
                        {
                            {"userId", userId.ToString()}
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
                                var userObject = JsonConvert.DeserializeObject<UserDataGetter>(responseData);

                                ViewBag.UserId = userID; // the selected user

                                //Admin info
                                ViewBag.Acc_Id = TempData.Acc_id;
                                ViewBag.Acc_FirstName = TempData.Acc_FirstName;
                                ViewBag.Acc_MiddleName = TempData.Acc_MiddleName;
                                ViewBag.Acc_LastName = TempData.Acc_LastName;
                                ViewBag.Acc_DisplayName = TempData.Acc_DisplayName;
                                ViewBag.Acc_UserType = TempData.Acc_UserType;

                                return View(userObject);
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
                    ViewBag.ErrorMessage = string.Format("No user record found");
                    ViewBag.Acc_Id = TempData.Acc_id;
                    ViewBag.Acc_FirstName = TempData.Acc_FirstName;
                    ViewBag.Acc_MiddleName = TempData.Acc_MiddleName;
                    ViewBag.Acc_LastName = TempData.Acc_LastName;
                    ViewBag.Acc_DisplayName = TempData.Acc_DisplayName;
                    ViewBag.Acc_UserType = TempData.Acc_UserType;
                    return View(new List<UserDataGetter>());// Returns an emtpy list 
                }
                return View();
            
        }
        public async Task<IActionResult> ListOfPost(int userID, string fname, string mname, string lname, UserDataGetter TempData)
        {
            var userData = HttpContext.Session.GetString("UserData");
            if (string.IsNullOrEmpty(userData))
            {
                // User is not logged in, redirect to login or handle as needed
                return RedirectToAction("Login", "UserManagement");
            }
            string url = "https://renthive.online/Admin_API/PostListGetter.php";
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        int userId = userID;
                        var data = new Dictionary<string, string>
                        {
                            {"userId", userId.ToString()}
                        };

                        var content = new FormUrlEncodedContent(data);
                        var response = await httpClient.PostAsync(url, content);

                        if (response.IsSuccessStatusCode)
                        {
                            // Parse the response content
                            var responseData = await response.Content.ReadAsStringAsync();

                            if (responseData == "No Users found")
                            {
                                ViewBag.ErrorMessage = string.Format("No users found.");
                            }
                            else
                            {
                                //Successfully retrieving data 
                                var userObject = JsonConvert.DeserializeObject<List<UserPostGetter>>(responseData);
                                //selected user's id
                                ViewBag.selecteUser = userID;
                                ViewBag.fname = fname;
                                ViewBag.mname = mname;
                                ViewBag.lname = lname;

                                ViewBag.Acc_Id = TempData.Acc_id;
                                ViewBag.Acc_FirstName = TempData.Acc_FirstName;
                                ViewBag.Acc_MiddleName = TempData.Acc_MiddleName;
                                ViewBag.Acc_LastName = TempData.Acc_LastName;
                                ViewBag.Acc_DisplayName = TempData.Acc_DisplayName;
                                ViewBag.Acc_UserType = TempData.Acc_UserType;
                                return View(userObject);
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
                    ViewBag.ErrorMessage = string.Format("No record found");
                    ViewBag.Acc_Id = TempData.Acc_id;
                    ViewBag.Acc_FirstName = TempData.Acc_FirstName;
                    ViewBag.Acc_MiddleName = TempData.Acc_MiddleName;
                    ViewBag.Acc_LastName = TempData.Acc_LastName;
                    ViewBag.Acc_DisplayName = TempData.Acc_DisplayName;
                    ViewBag.Acc_UserType = TempData.Acc_UserType;
                    return View(new List<UserPostGetter>());// Returns an emtpy list 
                }
                return View();
        }
        [HttpGet]
        public async Task<IActionResult> SelectedPostDetails(int userID, int postID, string fname, string mname, string lname, UserDataGetter TempData)
        {
            var userData = HttpContext.Session.GetString("UserData");
            if (string.IsNullOrEmpty(userData))
            {
                // User is not logged in, redirect to login or handle as needed
                return RedirectToAction("Login", "UserManagement");
            }
            string url = "https://renthive.online/Admin_API/PostDetailsGetter.php";
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        var data = new Dictionary<string, string>
                        {
                            {"userId", userID.ToString() },
                            {"postId", postID.ToString() }
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
                                var userObject = JsonConvert.DeserializeObject<UserPostGetter>(responseData);

                                ViewBag.UserId = userID; // the selected user
                                ViewBag.fname = fname;
                                ViewBag.mname = mname;
                                ViewBag.lname = lname;
                                ViewBag.postID = postID;

                                //Admin info
                                ViewBag.Acc_Id = TempData.Acc_id;
                                ViewBag.Acc_FirstName = TempData.Acc_FirstName;
                                ViewBag.Acc_MiddleName = TempData.Acc_MiddleName;
                                ViewBag.Acc_LastName = TempData.Acc_LastName;
                                ViewBag.Acc_DisplayName = TempData.Acc_DisplayName;
                                ViewBag.Acc_UserType = TempData.Acc_UserType;

                                return View(userObject);
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
                    ViewBag.ErrorMessage = string.Format("No user record found");
                    ViewBag.Acc_Id = TempData.Acc_id;
                    ViewBag.Acc_FirstName = TempData.Acc_FirstName;
                    ViewBag.Acc_MiddleName = TempData.Acc_MiddleName;
                    ViewBag.Acc_LastName = TempData.Acc_LastName;
                    ViewBag.Acc_DisplayName = TempData.Acc_DisplayName;
                    ViewBag.Acc_UserType = TempData.Acc_UserType;
                    return View();
                }
                return View();
        }
        [HttpPost]
        public async Task<IActionResult> SelectedPostDetails(int userID, int postID, int status, string fname, string mname, string lname, UserDataGetter TempData)
        {
            var userData = HttpContext.Session.GetString("UserData");
            if (string.IsNullOrEmpty(userData))
            {
                // User is not logged in, redirect to login or handle as needed
                return RedirectToAction("Login", "UserManagement");
            }
            if (status == 1)
            {
                string url = "https://renthive.online/Admin_API/PostStatusTO_0.php";
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        var data = new Dictionary<string, string>
                        {
                            { "userId", userID.ToString() },
                            { "postId", postID.ToString() }
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
                                // Update successful.
                                //int userID, int postID, string fname, string mname, string lname, UserDataGetter TempData
                                return RedirectToAction("SelectedPostDetails", new
                                {
                                    userID = userID, //selected user
                                    fname = fname,
                                    mname = mname,
                                    lname = lname,
                                    postID = postID,

                                    //admin info
                                    Acc_id = TempData.Acc_id,
                                    Acc_FirstName = TempData.Acc_FirstName,
                                    Acc_MiddleName = TempData.Acc_MiddleName,
                                    Acc_LastName = TempData.Acc_LastName,
                                    Acc_DisplayName = TempData.Acc_DisplayName,
                                    Acc_UserType = TempData.Acc_UserType
                                });

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
            }
            else
            {
                string url = "https://renthive.online/Admin_API/PostStatusTO_1.php";
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        var data = new Dictionary<string, string>
                        {
                            { "userId", userID.ToString() },
                            { "postId", postID.ToString() }
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
                                // Update successful.
                                return RedirectToAction("SelectedPostDetails", new
                                {
                                    userID = userID, //selected user
                                    fname = fname,
                                    mname = mname,
                                    lname = lname,
                                    postID = postID,

                                    //admin info
                                    Acc_id = TempData.Acc_id,
                                    Acc_FirstName = TempData.Acc_FirstName,
                                    Acc_MiddleName = TempData.Acc_MiddleName,
                                    Acc_LastName = TempData.Acc_LastName,
                                    Acc_DisplayName = TempData.Acc_DisplayName,
                                    Acc_UserType = TempData.Acc_UserType
                                });

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
            }
            return View();
        }
    }
}
