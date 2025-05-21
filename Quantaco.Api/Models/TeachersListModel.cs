namespace Quantaco.Models
{
    public class TeachersListModel
    {
        public IEnumerable<TeacherModel> Items { get; set; }
        public int TotalCount { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}
