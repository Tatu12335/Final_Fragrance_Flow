using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace fragrance_API.dtos.User
{
    public class CreateUserRequest
    {
        [Required]
        [JsonPropertyName("username")]
        public string username { get; set; }

        [Required]
        [JsonPropertyName("email")]
        public string email { get; set; }

        [Required]
        [JsonPropertyName("password")]
        public string password { get; set; }
    }
}
