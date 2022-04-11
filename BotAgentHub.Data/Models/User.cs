using System.ComponentModel.DataAnnotations;

namespace BotAgentHub.Data.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public RoleType RoleType { get; set; }
    }
}
