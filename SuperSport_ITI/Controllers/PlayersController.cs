using FinalProject_ITI.Data;
using FinalProject_ITI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_ITI.Controllers
{
    public class PlayersController : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _environment;
        public PlayersController(ApplicationDbContext context , IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;    
        }
        public IActionResult IndexPlayers(string? search)
        {
            ViewBag.Search = search;
            if (string.IsNullOrEmpty(search))
            {
                return View("Index", _context.players.Include(t => t.team).ToList());
            }
            return View("Index" , _context.players.Include(t => t.team).Where( p => p.PlayerName.Contains(search) || p.PlayerPosition.Contains(search)).ToList());
        }

        public IActionResult DetailsPlayer(int id) 
        {
            Player player = _context.players.Include(t => t.team).FirstOrDefault(p => p.Id == id);
            ViewBag.CurrentData = player;
            if(Empty == null)
            {
                return NotFound();
            }
            return View("Details" , player);
        }
        public IActionResult CreateView() 
        {
            ViewBag.AllTeams = _context.teams.ToList();
            return View("Create");
        }
        [HttpPost]
        public IActionResult CreatePlayer(Player player , IFormFile? imgFileForm)
        {
            if (ModelState.IsValid == true)
            {
                if (imgFileForm != null)
                {
                    string imageExtension = Path.GetExtension(imgFileForm.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imageExtension;
                    string imgPath = "\\Images\\" + imgName;
                    player.ImagePath = imgPath;
                    string imgFullPath = _environment.WebRootPath + imgPath;

                    FileStream fileStream = new FileStream(imgFullPath, FileMode.Create);
                    imgFileForm.CopyTo(fileStream);
                    fileStream.Close();

                }
                else
                {
                    player.ImagePath = "\\Images\\No_Image.png";
                }

                _context.players.Add(player);
                _context.SaveChanges();
                return RedirectToAction("IndexPlayers");
            }
            else
            {
                ViewBag.AllTeams = _context.teams.ToList();
                return View("Create", player);
            }
        }
        [HttpGet]
        public IActionResult GetEditView(int id)
        {
            Player player = _context.players.FirstOrDefault(p => p.Id == id);
            if (player == null)
            {
                return NotFound();
            }
            ViewBag.AllTeams = _context.teams.ToList();
            return View("Edit", player);
        }

        [HttpPost]
        public IActionResult EditCurrent(Player player, IFormFile? imgFormFile)
        {
            if (ModelState.IsValid == true)
            {
                if (imgFormFile != null)
                {
                    if (player.ImagePath != "\\Images\\No_Image.png")
                    {
                        string oldImgFullPath = _environment.WebRootPath + player.ImagePath;
                        System.IO.File.Delete(oldImgFullPath);
                    }
                    string imgExtension = Path.GetExtension(imgFormFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtension;
                    string imgPath = "\\Images\\" + imgName;
                    player.ImagePath = imgPath;
                    string imgFullPath = _environment.WebRootPath + imgPath;

                    FileStream fileStream = new FileStream(imgFullPath, FileMode.Create);
                    imgFormFile.CopyTo(fileStream);
                    fileStream.Close();
                }

                _context.players.Update(player);
                _context.SaveChanges();
                return RedirectToAction("IndexPlayers");
            }
            else
            {
                ViewBag.AllTeams = _context.teams.ToList();
                return View("Edit", player);
            }

        }

        [HttpGet]
        public IActionResult GetDeleteView(int id)
        {
            Player player = _context.players.Include(t => t.team).FirstOrDefault(p => p.Id == id);
            ViewBag.CurrentData = player;
            if (player == null)
            {
                return NotFound();
            }
            return View("Delete", player);
        }

        [HttpPost]
        public IActionResult DeleteCurrent(int id)
        {
            Player player = _context.players.Find(id);
            if (player != null && player.ImagePath != "\\images\\No_Image.png")
            {
                string imgFullPath = _environment.WebRootPath + player.ImagePath;
                System.IO.File.Delete(imgFullPath);
            }
            _context.players.Remove(player);
            _context.SaveChanges();
            return RedirectToAction("IndexPlayers");

        }
    }
}
