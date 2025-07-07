using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyDirectory.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Название обязательно")]
        [StringLength(100, ErrorMessage = "Название не должно превышать 100 символов")]
        public string Name { get; set; } = string.Empty; // Инициализация по умолчанию

        [DataType(DataType.Date)]
        [Display(Name = "Дата формирования")]
        public DateTime FormationDate { get; set; } = DateTime.Today;

        [Display(Name = "Описание")]
        public string? Description { get; set; }

        [Display(Name = "Родительское подразделение")]
        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual Department? Parent { get; set; }

        public virtual ICollection<Department> Children { get; set; } = new List<Department>();
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}