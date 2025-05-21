namespace Quantaco.Models
{
    /// <summary>
    /// DTOs for data transfer
    /// </summary>
    
    public class CreateStudentModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
