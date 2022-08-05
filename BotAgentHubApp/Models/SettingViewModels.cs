using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BotAgentHubApp.Models
{
    public class SettingViewModels
    {
        public string Name { get; set; }
        public IEnumerable<UserRoleDto> UserRole { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
        [Required]
        public ChatbotConfiguration ChatbotConfiguration { get; set; }
    }
}