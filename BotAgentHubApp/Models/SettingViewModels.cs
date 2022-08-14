using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace BotAgentHubApp.Models
{
    public class SettingViewModels
    {
        public string Name { get; set; }
        public IEnumerable<UserRoleDto> UserRole { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
        public ChatbotConfiguration ChatbotConfiguration { get; set; }
    }
}