using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    [Table("Bet")]
    public class Bet : BaseEntityModel
    {
        public int UserId { get; set; }
        public int BetAmount { get; set; }
        public int BetChoice { get; set; }
        public int BetActualResult { get; set; }
        public DateTime BetDoneAt { get; set; }
    }
}
