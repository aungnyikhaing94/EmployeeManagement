using System.ComponentModel.DataAnnotations;
namespace EmployeeManagement.Models
{
    public class Employee
    {
        [Key]
        public int empNo { get; set; }
        [Required]
        public string empName { get; set; }

        public DateTime DOB { get; set; }

        public string gender { get; set; }

        public string department { get; set; }

        public string position { get; set; }
    }
}
