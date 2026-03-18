using System.ComponentModel.DataAnnotations;

namespace fragrance_API.dtos.Admin
{
    public class PromoteDto
    {
        [Required]
        public int id { get; set; }
    }
}
