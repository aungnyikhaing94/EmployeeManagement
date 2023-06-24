using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeManagement.Models.ViewModel
{
    public class EmployeeVM
    {
        public Employee Employee { get; set; }
        public IEnumerable<SelectListItem> DepartmentSelectList { get; set; }
        public IEnumerable<SelectListItem> PositionSelectList { get; set; }
    }
}
