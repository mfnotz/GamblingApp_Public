using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class BetResult : BaseEntityModel
    {
        public BetStatusEnum Status { get; set; }
        public int ResultingCredit { get; set; }
        public int BetActualResult { get; set; }

    }

    public enum BetStatusEnum
    { 
        Loss,
        Win,
        Error
    }
}
