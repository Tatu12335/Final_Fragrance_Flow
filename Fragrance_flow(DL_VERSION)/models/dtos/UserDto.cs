using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Fragrance_flow_DL_VERSION_.models.dtos
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
