namespace Quantaco.Models
{
    public class TeacherAuthResponseModel
    {
        public string Token { get; set; } = string.Empty;
        public TeacherModel Teacher { get; set; } = null!;
    }
}
