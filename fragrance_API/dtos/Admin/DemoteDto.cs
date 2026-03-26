using System.ComponentModel.DataAnnotations;

namespace fragrance_API.dtos.Admin
{
    public class DemoteDto
    {
        [Required]
        public int id { get; set; }
    }
}
