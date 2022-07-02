using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FSHomeWork.Models.Entities
{
    [Table("Messages")]
    public class Message
    {
        [Key]
        [JsonIgnore]
        public int MessageId { get; set; }
        public string MessageText { get; set; }
        [JsonIgnore]
        public int SumOfBothId { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        [JsonIgnore]
        public User User { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public string Writer => User.Email ?? "null";
    }
}