using System.ComponentModel.DataAnnotations;

namespace fragrance_API.dtos.Admin
{
    public class BanDto
    {
        [Required]
        public int id { get; set; }
    }
}
