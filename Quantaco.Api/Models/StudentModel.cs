namespace Quantaco.Models
{
    /// <summary>
    /// DTOs for data transfer
    /// </summary>
    public class StudentModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int TeacherId { get; set; }
    }
}
