
using System;
using System.Linq;

using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Mpower.Models.ClientViewModels;
using Newtonsoft.Json;
using PaulMiami.AspNetCore.Mvc.Recaptcha;


namespace Mpower.Travel.Web.Admin.Controllers
{
    [AuthorizeAttribute]
    public class HomeController : Controller
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private HttpClient client;
        private ApplicationSetting _Setting;
        private string _Url;
        private string _OxirailUrl;
        private string _apiUrl;
        public HomeController(SignInManager<ApplicationUser> signInManager, IOptions<ApplicationSetting> AppSetting)
        {
            _signInManager = signInManager;
            _Setting = AppSetting.Value;
            _apiUrl = _Setting.ApiOxirail;
            _Url = _Setting.ApiUrls + "/Configuration/ApplicationSettings/" + _Setting.applicationId;
            _OxirailUrl = _Setting.ApiOxirail + "/Mpower/Rail/UserRegistration/GetAgentByAccountNumber/";
            client = new HttpClient();
            client.BaseAddress = new System.Uri(_Url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        [AllowAnonymousAttribute]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymousAttribute]
        public IActionResult Oxi_Home()
        {
            return View();
        }


        [Authorize(Roles = "Agent")]
        public IActionResult PnrStatus()
        {
            return View();
        }


        [Authorize(Roles = "Agent")]
        public IActionResult PnrStatus(string pnrNo)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(HttpContext.Session.GetString("accountNo"))) && !string.IsNullOrEmpty(pnrNo))
            {
                HttpResponseMessage responseMessage = client.GetAsync(_Url).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var ViewSetting = JsonConvert.DeserializeObject<Application_ResponseWrapper>(responseData);
                    //var response = (JsonConvert.DeserializeObject<ApplicationConfigurationViewModel>(ViewSetting.ResponseResult.ToString()));

                    return Ok(ViewSetting);
                }
            }
            return View();
        }

        [AllowAnonymousAttribute]
        public IActionResult Content()
        {
            return View();
        }

        [Authorize(Roles = "Agent")]
        public IActionResult PassengerTravelList()
        {
            return View();
        }
        [Authorize(Roles = "Agent")]
        public IActionResult TrainAtGlance()
        {
            return View();
        }

        [Authorize(Roles = "Agent")]
        public IActionResult PassengerList()
        {
            return View();
        }

        [Authorize(Roles = "Agent")]
        public IActionResult PendingTickets()
        {
            return View();
        }

        //Transaction Details Action Methods
        [Authorize(Roles = "Agent")]
        public IActionResult BookedTicket()
        {
            return View();
        }

        [Authorize(Roles = "Agent")]
        public IActionResult CancelTicket()
        {
            return View();
        }

        [Authorize(Roles = "Agent")]
        public IActionResult TDRHistory()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Agent")]
        public IActionResult DashBoard()
        {
            return View();
        }

        [Authorize(Roles = "Agent")]
        public IActionResult PopularStations()
        {
            return View();
        }
        [AllowAnonymousAttribute]
        public IActionResult Default()
        {
            HttpResponseMessage responseMessage = client.GetAsync(_Url).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var ViewSetting = JsonConvert.DeserializeObject<Application_ResponseWrapper>(responseData);
                var response = (JsonConvert.DeserializeObject<ApplicationConfigurationViewModel>(ViewSetting.ResponseResult.ToString()));
                ViewData["Layout"] = response.layoutName;
            }
            return View();
        }

        [AllowAnonymousAttribute]
        [HttpPostAttribute]
        [ValidateRecaptcha]
        public async Task<IActionResult> login(login model)
        {
            if (ModelState.IsValid)
            {
                string email = "";
                HttpResponseMessage responseMessage = client.GetAsync(_OxirailUrl + model.accountNo).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var ViewSetting = JsonConvert.DeserializeObject<Application_ResponseWrapper>(responseData);
                    if (ViewSetting.ResponseResult != null)
                    {
                        var response = (JsonConvert.DeserializeObject<Mpower.Rail.Model.Request.UserRegistration>(ViewSetting.ResponseResult.ToString()));
                        email = response.email;
                        var result = await _signInManager.PasswordSignInAsync(email, model.password, model.rememberPassword, lockoutOnFailure: false);
                        if (result.Succeeded)
                        {
                            HttpContext.Session.SetString("accountNo", model.accountNo);
                            TempData["msg"] = "";
                            HttpContext.Session.SetString("LoginId", model.accountNo);
                            return RedirectToAction("DashBoard");
                        }
                        else
                        {
                            TempData["msg"] = "User Login Failed incorrect userName or password";
                            return RedirectToAction("Default");
                        }
                    }
                    else
                    {
                        TempData["msg"] = "User Login Failed incorrect userName or password";
                        return RedirectToAction("Default");
                    }
                }
                else
                {
                    TempData["msg"] = "User Login Failed incorrect userName or password";
                    return RedirectToAction("Default");
                }
            }
            else
            {
                TempData["msg"] = "Incorrect Captcha";
                return RedirectToAction("Default");
            }
        }

        [AllowAnonymousAttribute]
        [HttpGetAttribute("GetLoginId")]
        public IActionResult getLoginId()
        {
            Application_ResponseWrapper response = new Application_ResponseWrapper();
            try
            {
                response.ResponseCode = "0";
                response.ResponseMessage = "Success";
                response.ResponseResult = HttpContext.Session.GetString("LoginId");
                return Ok(response);
            }
            catch (System.Exception e)
            {
                response.ResponseCode = "1000";
                response.ResponseMessage = "Error";
                response.ResponseResult = "Unable To Get LoginId || " + e.ToString();
                return Ok(response);
            }
        }

        [Authorize(Roles = "Agent")]
        public IActionResult TrainBtwnRoute([FromBodyAttribute]TrainBtwStationRequest Request)
        {
            _apiUrl = _apiUrl + "/Mpower/Rail/Booking/TrainBtwnStations";
            apiRequest Apirequest = new apiRequest
            {
                request = Request,
                userSession = "1233"
            };
            var contentData = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(Apirequest).ToString(), Encoding.UTF8, "application/json");
            using (var result = client.PostAsync(_apiUrl, contentData).Result)
            {
                if (!result.IsSuccessStatusCode)
                {
                    return null;
                }
                //Encoding enc = System.Text.Encoding.GetEncoding(1252);
                var response = result.Content.ReadAsStringAsync().Result;
                return Ok(JsonConvert.DeserializeObject<Application_ResponseWrapper>(response));
            }
        }

    }
}