using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quantaco.DataAccess.Entities
{
    /// <summary>
    /// Teacher entity
    /// </summary>
    [Table("Teacher", Schema = "dbo")]
    public class Teacher
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        [Required]
        [StringLength(50)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
        public string Email { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string PasswordHash { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [StringLength(50)] 
        public string LastName { get; set; } = string.Empty;
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
