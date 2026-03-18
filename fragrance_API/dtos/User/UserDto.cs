using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace fragrance_API.dtos.User
{

    public class User
    {
        [Required]
        [JsonPropertyName("username")]
        public string username { get; set; }

        [Required]
        [JsonPropertyName("password")]
        public string password { get; set; }
    }

}
