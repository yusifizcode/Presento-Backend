using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Presento.DAL;
using Presento.Helpers;
using Presento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presento.Areas.Manage.Controllers
{
    [Area("manage")]
    public class TeamMemberController : Controller
    {
        private AppDbContext _context;
        private IWebHostEnvironment _env;

        public TeamMemberController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            var teamMembers = _context.TeamMembers.ToList();
            return View(teamMembers);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TeamMember teamMember)
        {

            if (teamMember.ImageFile != null)
            {
                if (teamMember.ImageFile.ContentType != "image/png" && teamMember.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "Image file must be png, jpg or jpeg!");
                    return View();
                }
                if (teamMember.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "Image file must be less than 2MB");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("ImageFile","Image is requierd!");
                return View();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            teamMember.Image = FileManager.Save(_env.WebRootPath,"uploads/teamMembers",teamMember.ImageFile);

            _context.TeamMembers.Add(teamMember);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            var teamMember = _context.TeamMembers.FirstOrDefault(x=>x.Id == id);

            if (teamMember == null)
                return RedirectToAction("error", "dashboard");

            return View(teamMember);
        }
        [HttpPost]
        public IActionResult Edit(TeamMember teamMember)
        {
            var existTeamM = _context.TeamMembers.FirstOrDefault(x => x.Id == teamMember.Id);

            if (existTeamM == null)
                return RedirectToAction("error", "dashboard");


            if (teamMember.ImageFile != null)
            {
                if (teamMember.ImageFile.ContentType != "image/png" && teamMember.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "Image file must be png, jpg or jpeg!");
                    return View();
                }
                if (teamMember.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "Image file must be less than 2MB");
                    return View();
                }
                string newFileName = FileManager.Save(_env.WebRootPath, "uploads/teamMembers", teamMember.ImageFile);

                FileManager.Delete(_env.WebRootPath, "uploads/teamMembers", existTeamM.Image);

                existTeamM.Image = newFileName;
            }


            if (!ModelState.IsValid)
            {
                return View();
            }

      
            existTeamM.FullName = teamMember.FullName;
            existTeamM.Profession = teamMember.Profession;
            existTeamM.Instagram = teamMember.Instagram;
            existTeamM.Linkedin = teamMember.Linkedin;
            existTeamM.Twitter = teamMember.Twitter;
            existTeamM.Facebook = teamMember.Facebook;

            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            var teamMember = _context.TeamMembers.FirstOrDefault(x => x.Id == id);

            if (teamMember == null)
                return RedirectToAction("error","dashboard");

            return View(teamMember);
        }
        [HttpPost]
        public IActionResult Delete(TeamMember teamMember)
        {
            var existTeamM = _context.TeamMembers.FirstOrDefault(x=>x.Id == teamMember.Id);

            if (existTeamM == null)
                return RedirectToAction("error","dashboard");

            FileManager.Delete(_env.WebRootPath, "uploads/teamMembers", existTeamM.Image);

            _context.TeamMembers.Remove(existTeamM);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
