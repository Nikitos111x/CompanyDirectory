namespace CompanyDirectory.ViewModels
{
    public class DepartmentInfo
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime FormationDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public int EmployeeCount { get; set; }
        public int? ParentId { get; set; }
        public string ParentName { get; set; } = string.Empty;
    }
}