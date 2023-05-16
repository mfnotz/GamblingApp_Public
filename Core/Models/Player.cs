using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    [Table("Player")]
    public class Player : BaseEntityModel
    {
        public int UserId { get; set; }
        public UserInfo User { get; set; }

        public int Credit { get; set; }
        
    }
}
