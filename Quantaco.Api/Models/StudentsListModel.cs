namespace Quantaco.Models
{
    /// <summary>
    /// DTOs for data transfer
    /// </summary>
        public class StudentsListModel
    {
        public IEnumerable<StudentModel> Items { get; set; }
        public int TotalCount { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}
