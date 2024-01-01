using FinalProject_ITI.Data;
using FinalProject_ITI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FinalProject_ITI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger , ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var last5Players =_context.players.Include( t => t.team ).OrderByDescending(p => p.Id).Take(5).ToList();

            var SpainTeams = _context.teams.Include(p => p.Players).Where(t => t.TeamNationality == "Spain").ToList();

            var player_team = new Player_Team
            {
                teams = SpainTeams,
                players = last5Players,
            };
            return View(player_team);
        }

        public IActionResult Privacy()
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