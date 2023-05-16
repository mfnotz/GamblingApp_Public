using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class BetDTO : BaseEntityDTO
    {
        public int UserId { get; set; }
        public int BetAmount { get; set; }
        public int BetChoice { get; set; }
        public int BetActualResult { get; set; }
        public DateTime BetDoneAt { get; set; }
    }
}
