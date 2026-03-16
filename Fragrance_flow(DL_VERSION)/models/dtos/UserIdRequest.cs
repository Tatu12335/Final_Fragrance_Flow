using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fragrance_flow_DL_VERSION_.models.dtos
{
    public class UserIdRequest
    {
        [Required]
        public string username { get; set; }
    }
}
