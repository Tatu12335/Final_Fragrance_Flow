using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Fragrance_flow_DL_VERSION_.Domain.Entities
{
    public class LoginResponse
    {
        public string token { get; set; }
        public string role { get; set; } 
        
        
    }
}
