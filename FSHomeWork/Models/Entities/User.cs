using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FSHomeWork.Models.Entities
{
    [Table("Users")]
    [Index("Email",IsUnique = true)]
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
    }
}