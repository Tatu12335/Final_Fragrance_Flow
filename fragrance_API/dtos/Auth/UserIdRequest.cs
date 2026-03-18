using System.ComponentModel.DataAnnotations;

namespace fragrance_API.dtos.Auth
{
    public class UserIdRequest
    {
        [Required]
        public string username { get; set; }
    }
}
