using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Position
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }


        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        public ICollection<Employee> Employees { get; set; }

    }
}
