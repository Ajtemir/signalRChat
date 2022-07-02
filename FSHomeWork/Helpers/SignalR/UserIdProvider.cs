using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace FSHomeWork.Helpers.SignalR
{
    public class UserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(ClaimTypes.Email)?.Value;
        }
    }
}