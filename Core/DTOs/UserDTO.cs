using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class UserDTO : BaseEntityDTO
    {
        public String UserName { get; set; }
        public int Credit { get; set; }
    }
}
