using FSHomeWork.Models.Entities;

namespace FSHomeWork.Helpers.Extensions
{
    public static class UserExtensions
    {
        public static bool CheckPassword(this User user, string password) => user.Password.Equals(password);

     
    }
}