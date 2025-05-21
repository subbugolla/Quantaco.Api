using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quantaco.DataAccess.Entities
{
    /// <summary>
    /// Student entity
    /// </summary>
    [Table("Student", Schema = "dbo")]
    public class Student
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;
        [StringLength(50)]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
        public string Email { get; set; } = string.Empty;
        [Required]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null;

    }
}
