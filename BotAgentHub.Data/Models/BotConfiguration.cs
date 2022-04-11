using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace BotAgentHub.Data.Models
{
    public class BotConfiguration : BaseEntity
    {
        public int Id { get; set; }

        [Display(Name = "Nama Bot")]
        [Required, StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "URL Web Client")]
        [Required, DataType(DataType.Url)]
        public string UrlClient { get; set; }

        [Display(Name = "URL Knowledge Base")]
        [Required, DataType(DataType.Url)]
        public string UrlKb { get; set; }

        [Display(Name = "ID Admin Web")]
        [Required]
        public int UserId { get; set; }

        [Display(Name = "Gambar Web")]
        public string BotImageName { get; set; }

        [NotMapped]
        public HttpPostedFileBase BotImageFile { get; set; }
    }
}
