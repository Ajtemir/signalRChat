using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FSHomeWork.DAL.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FSHomeWork.Models;
using FSHomeWork.Models.ViewModels;
using FSHomeWork.Services.TokenService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace FSHomeWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITokenService _token;
        private readonly Context _context;

        public HomeController(ILogger<HomeController> logger, ITokenService token, Context context)
        {
            _logger = logger;
            _token = token;
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            ViewBag.Users = users;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        [Authorize]
        [HttpPost]
        public IActionResult Messages([FromBody] int receiverId)
        {
            int senderId = int.Parse(HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier));
            int sum = receiverId+senderId;
            var messages = _context.Messages
                .Include(m=>m.User)
                .Where(m=>m.SumOfBothId==sum)
                .Select(m => new
                {
                    text = m.MessageText,
                    date = m.Time,
                    writer = m.Writer
                })
                .OrderByDescending(m => m.date)
                .ToList();
            return Ok(messages);
        }


        [HttpPost]
        public IActionResult Token([FromBody] UserAuthViewModel data)
        {
            var result = _token.GetToken(data);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(new {result.Token});
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}