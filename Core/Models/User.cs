using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    [Table("User")]
    public class User : BaseEntityModel
    {
        public String UserName { get; set; }

        public int Credit { get; set; }
        
    }
}
