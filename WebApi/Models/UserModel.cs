using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class UserModel
    {
        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string Name { get; set; }
    }
}
