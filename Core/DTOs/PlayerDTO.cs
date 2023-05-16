using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class PlayerDTO : BaseEntityDTO
    {
        public int UserId { get; set; }
        public int Credit { get; set; }
    }
}
