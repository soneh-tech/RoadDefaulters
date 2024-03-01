using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RoadDefaulters.Models;
using System.Diagnostics;


namespace RoadDefaulters.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUser user;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        public HomeController(IUser user, Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment)
        {
            this.user = user;
            this._environment = _environment;
        }

        public async Task<IActionResult> Index()
        {
            var users = await user.GetPenaltyAsync();
            var model = new DashboardViewModel { users = users };
            return View(model);
        }
        public async Task<IActionResult> RoadUsers()
        {
            var users = await user.GetPenaltyAsync();
            return View(users);
        }
        public IActionResult Report()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var ofences = await user.GetOffencesAsync();
            var model = new UserViewModel { Offences = ofences };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            await user.CreateAsync(model);
            return RedirectToAction("create");
        }
        [HttpPost]
        public async Task<IActionResult> Modify(Penalty penalty)
        {
            await user.ModifyAsync(penalty);
            return RedirectToAction("RoadUsers");
        }
        [HttpPost]
        public async Task<IActionResult> DeletePenalty(Penalty penalty)
        {
            await user.DeleteAsync(penalty.PenaltyID);
            return RedirectToAction("RoadUsers");
        }
        public async Task<IActionResult> GetPenalty(int id)
        {
            var penalty = await user.GetPenalty(id);
            return PartialView(penalty);
        }
        public IActionResult Details()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CaptureImage(string name)
        {
            try
            {
                var files = HttpContext.Request.Form.Files;
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            var fileName = file.FileName;
                            var fileNameToStore = string.Concat(Convert.ToString(Guid.NewGuid()), Path.GetExtension(fileName));
                            //  Path to store the snapshot in local folder
                            var filepath = Path.Combine(_environment.WebRootPath, "images") + $@"\{fileNameToStore}";

                            // Save image file in local folder
                            if (!string.IsNullOrEmpty(filepath))
                            {
                                using (FileStream fileStream = System.IO.File.Create(filepath))
                                {
                                    file.CopyTo(fileStream);
                                    fileStream.Flush();
                                }
                            }

                            // Save image file in database
                            var imgBytes = System.IO.File.ReadAllBytes(filepath);
                            if (imgBytes != null)
                            {
                                if (imgBytes != null)
                                {
                                    string base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                                    string imageUrl = string.Concat("data:image/jpg;base64,", base64String);

                                    // Code to store into database
                                    // save filename and image url(base 64 string) to the database
                                }
                            }
                        }
                    }
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
