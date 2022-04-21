using System.ComponentModel.DataAnnotations;

namespace BotAgentHubApp.Models
{
    public class BaseEntity
    {
        [Display(Name = "Tanggal")]
        [StringLength(15)]
        public string Date { get; set; }


        [Display(Name = "Waktu")]
        [StringLength(15)]
        public string Time { get; set; }

        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}