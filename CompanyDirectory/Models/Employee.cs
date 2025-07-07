using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyDirectory.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "ФИО обязательно")]
        [StringLength(100, ErrorMessage = "ФИО не должно превышать 100 символов")]
        [Display(Name = "ФИО")]
        public string FullName { get; set; } = string.Empty; // Инициализация по умолчанию

        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        public DateTime BirthDate { get; set; } = DateTime.Today.AddYears(-20);

        [Required(ErrorMessage = "Укажите пол")]
        [Display(Name = "Пол")]
        public string Gender { get; set; } = "Мужской";

        [Required(ErrorMessage = "Должность обязательна")]
        [StringLength(50, ErrorMessage = "Должность не должна превышать 50 символов")]
        [Display(Name = "Должность")]
        public string Position { get; set; } = string.Empty; // Инициализация по умолчанию

        [Display(Name = "Водительские права")]
        public bool HasDrivingLicense { get; set; }

        [Display(Name = "Подразделение")]
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department? Department { get; set; }
    }
}