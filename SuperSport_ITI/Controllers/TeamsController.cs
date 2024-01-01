using FinalProject_ITI.Data;
using FinalProject_ITI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_ITI.Controllers
{
    public class TeamsController : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _webHostEnvironment;
        public TeamsController(ApplicationDbContext context , IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            
        }
        [HttpGet]
        public IActionResult IndexTeam(string? search)
        {
            ViewBag.Search = search;
            if(string.IsNullOrEmpty(search) )
            {
                return View("Index" , _context.teams.Include(p => p.Players).ToList());

            }
            return View("Index" , _context.teams.Include(P => P.Players).Where(t => t.Name.Contains(search)).ToList());
        }
        [HttpGet]
        public IActionResult DetailsTeam(int id)
        {
            Team team = _context.teams.Include(d => d.Players).FirstOrDefault(d => d.Id == id);
            ViewBag.CurrentData = team;
            if(team == null)
            {
                return NotFound();
            }
            return View("Details", team);
        }
        [HttpGet]
        public IActionResult CreateView()
        {
            return View("Create");
        }
        [HttpPost]
        public IActionResult CreateTeam(Team team , IFormFile? imageFormFile)
        {
            if(ModelState.IsValid)
            {
                if(imageFormFile != null)
                {
                    string imgExtension = Path.GetExtension(imageFormFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtension;
                    string imgPath = "\\Images\\" + imgName;
                    team.ImagePath = imgPath;
                    string imgFullPath = _webHostEnvironment.WebRootPath + imgPath;

                    FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                    imageFormFile.CopyTo(imgFileStream);
                    imgFileStream.Close();
                }
                else
                {
                    team.ImagePath = "\\Images\\No_Image.png";
                }
                _context.teams.Add(team);
                _context.SaveChanges();
                return RedirectToAction("IndexTeam");
            }
            return View("Create",team);
        }
        [HttpGet]
        public IActionResult EditView(int id)
        {
            Team team = _context.teams.FirstOrDefault(team => team.Id == id);
            if (team == null)
            {
                return NotFound();
            }
            return View("Edit", team);
        }
        [HttpPost]
        public IActionResult EditTeam(Team team, IFormFile? imageFormFile)
        {
            if (ModelState.IsValid == true)
            {
                if (imageFormFile != null)
                {
                    if (team.ImagePath != "\\Images\\No_Image.png")
                    {
                        string oldImgFullPath = _webHostEnvironment.WebRootPath + team.ImagePath;
                        System.IO.File.Delete(oldImgFullPath);
                    }
                    string imgExtension = Path.GetExtension(imageFormFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtension;
                    string imgPath = "\\Images\\" + imgName;
                    team.ImagePath = imgPath;
                    string imgFullPath = _webHostEnvironment.WebRootPath + imgPath;

                    FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                    imageFormFile.CopyTo(imgFileStream);
                    imgFileStream.Close();
                }


                _context.teams.Update(team);
                _context.SaveChanges();
                return RedirectToAction("IndexTeam");
            }
            else
            {
                return View("Edit", team);
            }

        }
        [HttpGet]
        public IActionResult DeleteView(int id)
        {
            Team team = _context.teams.Include(t => t.Players).FirstOrDefault(t => t.Id == id);
            ViewBag.CurrentData = team;

            if (team == null)
            {
                return NotFound();
            }
            return View("Delete", team);
        }

        [HttpPost]
        public IActionResult DeleteTeam(int id)
        {

            Team team = _context.teams.Find(id);


            if (team != null && team.ImagePath != "\\Images\\No_Image.png")
            {
                string imgFullPath = _webHostEnvironment.WebRootPath + team.ImagePath;
                System.IO.File.Delete(imgFullPath);
            }

            _context.teams.Remove(team);
            _context.SaveChanges();
            return RedirectToAction("IndexTeam");

        }


    }

}
