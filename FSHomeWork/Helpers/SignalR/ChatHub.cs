using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FSHomeWork.DAL.Contexts;
using FSHomeWork.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace FSHomeWork.Helpers.SignalR
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly Context _context;

        public ChatHub(Context context)
        {
            _context = context;
        }

        public async Task Send(string message, string to)
        {
            var fromEmail = Context.User?.FindFirst(ClaimTypes.Email)?.Value;
            var senderId = int.Parse(Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var receiverId = _context.Users.FirstOrDefault(u => u.Email == to)!.UserId;
            await _context.Messages.AddAsync(new Message
            {
                MessageText = message,
                SumOfBothId = senderId+receiverId,
                UserId = senderId
                
            });
            await _context.SaveChangesAsync();
            await Clients.User(fromEmail!).SendAsync("Receive", message, fromEmail);
            await Clients.User(to).SendAsync("Receive", message, fromEmail);
        }
    }
}