using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mpower.Travel.Web.Admin.Controllers
{
    // GET: /<controller>/
    [Authorize(Roles = "Administrator,Merchant")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult ErrorList()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Error()
        {
            return View();
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult ConfigureApp()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult PageSetting()
        {
            if (TempData["Layout"] != null)
                return View();
            else
                return RedirectToAction("Pages");
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult PageEdit(int id)
        {
            ViewData["PageId"] = id;
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult PagePans()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult PagePansAdd()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Pages()
        {
            TempData["Layout"] = this.PageLayout().ToString();
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult PageContent()
        {

            return View();
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult PageLayout()
        {
            return View();
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult MasterPans()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]

        public IActionResult PageMetaData()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult ApplicationSetting()
        {
            return View();
        }
    }
}

